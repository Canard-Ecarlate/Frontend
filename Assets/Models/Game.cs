using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public List<ICard> CardsInGame { get; set; } = new List<ICard>();
        public string CurrentPlayerId { get; set; }
        public string PreviousPlayerId { get; set; }
        public ICard PreviousDrawnCard { get; set; }
        public int RoundNb { get; set; }
        public int NbGreenDrawn { get; set; }
        public int NbDrawnDuringRound { get; set; }
        public int NbTotalRound { get; set; } = 4;
        public int NbCardsToDrawByRound { get; set; }
        public bool IsGameEnded { get; set; }
        public IRole Winners { get; set; }
    }
}