using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Drawing;

/*
coordinates:         __|__+
                    -  |  
*/

//Creates a true labyrinth using Randomized Prim's algorithm for generating a maze.
//A true labyrinth only has one route (without backtracking) between each of its points.
namespace LabyrinthGenerator
{
    class LabGen
    {
        static void Main(string[] args)
        {
            bool demoing = true;
            int demoDelay = 1000;

            //size of the grid
            int width = 120;
            int height = 50;

            if (demoing)
            {
                width = 120;
                height = 60;
            }

            Console.SetWindowSize(125, 70);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;

            //Builds the Maze
            Cell[,] grid = MazeBuilder(width, height, demoing, demoDelay);

            Console.SetCursorPosition(0, 0);
            PrintGrid(grid, true, true);

        }

        //Randomized Prim's algorithm for generating a maze:
        /*
        Start with a grid full of walls.
        Pick a cell, mark it as part of the maze.Add the walls of the cell to the wall list.
        While there are walls in the list:
        Pick a random wall from the list.If only one of the two cells that the wall divides is visited, then:
        Make the wall a passage and mark the unvisited cell as part of the maze.
        Add the neighboring walls of the cell to the wall list.
        Remove the wall from the list.
        */

        //grid full of walls
        public static Cell[,] WallGrid(int x, int y)
        {
            Cell[,] wallGrid = new Cell[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    wallGrid[i, j] = new WallCell(i, j);
                }
            }
            return wallGrid;
        }

        //Mazebuilder
        public static Cell[,] MazeBuilder(int x, int y, bool demoing, int demoDelay)
        {
            var rand1 = new Random();
            //List of walls
            List<Cell> wallList = new List<Cell>();
            //grid of walls
            Cell[,] startMaze = WallGrid(x, y);

            int a = rand1.Next(x);
            int b = rand1.Next(y);

            //pick a cell, mark it as part of the maze
            Cell cell = new Cell(a, b);
            startMaze[a, b] = cell;
            //cell.visited = true;

            //get the neighbours
            startMaze[a, b].neighbours = GetNeighbours(startMaze, startMaze[a, b], x, y);
            //add the neighbours that are walls to the wallList
            wallList.AddRange(startMaze[a, b].neighbours);

            /*
            //top row doesnt have neighbours on top            
            if (b < y - 1)
            {
                if (!startMaze[a, b + 1].visited)
                {
                    startMaze[a, b].neighbours.Add(startMaze[a, b + 1]);
                }
            }
            //bottom row has no neighbours under
            if (b > 0)
            {
                //add to neighbours if hasn't already been visited
                if (!startMaze[a, b - 1].visited)
                {
                    startMaze[a, b].neighbours.Add(startMaze[a, b - 1]);
                }
            }
            //left column has no neighbours on the left
            if (a > 0)
            {
                if (!startMaze[a - 1, b].visited)
                {
                    startMaze[a, b].neighbours.Add(startMaze[a - 1, b]);
                }
            }
            //right column ha no neighbours to the right
            if (a < x - 1)
            {
                if (!startMaze[a + 1, b].visited)
                {
                    startMaze[a, b].neighbours.Add(startMaze[a + 1, b]);
                }
            }
<<<<<<< Updated upstream
            
=======
>>>>>>> Stashed changes
            for (int i = 0; i < startMaze[a, b].neighbours.Count(); i++)
            {
                wallList.Add(startMaze[a, b].neighbours[i]);
            }
<<<<<<< Updated upstream
            */


=======
            //int count = startMaze[a, b].neighbours.Count();
>>>>>>> Stashed changes


            //while there are walls on the list
            while (wallList.Count > 0)
            {
                //Pick a random wall from the list.
                var random = new Random();
                int index = random.Next(wallList.Count);
                Cell randWall = wallList[index];

                int rwx = randWall.x;
                int rwy = randWall.y;

                randWall.visited = true;

                //for highlighting
                randWall.isHighLighted = true;

<<<<<<< Updated upstream
                //get the neighbours
                randWall.neighbours.AddRange(GetNeighbours(startMaze, startMaze[rwx, rwy], x, y));
                //add the walls that are neighbours to the list
                for (int i = 0; i < randWall.neighbours.Count(); i++)
                {
                    if (!randWall.neighbours[i].isLabyrinth)
                    {
                        wallList.Add(randWall.neighbours[i]);
                    }
                }

                /*
                //get the neighbours
=======
                //get the neighbouring walls
>>>>>>> Stashed changes
                //top row doesnt have neighbours on top
                if (rwy < y - 1)
                {
                    if (!startMaze[rwx, rwy + 1].visited)
                    {
                        randWall.neighbours.Add(startMaze[rwx, rwy + 1]);
                    }
                }
                //bottom row has no neighbours under
                if (rwy > 0)
                {
                    if (!startMaze[rwx, rwy - 1].visited)
                    {
                        randWall.neighbours.Add(startMaze[rwx, rwy - 1]);
                    }
                }
                //left column has no neighbours on the left
                if (rwx > 0)
                {
                    if (!startMaze[rwx - 1, rwy].visited)
                    {
                        randWall.neighbours.Add(startMaze[rwx - 1, rwy]);
                    }
                }
                //right column ha no neighbours to the right
                if (rwx < x - 1)
                {
                    if (!startMaze[rwx + 1, rwy].visited)
                    {
                        randWall.neighbours.Add(startMaze[rwx + 1, rwy]);
                    }
                }
                */


                //If only one of the two cells that the wall divides is visited, then:
                //Make the wall a passage and mark the unvisited cell as part of the maze.
                //labyrinth neighbours:
                int lbn = 0;
                for (int i = 0; i < randWall.neighbours.Count(); i++)
                {
                    if (randWall.neighbours[i].isLabyrinth)
                    {
                        lbn++;
                    }
                }

                if (lbn > 1)
                {
                    //too many labyrinth cells, remove the wall from the list
                    wallList.RemoveAt(index);
                    //mark as visited
                    //randWall.visited = true;
                }

                //Make the wall a passage and mark the unvisited cell as part of the maze.
                if (lbn == 1)
                {
                    //Add the neighboring walls of the cell to the wall list
                    int nC = randWall.neighbours.Count();
                    for (int i = 0; i < nC; i++)
                    {
                        //if not part of the labyrinth
                        if (!randWall.neighbours[i].isLabyrinth && !randWall.visited)
                        {
                            //add to the list of walls
<<<<<<< Updated upstream
                            wallList.Add(startMaze[rwx, rwy].neighbours[i]);
                            //Replace the Wall with a Cell 
                            startMaze[rwx, rwy] = new Cell(rwx, rwy);                           
=======
                            wallList.Add(randWall.neighbours[i]);
>>>>>>> Stashed changes
                        }
                    }
                    //remove the wall from the wall list 
                    wallList.RemoveAt(index);
                    //mark as visited
                    //startMaze[rwx, rwy].visited = true;
                    //highlighting
                    startMaze[rwx, rwy].isHighLighted = true;


                }

                PrintGrid(startMaze, false, demoing);
                if (demoing) { System.Threading.Thread.Sleep(demoDelay); }
            }
            return startMaze;
        }

        //returns a list of neighbours the given cell has in a grid of given dimensions
        public static List<Cell> GetNeighbours(Cell[,] cells, Cell c, int xMax, int yMax)
        {
            //neighbourList
            List<Cell> rl = new List<Cell>();

            //get the neighbours
            //top row doesnt have neighbours on top            
            if (c.y < yMax - 1)
            {
                rl.Add(cells[c.x, c.y + 1]);
            }
            //bottom row has no neighbours under
            if (c.y > 0)
            {
                rl.Add(cells[c.x, c.y - 1]);
            }
            //left column has no neighbours on the left
            if (c.x > 0)
            {
                rl.Add(cells[c.x - 1, c.y]);
            }
            //right column ha no neighbours to the right
            if (c.x < xMax - 1)
            {
                rl.Add(cells[c.x + 1, c.y]);
            }

            /*
            //add the walls to the list
            for (int i = 0; i < c.neighbours.Count(); i++)
            {
                rl.Add(c.neighbours[i]);

            }
            */

            return rl;
        }

        //Print the grid to console, with StringBuilder prints it all at once
        public static int PrintGrid(Cell[,] grid, bool isReady, bool isDemoing)
        {
            StringBuilder sb = new StringBuilder(grid.GetLength(0) * grid.GetLength(1));

            for (int j = grid.GetLength(1) - 1; j >= 0; j--)
            {
                for (int i = 0; i < (grid.GetLength(0)); i++)
                {
                    if (grid[i, j].isHighLighted)
                    {
                        //shaded block
                        sb.Append("\u2592");
                        grid[i, j].isHighLighted = false;
                    }
                    //full block
                    else if (grid[i, j].typeNro == 1) { sb.Append("\u2588"); }
                    //space
                    else if (grid[i, j].typeNro == 0) { sb.Append(" "); };
                }
                sb.AppendLine();
            }
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            if (isDemoing)
            {
                Console.Write(sb);
            }
            return 0;
        }
    }

    public class Cell
    {
        public int typeNro;
        //coordinates, start from 0
        public int x, y;
        //is part of the labyrinth?
        public bool isLabyrinth;
        //List of the cells neighbours
        public List<Cell> neighbours;
        //has the cell already been visited (checked by the algorithm)
        public bool visited;
        //for highlighting
        public bool isHighLighted;

        public Cell(int a, int b)
        {
            x = a;
            y = b;
            typeNro = 0;
            isLabyrinth = true;
            neighbours = new List<Cell>();
            visited = true;
            isHighLighted = false;
        }
    }
    public class WallCell : Cell
    {
        public WallCell(int a, int b) : base(a, b)
        {
            typeNro = 1;
            isLabyrinth = false;
            visited = false;
        }
    }
}
