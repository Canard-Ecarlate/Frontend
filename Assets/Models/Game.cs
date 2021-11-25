using System;
using System.Collections.Generic;
using System.Text;

namespace CanardEcarlate.Models
{
    public class Game
    {
        public string name { get; set; }
        public int nbOfPlayers { get; set; }
        public string actualPlayer { get; set; }
        public List<string> waitingPlayers { get; set; }
        public int numOfTour { get; set; }
        public int greenCardsDiscovered { get; set; }
        public int cardsDiscoveredDuringTour { get; set; }
        public string lastCard { get; set; }
        public string lastPlayer { get; set; }
        public string win { get; set; }
        public List<Player> players { get; set; }

        public string greenCardsDiscoveredString { get; set; }

        public string GetNumOfTour
        {
            get { return "Tour "+(numOfTour+1) + "/4"; }
        }
        public string GetCardsDiscovered
        {
            get { return "Tirage " + cardsDiscoveredDuringTour + "/" + nbOfPlayers; }
        }
        public string GetGreenCardsDiscovered
        {
            get { return greenCardsDiscovered + "/" + nbOfPlayers; }
        }

        public string GetLastPlayer
        {
            get { return lastPlayer==null?"":lastPlayer+ " a pioché"; }
        }

        public string GetActualPlayer
        {
            get { return "C'est à " + actualPlayer + " de piocher"; }
        }
    }
}
