using System;
using System.Runtime.Serialization;

namespace WebZapService.DTOs.Responses
{
    [DataContract]
    public class SubscribeResponse : BaseResponse
    {
        [DataMember(Name = "subscription_id")]
        public int Subscription_Id { get; set; }

        public SubscribeResponse()
        {
            Subscription_Id = -1;
        }
    }
}
