using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CanardEcarlate.Models;
using CanardEcarlate.Utils;
using Newtonsoft.Json;

namespace CanardEcarlate.Controlers
{
    class UserControler : WebCommunicatorControler
    {
        public bool signUp(string pseudo, string email, string password)
        {
            string result = base.AppelWebRegistration(GlobalVariable.url + "auth/signup", pseudo, email, password);
            try
            {
                XNode node = JsonConvert.DeserializeXNode(result, "root");
                XDocument doc = XDocument.Parse(node.ToString());
                bool register = doc.Root.Element("error") == null;
                if (register)
                {
                    return true;
                }
                if(doc.Root.Element("error").Element("errors").Element("email") != null)
                {
                    GlobalVariable.CurrentUser.Error = "Email déjà utilisé";
                }
                if(doc.Root.Element("error").Element("errors").Element("pseudo") != null)
                {
                    GlobalVariable.CurrentUser.Error = "Pseudo indisponible";
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Authentification error. " + e.Message + Environment.NewLine + e.InnerException);
            }
            return false;
        }

        public bool signIn(string pseudoOrEmail, string password)
        {
            return ParseUserWebXml(base.AppelWebAuthentification(GlobalVariable.url + "auth/login", pseudoOrEmail, password));
        }

        public static bool ParseUserWebXml(string baseXML)
        {
            try
            {
                XNode node = JsonConvert.DeserializeXNode(baseXML, "root");
                XDocument doc = XDocument.Parse(node.ToString());
                bool connection = doc.Root.Element("error") == null;
                if (connection)
                {
                    GlobalVariable.CurrentUser.UserId = doc.Root.Element("userId").Value;
                    GlobalVariable.CurrentUser.Pseudo = doc.Root.Element("pseudo").Value;
                    GlobalVariable.CurrentUser.Token = doc.Root.Element("token").Value;
                    AddPlayerBySocket();
                    return true;
                }
                else
                {
                    GlobalVariable.CurrentUser.Error = doc.Root.Element("error").Value;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Authentification error. " + e.Message + Environment.NewLine + e.InnerException);
            }
            return false;
        }

        public static void AddPlayerBySocket ()
        {
            Player player = new Player()
            {
                userId = GlobalVariable.CurrentUser.UserId,
                pseudo = GlobalVariable.CurrentUser.Pseudo
            };
            GlobalVariable.Emit("addPlayer", player);
        }
    }
}
