using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebZapService.DTOs.Responses
{
    [DataContract]
    public class TestTriggerObject: BaseResponse
    {
        public TestTriggerObject() { }
        public TestTriggerObject(string messageText) 
        {
            Message_Text = messageText;
        }

        [DataMember(Name = "message_text")]
        public string Message_Text { get; set; }
    }
}





