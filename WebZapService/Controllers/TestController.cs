using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebZapService.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [HttpGet]        
        [Route("ok")]
        public string GetOK()
        {
            return "OK!!!";
        }

        [HttpGet]
        [Route("api_key_validate")]
        public HttpResponseMessage API_Key_Validate()
        {
            string API_Key = Request.Headers.GetValues("X-Dcm-Clientuid").First<string>();

            bool isValid = (API_Key.Length > 0) ? true : false;
            HttpStatusCode stc = (isValid) ? HttpStatusCode.OK : HttpStatusCode.Forbidden;

            return Request.CreateResponse<bool>(stc, isValid);
        }
    }
}
