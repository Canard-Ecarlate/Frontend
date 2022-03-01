using System.Collections.Generic;

namespace Models
{
    public class PlayerMeDto
    {
        public IRole Role { get; set; }
        public IEnumerable<ICard> CardsInHand { get; set; }
    }
}