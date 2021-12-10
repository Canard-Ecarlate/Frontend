using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CanardEcarlate.Models;
using CanardEcarlate.Utils;
using Newtonsoft.Json;

namespace CanardEcarlate.Controlers
{
    public class WebCommunicatorControler
    {
        protected readonly WebCommunicator webCommunicator;

        public WebCommunicatorControler()
        {
            webCommunicator = new WebCommunicator();
        }

        public string AppelWebAuthentification(string url, string pseudo, string password)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", pseudo));
            postData.Add(new KeyValuePair<string, string>("password", password));
            return webCommunicator.AppelWeb(url, postData);
        }

        public string AppelWebRegistration(string url, string pseudo,string email, string password, string passwordConfirm)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", pseudo));
            postData.Add(new KeyValuePair<string, string>("email", email));
            postData.Add(new KeyValuePair<string, string>("password", password));
            postData.Add(new KeyValuePair<string, string>("passwordConfirmation", passwordConfirm));
            return webCommunicator.AppelWeb(url, postData);
        }

        public string AppelWebCreateRoom(string url, string name, string nbOfPlayers, string userId, string token)
        {
            List<KeyValuePair<string, string>> room = new List<KeyValuePair<string, string>>();
            room.Add(new KeyValuePair<string, string>("name", name));
            room.Add(new KeyValuePair<string, string>("nbOfPlayers", nbOfPlayers));
            room.Add(new KeyValuePair<string, string>("userId", userId));
            Dictionary<string, string> jj = new Dictionary<string, string>();
            foreach (var item in room)
            {
                jj.Add(item.Key, item.Value);
            }

            string json = "{\"room\":" + JsonConvert.SerializeObject(jj) + "}";
            return webCommunicator.AppelWeb(url, json, token);
        }

        public string AppelWebDelete(string url, string token)
        {
            return webCommunicator.AppelWebDelete(url, token);
        }

        public string AppelWebRoom(string url, string token)
        {
            return webCommunicator.AppelWeb(url, token);
        }
    }
}
