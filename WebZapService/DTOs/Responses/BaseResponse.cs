﻿using System;
using System.Runtime.Serialization;


namespace WebZapService.DTOs.Responses
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember(Name="success")]
        public bool Success { get; set; }
        [DataMember(Name = "error")]
        public ErrorInfo Error { get; set; }

        public BaseResponse()
        {
            Success = true;
            Error = null;
        }
    }
}
