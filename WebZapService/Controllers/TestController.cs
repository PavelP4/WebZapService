using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebZapService.BusinessLogic;
using WebZapService.BusinessLogic.DTOs;
using WebZapService.DTOs.Responses;

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

        [HttpGet]
        [Route("alert_test_list")]
        public IEnumerable<ZapAlert> AlertTestList()
        {
            string zapMessage = "";
            string zapAccountName = "Your zap account.";

            for (int i = 0; i < 3; i++)
			{
			    zapMessage = string.Format("Message #{0} for testing a zap (i.e. Hello world! I'm a #{0} message!).", i + 1);
                
                yield return AlertHelper.GetNewAlert(zapMessage, zapAccountName);
			}
        }

        [HttpGet]
        [Route("test_trigger_data")]
        public IEnumerable<TestTriggerObject> TestTriggerData()
        {
            string API_Key = Request.Headers.GetValues("X-Dcm-Clientuid").First<string>();
            bool isValid = AuthHelper.ApiKeyValidate(API_Key);

            if (isValid)
            {
                for (int i = 0; i < 1; i++)
                {
                    yield return new TestTriggerObject(string.Format("Hello world! I'm a #{0} test message!).", i + 1));
                }
            }
            else
            {
                yield break;
            }
            
            //HttpStatusCode stc = (isValid) ? HttpStatusCode.OK : HttpStatusCode.Forbidden;
            //HttpResponseMessage response = Request.CreateResponse<bool>(stc, isValid);
            
        }
    }
}
