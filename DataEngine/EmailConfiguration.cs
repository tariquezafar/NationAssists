﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class EmailConfiguration
    {
        public int EmailConfigurationId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSl { get; set; }
        public string DisplayName { get; set; }
    }
}
