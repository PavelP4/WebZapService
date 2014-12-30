using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebZapService.DataAccess.DataModel;


namespace WebZapService.DTOs.Responses
{
    [DataContract]
    public class DevicesResponse: BaseResponse
    {
        [DataMember(Name="devices")]
        public List<Device> Devices { get; set; }
    }
}
