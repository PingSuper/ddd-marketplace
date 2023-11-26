using Marketplace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using Serilog;

namespace Marketplace.ClassifiedAd
{
    [Route("/ad")]
    public class ClassifiedAdsQueryApi: Controller
    {
        private static Serilog.ILogger _log = Log.ForContext<ClassifiedAdsQueryApi>();

        private readonly IAsyncDocumentSession _session;

        public ClassifiedAdsQueryApi(IAsyncDocumentSession session)
            => _session = session;

        [HttpGet]
        public Task<IActionResult> Get(QueryModels.GetPublishedClassifiedAds request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);

        [HttpGet]
        public Task<IActionResult> Get(QueryModels.GetOwnersClassifiedAd request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);

        [HttpGet]
        public Task<IActionResult> Get(QueryModels.GetPublicClassifiedAd request)
            => RequestHandler.HandleQuery(() => _session.Query(request), _log);

    }
}
