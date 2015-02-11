using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public HttpResponseMessage Subscribe(SubscribeRequest request)
        {
            ErrorInfo validateError = request.Validate();
            if (validateError != null)
            {              
                return Request.CreateResponse<SubscribeResponse>(
                    HttpStatusCode.BadRequest, 
                    new SubscribeResponse() { Success = false, Error = validateError });
            }

            SubscribeResponse response = new SubscribeResponse();
            ErrorInfo error = null;

            //Request.Headers.GetValues("X-Dcm-Clientuid").First<string>();

            try
            {
                using (DBWebZapService context = new DBWebZapService())
                {
                    Subscribe newSubscribe = new Subscribe()
                    {
                        Account_Name = request.Account_Name,
                        API_Key = Request.Headers.GetValues("X-Dcm-Clientuid").First<string>(),
                        Subscription_URL = request.Subscription_URL,
                        Target_URL = request.Target_URL,
                        Event = request.Event,
                        Created = DateTime.Now,
                        Phone = request.Phone,
                        Country_Id = request.Country_Id,
                        Device_Id = request.Device_Id
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
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    error = new ErrorInfo(0, string.Format("Error access to the database: {0}", ex.Message));
                }
            }

            if (error != null)
            {
                return Request.CreateResponse<SubscribeResponse>(
                    HttpStatusCode.BadRequest,
                    new SubscribeResponse() { Success = false, Error = error });                
            }
            else
            {
                return Request.CreateResponse<SubscribeResponse>(
                    HttpStatusCode.Created,
                    response);   
            }
        }


        [HttpDelete]
        [Route("unsubscribe")]
        public HttpResponseMessage UnSubscribe(UnSubscribeRequest request)
        {
            ErrorInfo validateError = request.Validate();
            if (validateError != null)
            {
                return Request.CreateResponse<UnSubscribeResponse>(
                    HttpStatusCode.BadRequest,
                    new UnSubscribeResponse() { Success = false, Error = validateError }); 
            }

            UnSubscribeResponse response = new UnSubscribeResponse();
            ErrorInfo error = null;

            try
            {
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
                        throw;
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
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    error = new ErrorInfo(0, string.Format("Error access to the database: {0}", ex.Message));
                }
            }

            if (error != null)
            {
                return Request.CreateResponse<UnSubscribeResponse>(
                    HttpStatusCode.BadRequest,
                    new UnSubscribeResponse() { Success = false, Error = error });
            }
            else
            {
                return Request.CreateResponse<UnSubscribeResponse>(
                    HttpStatusCode.OK,
                    response);
            }
        }
    }
}
