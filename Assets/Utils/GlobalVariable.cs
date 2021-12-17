using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using CanardEcarlate.Models;

namespace CanardEcarlate.Utils
{
    static class GlobalVariable
    {
        public readonly static User CurrentUser = new User();
        public readonly static string url = "http://canardecarlate.fr:3100/api/";
        public readonly static HttpClient HttpClient = new HttpClient();
    }
}
