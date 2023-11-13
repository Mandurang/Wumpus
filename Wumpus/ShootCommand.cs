using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class ShootCommand : ICommand
    {
        private Player player;
        private WumpusWorldGame world;
        private int directionX, directionY;

        public ShootCommand(Player player, WumpusWorldGame world, int directionX, int directionY)
        {
            this.player = player;
            this.world = world;
            this.directionX = directionX;
            this.directionY = directionY;
        }

        public void Execute()
        {
            player.ShootArrow(directionX, directionY, world);
        }
    }
}
