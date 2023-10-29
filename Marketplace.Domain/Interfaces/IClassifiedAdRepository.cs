using System;
namespace Marketplace.Domain.Interfaces
{
	public interface IClassifiedAdRepository
	{
        Task<ClassifiedAd> Load(ClassifiedAdId id);

        Task Add(ClassifiedAd entity);

        Task<bool> Exists(ClassifiedAdId id);

        // temp
        Task Save(ClassifiedAd entity);
    }
}

