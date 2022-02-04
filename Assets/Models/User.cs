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

        public void changeUser(User user)
        {
            this.id = user.id;
            this.name = user.name;
            this.email = user.email;
            this.token = user.token;
            this.error = user.error;
        }
    }
}
