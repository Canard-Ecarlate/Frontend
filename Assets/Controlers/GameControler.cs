using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using CanardEcarlate.Utils;
using Newtonsoft.Json;

namespace CanardEcarlate.Controlers
{
    class GameControler : WebCommunicatorControler
    {
        public bool getPlayer(string userId)
        {
            try
            {
                ParsePlayerWebXml(base.AppelWebRoom(GlobalVariable.url + "game/getPlayer/" + userId, GlobalVariable.CurrentUser.Token));
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetPlayer error. " + e.Message + Environment.NewLine + e.InnerException);
            }
            return false;
        }
        public bool selectedPlayer(string pseudo)
        {
            try
            {
                base.AppelWebRoom(GlobalVariable.url + "game/selectedPlayer/" + pseudo, GlobalVariable.CurrentUser.Token);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("SelectedPlayer error. " + e.Message + Environment.NewLine + e.InnerException);
            }
            return false;
        }

        public static bool ParsePlayerWebXml(string baseXML)
        {
            try
            {
                XNode node = JsonConvert.DeserializeXNode(baseXML, "root");
                XDocument doc = XDocument.Parse(node.ToString());
                bool connection = doc.Root.Element("error") == null;
                if (connection)
                {
                    GlobalVariable.Player.activeRoomName = doc.Root.Element("activeRoomName").Value;
                    GlobalVariable.Player.date = doc.Root.Element("date").Value;
                    GlobalVariable.Player.isConnected = bool.Parse(doc.Root.Element("isConnected").Value);
                    GlobalVariable.Player.nbCards = int.Parse(doc.Root.Element("nbCards").Value);
                    GlobalVariable.Player.pseudo = doc.Root.Element("pseudo").Value;
                    GlobalVariable.Player.role = doc.Root.Element("role").Value;
                    GlobalVariable.Player.userId = doc.Root.Element("userId").Value;
                    GlobalVariable.Player.hand = new List<string>();
                    foreach (var card in doc.Root.Elements("hand"))
                    {
                        GlobalVariable.Player.hand.Add(card.Value);
                    }
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
    }
}
