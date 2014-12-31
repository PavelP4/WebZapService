using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebZapService.DTOs;
using WebZapService.DTOs.Requests;
using WebZapService.DTOs.Responses;
using WebZapService.DataAccess;
using WebZapService.DataAccess.DataModel;

namespace WebZapService.Controllers
{
    [RoutePrefix("api/info")]
    public class InfoController : ApiController
    {
        [HttpGet]
        [Route("devices")]
        public DevicesResponse Devices()
        {
            DevicesResponse response = new DevicesResponse();
            ErrorInfo error = null;

            using (DBWebZapService context = new DBWebZapService())
            {
                try
                {
                    response.Devices = context.Devices.ToList<Device>();
                }
                catch (Exception ex)
                {
                    error = new ErrorInfo(0, string.Format("Cannot get devices list: {0}", ex.Message));
                }
            }

            if (error != null)
            {
                return new DevicesResponse() { Success = false, Error = error };
            }
            else
            {
                return response;
            }
        }
    }
}
