using System;
using Marketplace.Framework;
using static Marketplace.ClassifiedAd.Contracts;
using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.Shared;

namespace Marketplace.ClassifiedAd
{
    public class ClassifiedAdsApplicationService : IApplicationService
    {
        private readonly IClassifiedAdRepository _classifiedAdRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyLookup _currencyLookup;

        public ClassifiedAdsApplicationService(
            IClassifiedAdRepository classifiedAdRepository, IUnitOfWork unitOfWork,
            ICurrencyLookup currencyLookup)
        {
            _classifiedAdRepository = classifiedAdRepository;
            _unitOfWork = unitOfWork;
            _currencyLookup = currencyLookup;
        }

        public async Task Handle(object command)
        {
            switch (command)
            {
                case V1.Create cmd:
                    await HandleCreate(cmd);
                    break;

                case V1.SetTitle cmd:
                    await HandleUpdate(cmd.Id,
                        c => c.SetTitle(ClassifiedAdTitle.FromString(cmd.Title)));
                    break;

                case V1.UpdateText cmd:
                    await HandleUpdate(cmd.Id,
                        c => c.UpdateText(ClassifiedAdText.FromString(cmd.Text)));
                    break;

                case V1.UpdatePrice cmd:
                    await HandleUpdate(cmd.Id,
                        c => c.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency, _currencyLookup)));
                    break;

                case V1.RequestToPublish cmd:
                    await HandleUpdate(cmd.Id,
                        c => c.RequestToPublish());
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Command type {command.GetType().FullName} is unknown");
            }
        }

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await _classifiedAdRepository.Exists(new ClassifiedAdId(cmd.Id)))
                throw new InvalidOperationException(
                    $"Entity with id {cmd.Id} already exists");

            var classifiedAd = new Domain.ClassifiedAd.ClassifiedAd(
                new ClassifiedAdId(cmd.Id),
                new UserId(cmd.OwnerId));

            await _classifiedAdRepository.Add(classifiedAd);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(
            Guid classifiedAdId,
            Action<Domain.ClassifiedAd.ClassifiedAd> operation)
        {
            var classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(classifiedAdId));
            if (classifiedAd == null)
                throw new InvalidOperationException(
                    $"Entity with id {classifiedAdId} cannot be found");

            // ******
            operation(classifiedAd);

            await _unitOfWork.Commit();
        }

    }
}

