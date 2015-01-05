﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebZapService.DTOs.Requests
{
    [DataContract]
    public class NewAlertRequest
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "account_name")]
        public string AccountName { get; set; }
    }
}