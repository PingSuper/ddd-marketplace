using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.Contracts.ClassifiedAds;
using Serilog;
using Marketplace.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marketplace.Api
{
    [Route("/ad")]
    public class ClassifiedAdsCommandsApi : Controller
    {
        private readonly ClassifiedAdsApplicationService _applicationService;
        private static Serilog.ILogger Log = Serilog.Log.ForContext<ClassifiedAdsCommandsApi>();


        public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public Task<IActionResult> Post(ClassifiedAds.V1.Create request)
           => HandleRequest(request, _applicationService.Handle);

        //[HttpPost]
        //public async Task<IActionResult> Post(V1.Create request)
        //{
        //    await _applicationService.Handle(request);
        //    return Ok();
        //}

        [Route("name")]
        [HttpPut]
        public Task<IActionResult> Put(ClassifiedAds.V1.SetTitle request)
            => HandleRequest(request, _applicationService.Handle);

        //[Route("name")]
        //[HttpPut]
        //public async Task<IActionResult> Put(V1.SetTitle request)
        //{
        //    await _applicationService.Handle(request);
        //    return Ok();
        //}

        [Route("text")]
        [HttpPut]
        public Task<IActionResult> Put(ClassifiedAds.V1.UpdateText request)
            => HandleRequest(request, _applicationService.Handle);

        //[Route("text")]
        //[HttpPut]
        //public async Task<IActionResult> Put(V1.UpdateText request)
        //{
        //    await _applicationService.Handle(request);
        //    return Ok();
        //}

        [Route("price")]
        [HttpPut]
        public Task<IActionResult> Put(ClassifiedAds.V1.UpdatePrice request)
            => HandleRequest(request, _applicationService.Handle);

        //[Route("price")]
        //[HttpPut]
        //public async Task<IActionResult> Put(V1.UpdatePrice request)
        //{
        //    await _applicationService.Handle(request);
        //    return Ok();
        //}89

        [Route("publish")]
        [HttpPut]
        public Task<IActionResult> Put(ClassifiedAds.V1.RequestToPublish request)
            => HandleRequest(request, _applicationService.Handle);

        private async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task> handler)
        {
            try
            {
                Log.Debug("Handling HTTP request of type {type}", typeof(T).Name);
                await handler(request);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error("Error handling the request", e);
                return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }

    }
}

