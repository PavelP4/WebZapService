using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebZapService.DTOs.Responses;
using WebZapService.DTOs.Requests;
using System.Net.Http;
using System.Net.Http.Headers;
using WebZapService.DataAccess.DataModel;
using WebZapService.DataAccess;
using WebZapService.Controllers;

namespace ZapService.Test
{
    [TestClass]
    public class ZapServiceHostTest
    {
        private string ServiceUri = "http://192.168.107.132:8800/";
        //private string ServiceUri = "http://localhost:58158/";



        [TestMethod]
        public async Task Get_OK()
        {    
            using (HttpClient client = new HttpClient())                     
            {
                client.BaseAddress = new Uri(ServiceUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/test/ok");  
                string result = string.Empty;

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }

                result = result.Trim(new []{'"'});
                
                Assert.AreEqual<string>("OK!!!", result);         
            }                           
        }

        [TestMethod]
        public async Task Get_Devices()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/info/devices");
                DevicesResponse result = null;

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<DevicesResponse>();
                }


                Assert.IsNotNull(result, "Result object must be not null.");
                Assert.IsNotNull(result.Devices, "Property Devices must be not null.");
                Assert.IsTrue(result.Devices.Count > 0, "The Device's list must contain some data.");
            }
        }

        [TestMethod]        
        public async Task NewAlertEvent()
        {
            NewAlertRequest requestObj = new NewAlertRequest() { 
                Message = string.Format("This is a new alert message == App V2 == for test (Date and Time: {0}).", DateTime.Now),
                API_Key = "3719977C0ADC4E29A650D147A3916983" 
            };
            NewAlertResponse responseObj = null;
           

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                
                var response = await client.PostAsJsonAsync("api/events/new_alert", requestObj);

                if (response.IsSuccessStatusCode)
                {
                    responseObj = await response.Content.ReadAsAsync<NewAlertResponse>();
                }
            }


            Assert.IsNotNull(responseObj, "Response object must to be received.");
            Assert.IsTrue(responseObj.Success,
                string.Format("Success must to be TRUE. Error: {0}.", (responseObj.Error == null) ? "none" : responseObj.Error.Message));
        }

       

        //[TestMethod]        
        //public async Task Subscribe_Unsubscribe()
        //{
        //    SubscribeRequest requestObj = new SubscribeRequest() { Event = "Trigger_H", Target_URL = @"http://test.com/asdf" };
        //    SubscribeResponse responseObj = null;
        //    UnSubscribeResponse responseObj2 = null;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(ServiceUri);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // UnSubscribe
        //        HttpResponseMessage response = await client.PostAsJsonAsync("api/hooks/subscribe", requestObj);
                
        //        if (response.IsSuccessStatusCode)
        //        {
        //            responseObj = await response.Content.ReadAsAsync<SubscribeResponse>();
        //        }
                                
        //        Assert.IsNotNull(responseObj, "responseObj must to be created.");
        //        Assert.IsTrue(responseObj.Success, "Success must to be TRUE.");
        //        Assert.IsTrue(responseObj.Subscription_Id > 0, "SubscribeId must to be more than 0.");


        //        // UnSubscribe
        //        response = await client.DeleteAsync((string.Format("api/hooks/unsubscribe", responseObj.Subscription_Id));
                
        //        if (response.IsSuccessStatusCode)
        //        {
        //            responseObj2 = await response.Content.ReadAsAsync<UnSubscribeResponse>();
        //        }

        //        Assert.IsNotNull(responseObj2, "responseObj2 must to be created.");
        //        Assert.IsTrue(responseObj2.Success, "Success must to be TRUE.");
        //    } 
        //}

        //[TestMethod]
        //public async Task Unsubscribe()
        //{
        //    UnSubscribeResponse responseObj2 = null;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(ServiceUri);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        //        // UnSubscribe
        //        HttpResponseMessage response = await client.DeleteAsync("api/hooks/unsubscribe");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            responseObj2 = await response.Content.ReadAsAsync<UnSubscribeResponse>();
        //        }

        //        Assert.IsNotNull(responseObj2, "responseObj2 must to be created.");
        //        Assert.IsTrue(responseObj2.Success, "Success must to be TRUE.");
        //    }
        //}

    }
}
