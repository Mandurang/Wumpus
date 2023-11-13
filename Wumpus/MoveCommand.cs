using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class MoveCommand : ICommand
    {
        private Player player;
        private int newX, newY;

        public MoveCommand(Player player, int newX, int newY)
        {
            this.player = player;
            this.newX = newX;
            this.newY = newY;
        }

        public void Execute()
        {
            player.Move(newX, newY);
        }
    }
}
