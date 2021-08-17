using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{

    class MazeNode : IComparable
    {

        public MazeNodeType Type{ get; set; }
        public MazeNode PrevNode{ get; set; }
        public int X{ get; private set; }
        public int Y{ get; private set; }
        public int Distance{ get; set; }


        public MazeNode(int posX, int posY, MazeNodeType type)
        {
            X = posX;
            Y = posY;
            Type = type;
            PrevNode = null;
            Distance = Int32.MaxValue;
        }

        public string DrawType()
        {
            switch (Type)
            {
                case MazeNodeType.WALKABLE:
                    return "▒▒";
                case MazeNodeType.BLOCKED:
                    return "██";
                case MazeNodeType.VISITED:
                    return "░░";
                case MazeNodeType.PATH:
                    return "  ";
            }
            throw new Exception("BROKEN NODE : " + Type);
        }

        public int CompareTo(object obj)
        {

            if (this != obj && obj is MazeNode)
            {
                MazeNode objM = obj as MazeNode;
                if (X < objM.X || Y < objM.Y)
                {
                    return -1;
                }
                else if (X > objM.X || Y > objM.Y)
                {
                    return 1;
                }
            }

            return 0;
        }
    }


}
