using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using CanardEcarlate.Controlers;
using CanardEcarlate.Models;
using UnityEngine;

namespace CanardEcarlate.Utils
{
    static class GlobalVariable
    {
        public readonly static User CurrentUser = new User();
        public readonly static string url = "http://canardecarlate.fr:3100/api/";
        public readonly static HttpClient HttpClient = new HttpClient();
        public readonly static WebCommunicatorControler webCommunicatorControler = new WebCommunicatorControler();
    }
}
