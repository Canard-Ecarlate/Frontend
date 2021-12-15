using System;
using System.Collections.Generic;
using System.Text;

namespace CanardEcarlate.Models
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public string error { get; set; }
    }
}
