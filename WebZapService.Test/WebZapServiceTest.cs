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
