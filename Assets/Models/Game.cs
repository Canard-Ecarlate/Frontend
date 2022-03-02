using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public List<ICard> CardsInGame { get; set; }
        public string CurrentPlayerId { get; set; }
        public string CurrentPlayerName { get; set; }
        public string PreviousPlayerId { get; set; }
        public string PreviousPlayerName { get; set; }
        public ICard PreviousDrawnCard { get; set; }
        public int RoundNb { get; set; }
        public int NbGreenDrawn { get; set; }
        public int NbDrawnDuringRound { get; set; }
        public int NbTotalRound { get; set; }
        public int NbCardsToDrawByRound { get; set; }
        public bool IsGameEnded { get; set; }
        public IRole Winners { get; set; }
    }
}