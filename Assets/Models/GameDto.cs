using System.Collections.Generic;

namespace Models
{
    public class GameDto 
    {
        public PlayerMeDto PlayerMe { get; set; }
        public Game Game { get; set; }
        public HashSet<string> PlayerDrawable { get; set; }
        public HashSet<OtherPlayerDto> OtherPlayers { get; set; }
    }
}