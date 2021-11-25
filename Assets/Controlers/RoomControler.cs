using CanardEcarlate.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace CanardEcarlate.Controlers
{
    class RoomControler : WebCommunicatorControler
    {
        public bool createRoom(string name, string nbOfPlayers)
        {
            try
            {
                string result = base.AppelWebCreateRoom(GlobalVariable.url + "room/create", name, nbOfPlayers, GlobalVariable.CurrentUser.UserId, GlobalVariable.CurrentUser.Token);
                XNode node = JsonConvert.DeserializeXNode(result, "root");
                XDocument doc = XDocument.Parse(node.ToString());
                bool connection = doc.Root.Element("error") == null;
                if (connection)
                {
                    GlobalVariable.CurrentRoom.name = name;
                    return true;
                }
                else
                {
                    GlobalVariable.CurrentUser.Error = "Le nom n'est pas disponible";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Creation error. " + e.Message + Environment.NewLine + e.InnerException);
                GlobalVariable.CurrentUser.Error = "La salle n'a pas pu être créée";
            }
            return false;
        }

        public bool deleteRoom(string name)
        {
            try
            {
                string result = base.AppelWebDelete(GlobalVariable.url + "room/delete/" + name, GlobalVariable.CurrentUser.Token);
                XNode node = JsonConvert.DeserializeXNode(result, "root");
                XDocument doc = XDocument.Parse(node.ToString());
                bool connection = doc.Root.Element("error") == null;
                if (connection)
                {
                    return true;
                }
                else
                {
                    GlobalVariable.CurrentUser.Error = "Le salle n'a pas pu être supprimée";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Creation error. " + e.Message + Environment.NewLine + e.InnerException);
                GlobalVariable.CurrentUser.Error = "Le salle n'a pas pu être supprimée";
            }
            return false;
        }
    }
}
