using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using CanardEcarlate.Models;
using SocketIOClient;

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
        public static SocketIO socketIO = new SocketIO("http://canardecarlate.fr:3100/");
        
        public static void ConnectSocket()
        {
            try
            {
                GlobalVariable.socketIO.ConnectAsync();
            }
            catch (Exception e)
            {
                CurrentUser.Error = "Impossible de se connecter veuillez redémarrer l'application";
                Debug.WriteLine("Socket error- " + e.Message + Environment.NewLine + e.InnerException);
            }
        }

        public static void Emit(string channel, Object anobject)
        {
            try
            {
                Console.WriteLine("check socket "+socketIO.Connected);
                GlobalVariable.socketIO.EmitAsync(channel, anobject);
            }
            catch (Exception e)
            {
                CurrentUser.Error = "Impossible de se connecter veuillez redémarrer l'application";
                Debug.WriteLine("Socket error- " + e.Message + Environment.NewLine + e.InnerException);

            }
        }
    }
}
