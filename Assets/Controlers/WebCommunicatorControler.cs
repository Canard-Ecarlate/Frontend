using System.Collections.Generic;
using Models;
using Newtonsoft.Json;

namespace Controlers
{
    public class WebCommunicatorControler
    {
        protected readonly WebCommunicator WebCommunicator;

        public WebCommunicatorControler()
        {
            WebCommunicator = new WebCommunicator();
        }

        public string AppelWebAuthentification(string url, string pseudo, string password)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", pseudo));
            postData.Add(new KeyValuePair<string, string>("password", password));
            return WebCommunicator.AppelWeb(url, postData);
        }

        public string AppelWebRegistration(string url, string pseudo,string email, string password, string passwordConfirm)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", pseudo));
            postData.Add(new KeyValuePair<string, string>("email", email));
            postData.Add(new KeyValuePair<string, string>("password", password));
            postData.Add(new KeyValuePair<string, string>("passwordConfirmation", passwordConfirm));
            return WebCommunicator.AppelWeb(url, postData);
        }
        
        public string AppelWebCheckToken(string url, string token)
        {
            string postData = "";
            return WebCommunicator.AppelWeb(url, postData, token);
        }
        
        public string AppelWebAuthentification(string url, string token)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("token", token));
            return WebCommunicator.AppelWeb(url, postData);
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
            return WebCommunicator.AppelWeb(url, json, token);
        }

        public string AppelWebDelete(string url, string token)
        {
            return WebCommunicator.AppelWebDelete(url, token);
        }

        public string AppelWebRoom(string url, string token)
        {
            return WebCommunicator.AppelWeb(url, token);
        }
    }
}
