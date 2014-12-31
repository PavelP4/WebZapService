using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebZapService.DataAccess;
using WebZapService.DataAccess.DataModel;
using WebZapService.DTOs;
using WebZapService.DTOs.Requests;
using WebZapService.DTOs.Responses;


namespace WebZapService.Controllers
{
    [RoutePrefix("api/hooks")]
    public class HooksController : ApiController
    {
        [HttpPost]
        [Route("subscribe")]
        public SubscribeResponse Subscribe(SubscribeRequest request)
        {
            //WebOperationContext ctx = WebOperationContext.Current;

            ErrorInfo validateError = request.Validate();
            if (validateError != null)
            {
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                return new SubscribeResponse() { Success = false, Error = validateError };
            }

            SubscribeResponse response = new SubscribeResponse();
            ErrorInfo error = null;

            using (DBWebZapService context = new DBWebZapService())
            {
                Subscribe newSubscribe = new Subscribe()
                {
                    Account_Name = request.Account_Name,
                    Subscription_URL = request.Subscription_URL,
                    Target_URL = request.Target_URL,
                    Event = request.Event,
                    Created = DateTime.Now
                };

                try
                {
                    context.Subscribes.Add(newSubscribe);
                    context.SaveChanges();

                    response.Subscription_Id = newSubscribe.Id;
                }
                catch (Exception ex)
                {
                    error = new ErrorInfo(0, string.Format("Error saving to the database: {0}", ex.Message));
                }
            }

            if (error != null)
            {
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                return new SubscribeResponse() { Success = false, Error = error };
            }
            else
            {
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.Created;
                return response;
            }
        }


        [HttpDelete]
        [Route("unsubscribe")]
        public UnSubscribeResponse UnSubscribe(UnSubscribeRequest request)
        {
            //WebOperationContext ctx = WebOperationContext.Current;

            ErrorInfo validateError = request.Validate();
            if (validateError != null)
            {
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                return new UnSubscribeResponse() { Success = false, Error = validateError };
            }

            UnSubscribeResponse response = new UnSubscribeResponse();
            ErrorInfo error = null;

            using (DBWebZapService context = new DBWebZapService())
            {
                Subscribe delSubscribe = null;

                try
                {
                    delSubscribe = context.Subscribes.Find(request.Subscription_Id);
                }
                catch (Exception ex)
                {
                    error = new ErrorInfo(0, string.Format("Error serching in the database: {0}", ex.Message));
                }

                if (delSubscribe == null)
                {
                    error = new ErrorInfo(0, string.Format("Data by [ ID = {0} ] not found.", request.Subscription_Id));
                }

                delSubscribe.IsUnsubscribed = true;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    error = new ErrorInfo(0, string.Format("Error saving to the database: {0}", ex.Message));
                }
            }

            if (error != null)
            {
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                return new UnSubscribeResponse() { Success = false, Error = error };
            }
            else
            {
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }
    }
}
