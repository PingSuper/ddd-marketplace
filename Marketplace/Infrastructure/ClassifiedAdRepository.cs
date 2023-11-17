using System;
using System.Threading.Tasks;
using Marketplace.Domain;
using Marketplace.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Raven.Client.Documents.Session;

namespace Marketplace.Infrastructure
{
    public class ClassifiedAdRepository : IClassifiedAdRepository, IDisposable
    {
        private readonly ClassifiedAdDbContext _dbContext;

        public ClassifiedAdRepository(ClassifiedAdDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(ClassifiedAd entity)
            => await _dbContext.ClassifiedAds.AddAsync(entity);

        public async Task<bool> Exists(ClassifiedAdId id)
            => await _dbContext.ClassifiedAds.FindAsync(id.Value) != null;

        public async Task<ClassifiedAd> Load(ClassifiedAdId id)
            => await _dbContext.ClassifiedAds.FindAsync(id.Value);

        public void Dispose() => _dbContext.Dispose();
    }

    //public class ClassifiedAdRepository : IClassifiedAdRepository, IDisposable
    //{
    //    private readonly IAsyncDocumentSession _session;

    //    public ClassifiedAdRepository(IAsyncDocumentSession session)
    //         => _session = session;

    //    public Task Add(ClassifiedAd entity)
    //     => _session.StoreAsync(entity, EntityId(entity.Id));

    //    public Task<bool> Exists(ClassifiedAdId id)
    //        => _session.Advanced.ExistsAsync(EntityId(id));

    //    public Task<ClassifiedAd> Load(ClassifiedAdId id)
    //        => _session.LoadAsync<ClassifiedAd>(EntityId(id));

    //    public void Dispose() => _session.Dispose();

    //    private static string EntityId(ClassifiedAdId id)
    //        => $"ClassifiedAd/{id.ToString()}";

    //}
}
