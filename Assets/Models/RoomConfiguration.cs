using System;
using System.Collections.Generic;

namespace Models
{
    public class RoomConfiguration
    {
        public bool IsPrivate { get; set; }
        public int NbPlayers { get; set; }
        public List<NbEachCard> Cards { get; set; }
        public List<NbEachRole> Roles { get; set; }
        public int NumberOfCardsFirstRound { get; set; }

        // public RoomConfiguration(bool isPrivate, int nbPlayers)
        // {
        //     IsPrivate = isPrivate;
        //     NbPlayers = nbPlayers;
        //     Cards = new List<NbEachCard>
        //     {
        //         new NbEachCard("Bomb",1),
        //         new NbEachCard("Green",NbPlayers),
        //         new NbEachCard("Yellow", (NbPlayers * NumberOfCardsFirstRound) - NbPlayers - 1)
        //     };
        //     int redPlayerNumber = NbPlayers / 2;
        //     if (NbPlayers % 2 == 0)
        //     {
        //         Random random = new Random();
        //         int rnd = random.Next(4);
        //         if(rnd % 2 == 0)
        //         {
        //             redPlayerNumber--;
        //         }
        //     }
        //     Roles = new List<NbEachRole>
        //     {
        //         new NbEachRole("Blue", NbPlayers - redPlayerNumber),
        //         new NbEachRole("Red", redPlayerNumber),
        //     };
        // }
    }
}