using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Algorithms
{
    class Dijkstra : Algorithm
    {

        public Dijkstra(Maze maze) : base(maze)
        {
        }

        public override List<MazeNode> SearchAlgo()
        {
            List<MazeNode> listOfVisitedNodes = new List<MazeNode>();
            Queue<MazeNode> queueOfUnvisitedNodes = new Queue<MazeNode>();
            List<MazeNode> pathToEndNode = new List<MazeNode>();

            maze.StartNode.Distance = 0;

            queueOfUnvisitedNodes.Enqueue(maze.StartNode);
            maze.StartNode.Type = MazeNodeType.VISITED;

            while (queueOfUnvisitedNodes.Count != 0)
            {
                var curNode = queueOfUnvisitedNodes.Dequeue();
                listOfVisitedNodes.Add(curNode);

                if (curNode == maze.EndNode)
                {
                    while (curNode != null)
                    {
                        pathToEndNode.Add(curNode);
                        curNode.Type = MazeNodeType.PATH;
                        curNode = curNode.PrevNode;

                    }
                    return pathToEndNode;
                }


                MazeNode nextNode = CheckNeighbors(curNode);
                if (nextNode != null && !listOfVisitedNodes.Contains(nextNode))
                {
                    if (!queueOfUnvisitedNodes.Contains(nextNode))
                    {
                        nextNode.Distance = curNode.Distance + 1;
                        nextNode.PrevNode = curNode;
                        nextNode.Type = MazeNodeType.VISITED;
                        queueOfUnvisitedNodes.Enqueue(nextNode);
                    }
                    else
                    {
                        if (nextNode.Distance > curNode.Distance + 1)
                        {

                            nextNode.Distance = curNode.Distance + 1;
                            nextNode.PrevNode = curNode;

                            if (!queueOfUnvisitedNodes.Contains(nextNode))
                            {
                                nextNode.Type = MazeNodeType.VISITED;
                                queueOfUnvisitedNodes.Enqueue(nextNode);
                            }
                            

                        }
                    }
                }
                else if (nextNode == null)
                {
                    MazeNode lastNode = null;
                    int num = 1;
                    do
                    {
                        if ((listOfVisitedNodes.Count - num) < 0 )
                        {
                            break;
                        }
                        lastNode = listOfVisitedNodes[listOfVisitedNodes.Count - num];
                        num++;
                    } while (CheckNeighbors(lastNode) == null);
                    queueOfUnvisitedNodes.Enqueue(lastNode);


                }

            }

            return null;
        }


        public MazeNode CheckNeighbors(MazeNode node)
        {
            List<MazeNode> listOfWNodes = new List<MazeNode>();

            if (node.X - 1 >= 0 && maze.MazeNodeMatrix[node.X - 1, node.Y].Type == MazeNodeType.WALKABLE)
            {
                listOfWNodes.Add(maze.MazeNodeMatrix[node.X - 1, node.Y]);
            }
            if (node.X + 1 < Maze.size_ && maze.MazeNodeMatrix[node.X + 1, node.Y].Type == MazeNodeType.WALKABLE)
            {
                listOfWNodes.Add(maze.MazeNodeMatrix[node.X + 1, node.Y]);
            }
            if (node.Y - 1 >= 0 && maze.MazeNodeMatrix[node.X, node.Y - 1].Type == MazeNodeType.WALKABLE)
            {
                listOfWNodes.Add(maze.MazeNodeMatrix[node.X, node.Y - 1]);
            }
            if (node.Y + 1 < Maze.size_ && maze.MazeNodeMatrix[node.X, node.Y + 1].Type == MazeNodeType.WALKABLE)
            {
                listOfWNodes.Add(maze.MazeNodeMatrix[node.X, node.Y + 1]);
            }

            Random random = new Random();
            if (listOfWNodes.Count == 0)
            {
                return null;
            }
            var index = random.Next(0, listOfWNodes.Count);

            return listOfWNodes[index];

        }

    }


}
