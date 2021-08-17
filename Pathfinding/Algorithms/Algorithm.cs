using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Algorithms
{
    class Algorithm
    {
        public Maze maze;

        public Algorithm(Maze maze)
        {
            this.maze = maze;
            
        }

        public virtual List<MazeNode> SearchAlgo() => null;
    }
}
