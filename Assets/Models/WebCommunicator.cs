using CanardEcarlate.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using UnityEngine;

namespace CanardEcarlate.Models
{
    public class WebCommunicator
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public WebCommunicator()
        {
            try
            {
                HttpClient.Timeout = TimeSpan.FromSeconds(20);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        /// <summary>
        /// Appel API : Utilisé pour récupérer les données du site. Envoie des données en GET
        /// </summary>
        public string AppelWeb(string url, string TOKEN)
        {
            Debug.Log(url);
            Debug.Log(TOKEN);
            string retour = "";
            try
            {
                StringContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                //Récupération du XML
                var response = HttpClient.GetAsync(url).Result;

                //Parse du XML reçu
                retour = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, string TOKEN) >>>>>>>>>>>>>>>>>");
                Debug.Log(url);
                Debug.Log(TOKEN);
                Debug.Log(e.Message + Environment.NewLine + e.InnerException);
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, string TOKEN) >>>>>>>>>>>>>>>>>");
            }
            return retour;
        }

        /// <summary>
        /// Appel API : Envoie des données en POST
        /// </summary>
        public string AppelWeb(string url, XDocument postData, string TOKEN)
        {
            string retour = "";
            try
            {
                string json = JsonConvert.SerializeObject(postData);
                StringContent content = new StringContent(json);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                //Récupération du XML
                var response = HttpClient.PostAsync(url, content).Result;

                //Parse du XML reçu
                retour = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, XDocument postData, string TOKEN) >>>>>>>>>>>>>>>>>");
                Debug.Log(url);
                Debug.Log(e.Message + Environment.NewLine + e.InnerException);
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, XDocument postData, string TOKEN) >>>>>>>>>>>>>>>>>");
                retour = "{\"Status\":999,\"Errors\":[\"" + e.Message + "\"]}";
            }
            return retour;
        }

        public string AppelWeb(string url, string postData, string TOKEN)
        {
            string retour = "";
            try
            {
                StringContent content = new StringContent(postData);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                //Récupération du XML
                var response = HttpClient.PostAsync(url, content).Result;

                //Parse du XML reçu
                retour = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, XDocument postData, string TOKEN) >>>>>>>>>>>>>>>>>");
                Debug.Log(url);
                Debug.Log(e.Message + Environment.NewLine + e.InnerException);
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, XDocument postData, string TOKEN) >>>>>>>>>>>>>>>>>");
                retour = "{\"Status\":999,\"Errors\":[\"" + e.Message + "\"]}";
            }
            return retour;
        }

        //Delete
        public string AppelWebDelete(string url, string TOKEN)
        {
            string retour = "";
            try
            {                
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                //Récupération du XML
                var response = HttpClient.DeleteAsync(url).Result;

                //Parse du XML reçu
                retour = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, XDocument postData, string TOKEN) >>>>>>>>>>>>>>>>>");
                Debug.Log(url);
                Debug.Log(e.Message + Environment.NewLine + e.InnerException);
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, XDocument postData, string TOKEN) >>>>>>>>>>>>>>>>>");
                retour = "{\"Status\":999,\"Errors\":[\"" + e.Message + "\"]}";
            }
            return retour;
        }
        /// <summary>
        /// Appel API : utilisée pour l'authentification. Envoie des données en POST
        /// </summary>
        public string AppelWeb(string url)
        {
            string retour = "";
            try
            {
                //Construction des entetes POST
                var postData = new List<KeyValuePair<string, string>>();

                Dictionary<string, string> jj = new Dictionary<string, string>();
                foreach (var item in postData)
                {
                    jj.Add(item.Key, item.Value);
                }

                string json = JsonConvert.SerializeObject(jj);

                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //Récupération du XML
                var response = HttpClient.PostAsync(url, content).Result;

                //Parse du XML reçu
                retour = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, List<KeyValuePair<string, string>> postData = null) >>>>>>>>>>>>>>>>>");
                Debug.Log(url);
                Debug.Log(e.Message + Environment.NewLine + e.InnerException);
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, List<KeyValuePair<string, string>> postData = null) >>>>>>>>>>>>>>>>>");
                GlobalVariable.CurrentUser.Error = "Erreur Réseau, veuillez vérifier votre connexion internet et relancer l'application";
            }
            return retour;
        }
        
        public string AppelWeb(string url, List<KeyValuePair<string, string>> postData = null)
        {
            string retour = "";
            try
            {
                //Construction des entetes POST
                if (postData == null)
                {
                    postData = new List<KeyValuePair<string, string>>();
                }

                Dictionary<string, string> jj = new Dictionary<string, string>();
                foreach (var item in postData)
                {
                    jj.Add(item.Key, item.Value);
                }

                string json = JsonConvert.SerializeObject(jj);

                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //Récupération du XML
                var response = HttpClient.PostAsync(url, content).Result;

                //Parse du XML reçu
                retour = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, List<KeyValuePair<string, string>> postData = null) >>>>>>>>>>>>>>>>>");
                Debug.Log(url);
                Debug.Log(e.Message + Environment.NewLine + e.InnerException);
                Debug.Log("<<<<<<<<<<<<<<<<< AppelWeb(string url, List<KeyValuePair<string, string>> postData = null) >>>>>>>>>>>>>>>>>");
                GlobalVariable.CurrentUser.Error = "Erreur Réseau, veuillez vérifier votre connexion internet et relancer l'application";
            }
            return retour;
        }
    }
}
