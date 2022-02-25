using System.Collections.Generic;

namespace Models
{
    public interface ICard
    {
        string Name { get; }

        void DrawAction(Player playerWhoDraw, Player playerWhereCardIsDrawing, Game game, HashSet<Player> players);
    }
}
