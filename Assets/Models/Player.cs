using System;
using System.Collections.Generic;
using System.Text;

namespace CanardEcarlate.Models
{
    public class Player : ClasseParfaite
    {
        public string userId { get; set; }
        public string pseudo { get; set; }
        public string activeRoomName { get; set; }
        public string role { get; set; }
        public List<string> hand { get; set; }
        public bool isConnected { get; set; }
        public string date { get; set; }
        public int nbCards { get; set; }

    }
}
