using System.Collections.Generic;

namespace Models
{
    public class GameDto 
    {
        public PlayerMeDto PlayerMe { get; set; }
        public Game Game { get; set; }
        public IEnumerable<string> PlayerDrawable { get; set; }
        public IEnumerable<OtherPlayerDto> OtherPlayers { get; set; }

        public void SetGame(GameDto game)
        {
            PlayerMe = game.PlayerMe;
            Game = game.Game;
            PlayerDrawable = game.PlayerDrawable;
            OtherPlayers = game.OtherPlayers;
        }
    }
}