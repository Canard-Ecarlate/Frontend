namespace Models
{
    public class OtherPlayerDto
    {
        public string PlayerId { get; set; }
        public int NbCardsInHand { get; set; }

        public OtherPlayerDto(string playerId, int nbCardsInHand)
        {
            PlayerId = playerId;
            NbCardsInHand = nbCardsInHand;
        }
    }
}