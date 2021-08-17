using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    class Maze
    {
        public const int size_ = 30;

        public MazeNode[,] MazeNodeMatrix { get; set; }
        public MazeNode StartNode { get; set; }
        public MazeNode EndNode { get; set; }


        public Maze()
        {
            MazeNodeMatrix = new MazeNode[size_, size_];

            for (int i = 0; i < size_; i++)
            {
                for (int j = 0; j < size_; j++)
                {
                    MazeNodeMatrix[i, j] = new MazeNode(i, j, MazeNodeType.BLOCKED);
                }
            }

            CreatePaths();
        }

        public void DrawMaze()
        {
            Console.ResetColor();

            for (int i = 0; i < size_; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int j = 0; j < size_; j++)
                {
                    switch(MazeNodeMatrix[i, j].Type)
                    {
                        case MazeNodeType.WALKABLE :
                            Console.BackgroundColor = ConsoleColor.Gray;
                            break;
                        case MazeNodeType.VISITED:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case MazeNodeType.PATH:
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                    }

                    if (StartNode == MazeNodeMatrix[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    if (EndNode == MazeNodeMatrix[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    }
                    Console.Write(MazeNodeMatrix[i, j].DrawType());
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("▒▒");
            Console.ResetColor();
            Console.WriteLine(" : Walkable");
            
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("██");
            Console.ResetColor();
            Console.WriteLine(" : Blocked");

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write("░░");
            Console.ResetColor();
            Console.WriteLine(" : Visited");
           
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("  ");
            Console.ResetColor();
            Console.WriteLine(" : Path");

        }


        public void CreatePaths()
        {
            
            int curNodeX = 1;
            int curNodeY = 1;

            Stack<MazeNode> stack = new Stack<MazeNode>();
            MazeNodeMatrix[curNodeX, curNodeY].Type = MazeNodeType.WALKABLE;

            StartNode = MazeNodeMatrix[curNodeX, curNodeY];

            stack.Push(MazeNodeMatrix[curNodeX, curNodeY]);

            int[] pos = null;

            while (stack.Count != 0)
            {
                pos = FindNextPlace(curNodeX, curNodeY);
                if (pos != null)
                {
                    curNodeX = pos[0];
                    curNodeY = pos[1];
                    MazeNodeMatrix[curNodeX, curNodeY].Type = MazeNodeType.WALKABLE;
                    stack.Push(MazeNodeMatrix[curNodeX, curNodeY]);
                }
                else
                {
                    var node = stack.Pop();
                    pos = FindNextPlace(node.X, node.Y);
                    if (pos != null)
                    {
                        curNodeX = pos[0];
                        curNodeY = pos[1];
                        MazeNodeMatrix[curNodeX, curNodeY].Type = MazeNodeType.WALKABLE;
                        stack.Push(MazeNodeMatrix[curNodeX, curNodeY]);
                    }

                }
            }

            List<MazeNode> waklableNodes = new List<MazeNode>();
            foreach (var node in MazeNodeMatrix)
            {
                if (node.Type == MazeNodeType.WALKABLE && StartNode != node)
                {
                    waklableNodes.Add(node);
                }
            }
            Random rand = new Random();
            var index = rand.Next(0, waklableNodes.Count);

            EndNode = waklableNodes[index];

        }

        public int[] FindNextPlace(int curX, int curY)
        {
            int[] pos = new int[2];
            List<MazeNode> validMazeNodes = new List<MazeNode>();
            
            if (EvaluateNode(curX - 2, curY) && EvaluateNode(curX - 1, curY))
            {
                if (EvaluateNeighbors(curX - 1, curY))
                {
                    validMazeNodes.Add(MazeNodeMatrix[curX - 1, curY]);
                }
                
            }
            if (EvaluateNode(curX + 2, curY) && EvaluateNode(curX + 1, curY))
            {
                if (EvaluateNeighbors(curX + 1, curY))
                {
                    validMazeNodes.Add(MazeNodeMatrix[curX + 1, curY]);
                }
                
            }
            if (EvaluateNode(curX, curY - 2) && EvaluateNode(curX, curY - 1))
            {
                if (EvaluateNeighbors(curX, curY - 1))
                {
                    validMazeNodes.Add(MazeNodeMatrix[curX, curY - 1]);
                }
               
            }
            if (EvaluateNode(curX, curY + 2) && EvaluateNode(curX, curY + 1))
            {
                if (EvaluateNeighbors(curX, curY + 1))
                {
                    validMazeNodes.Add(MazeNodeMatrix[curX, curY + 1]);
                }
                
            }

            Random random = new Random();
            if (validMazeNodes.Count == 0)
            {
                return null;
            }
            var index = random.Next(0, validMazeNodes.Count);

            pos[0] = validMazeNodes[index].X;
            pos[1] = validMazeNodes[index].Y;
            return pos;
        }

        public bool EvaluateNode(int posX, int posY)
        {
            if ((posX >= 0 && posX < size_) && (posY >= 0 && posY < size_) )
            {
                return true;
                /*
                if (MazeNodeMatrix[posX, posY].Type == MazeNodeType.BLOCKED)
                {
                    return true;
                }
                else if (MazeNodeMatrix[posX, posY].Type == MazeNodeType.WALKABLE)
                {
                    return true;
                }
                */
            }
            
            return false;
        }

        public bool EvaluateNeighbors(int posX, int posY)
        {
            int clear = 0;
            if ((posX >= 0 && posX < size_) && (posY >= 0 && posY < size_))
            {
                if (posX - 1 >= 0 && MazeNodeMatrix[posX - 1, posY].Type == MazeNodeType.WALKABLE)
                {
                    if (posY - 1 >= 0 && MazeNodeMatrix[posX - 1, posY - 1].Type == MazeNodeType.BLOCKED
                        && posY + 1 < size_ && MazeNodeMatrix[posX - 1, posY + 1].Type == MazeNodeType.BLOCKED)
                    {
                        //clear++;
                    }
                    clear++;
                }
                if (posX + 1 < size_ && MazeNodeMatrix[posX + 1, posY].Type == MazeNodeType.WALKABLE)
                {
                    if (posY - 1 >= 0 && MazeNodeMatrix[posX + 1, posY - 1].Type == MazeNodeType.BLOCKED
                        && posY + 1 < size_ && MazeNodeMatrix[posX + 1, posY + 1].Type == MazeNodeType.BLOCKED)
                    {
                        //clear++;
                    }
                    clear++;
                }
                if (posY - 1 >= 0 && MazeNodeMatrix[posX, posY - 1].Type == MazeNodeType.WALKABLE)
                {
                    if (posX - 1 >= 0 && MazeNodeMatrix[posX - 1, posY - 1].Type == MazeNodeType.BLOCKED
                        && posX + 1 < size_ && MazeNodeMatrix[posX + 1, posY - 1].Type == MazeNodeType.BLOCKED)
                    {
                        //clear++;
                    }
                    clear++;
                }
                if (posY + 1 < size_ && MazeNodeMatrix[posX, posY + 1].Type == MazeNodeType.WALKABLE)
                {
                    if (posX - 1 >= 0 && MazeNodeMatrix[posX - 1, posY + 1].Type == MazeNodeType.BLOCKED
                       && posX + 1 < size_ && MazeNodeMatrix[posX + 1, posY + 1].Type == MazeNodeType.BLOCKED)
                    {
                        //clear++;
                    }
                    clear++;
                }
            }
            if (clear == 1)
            {
                return true;
            }


            return false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
