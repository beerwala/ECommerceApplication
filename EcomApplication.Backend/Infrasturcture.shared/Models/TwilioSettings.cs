﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.shared.Models
{
    public class TwilioSettings
    {
        public string AccountSid {  get; set; }
        public string AuthToken { get; set; }
        public string SenderPhoneNumber { get; set; }

    }
}
