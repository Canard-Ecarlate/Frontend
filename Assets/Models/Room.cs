using System;
using System.Collections.Generic;
using System.Text;

namespace CanardEcarlate.Models
{
    public class Room : ClasseParfaite
    {
        public string name { get; set; }
        public int nbOfPlayers { get; set; }
        public string userId { get; set; }
        public List<Player> players { get; set; }
        public List<string> listOfCards { get; set; }
        public string actualPlayer { get; set; }
        public List<string> waitingPlayers { get; set; }
        public int numOfTour { get; set; }
        public int greenCardsDiscovered { get; set; }
        public int cardsDiscoveredDuringTour { get; set; }
        public string lastCard { get; set; }
        public string win { get; set; }
        public bool isInGame { get; set; }

        public string GetNbPlayersInRoom
        {
            get { return players.Count + "/" + nbOfPlayers + " joueurs"; }
            protected set { GetNbPlayersInRoom = value; }
        }

        public string GetCreatorPseudo
        {
            get { return "👑 " + userId; }
            //protected set { GetCreator = value; }
        }

        public bool RoomIsFull 
        {
            get { return nbOfPlayers == players.Count; }
        }

        public bool RoomIsNotFull
        {
            get { return nbOfPlayers != players.Count; }
        }
    }
}
