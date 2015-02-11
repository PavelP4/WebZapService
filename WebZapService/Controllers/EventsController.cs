using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebZapService.DTOs.Requests;
using WebZapService.DTOs.Responses;
using WebZapService.DataAccess;
using WebZapService.DTOs;
using WebZapService.BusinessLogic;
using WebZapService.DataAccess.DataModel;
using System.Net.Http.Headers;
using WebZapService.Exceptions;

namespace WebZapService.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        private const string EN_NEW_ALERT = "event_new_alert";

        [HttpPost]
        [Route("new_alert")]
        public async Task<NewAlertResponse> NewAlert(NewAlertRequest request)
        {            
            ErrorInfo error = null;

            try
            {
                using (DBWebZapService context = new DBWebZapService())
                {
                    var subscriptions = from s in context.Subscribes
                                        where s.API_Key == request.API_Key
                                            && s.Event == EN_NEW_ALERT
                                            && s.IsUnsubscribed == false
                                        select s;

                    int countS = 0;

                    foreach (var item in subscriptions)
                    {
                        try
                        {
                            await NewAlertPost(item.Subscription_URL, request.Message, item.Account_Name);                            
                        }
                        catch (Exception ex)
                        {
                            error = new ErrorInfo(0, string.Format("Error occurred during send alerts ({0}).", ex.Message));
                            throw;
                        }

                        countS++;
                    }

                    if (countS == 0)
                    {
                        error = new ErrorInfo(0, "Has not found any subscriptions.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    error = new ErrorInfo(0, ex.Message);
                }
            }

            if (error != null)
            {
                return new NewAlertResponse() { Success = false, Error = error };
            }
            else
            {
                return new NewAlertResponse();
            }
        }

        private async Task NewAlertPost(string webhookUri, string alertMessage, string accountName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(webhookUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("", AlertHelper.GetNewAlert(alertMessage, accountName));

                if (!response.IsSuccessStatusCode)
                {
                    throw new AlertSendException(string.Format("Unsuccessfully sent. Status code: {0}", response.StatusCode));
                }
            }
        }
    }
}
