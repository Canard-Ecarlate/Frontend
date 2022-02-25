using System.Collections.Generic;

namespace Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public bool Ready { get; set; }
        public IRole Role { get; set; }
        public List<ICard> CardsInHand { get; set; } = new List<ICard>();
        public bool IsCardsDrawable { get; set; } = true;
    }
}
