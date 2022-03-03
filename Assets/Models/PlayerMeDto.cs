using System.Collections.Generic;

namespace Models
{
    public class PlayerMeDto
    {
        public IRole Role { get; set; }
        public List<ICard> CardsInHand { get; set; }
    }
}