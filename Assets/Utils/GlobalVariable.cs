﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using CanardEcarlate.Models;

namespace CanardEcarlate.Utils
{
    class GlobalVariable
    {
        public static User CurrentUser = new User();
        public static Room CurrentRoom = new Room();
        public static Game Game = new Game();
        public static Player Player = new Player();
        public static string url = "http://canardecarlate.fr:3100/api/";
        public static HttpClient HttpClient = new HttpClient();
    }
}
