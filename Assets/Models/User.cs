using System;
using System.Collections.Generic;
using System.Text;

namespace CanardEcarlate.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Pseudo { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }

    }
}
