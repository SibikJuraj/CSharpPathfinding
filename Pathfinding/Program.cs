using System;


namespace Pathfinding
{
    class Program
    {
        static void Main(string[] args)
        {

            Maze maze = new Maze();

            maze.DrawMaze();
            
            Console.WriteLine();

            Pathfinding.Algorithms.Dijkstra dijkstra = new Pathfinding.Algorithms.Dijkstra(maze.Clone() as Maze);

            dijkstra.SearchAlgo();
            dijkstra.maze.DrawMaze();

            //Console.BackgroundColor = ConsoleColor.DarkYellow;
            //Console.BackgroundColor = ConsoleColor.Green;


        }
    }
}
