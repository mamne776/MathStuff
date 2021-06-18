using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                width = 12;
                height = 5;
            }

            Console.SetWindowSize(121, 51);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;

            //Builds the Maze
            Cell[,] grid = MazeBuilder(width, height, demoing, demoDelay);

            //Cleaner printing
            Console.SetCursorPosition(0, 0);
            PrintGrid(grid, true);

            //Without this, the console application sometimes exits without waiting for user input, ie. "Press any key to continue"
            //It's because there's a bug in Visual Studio, not in the code.
            Console.Write("Valmis!");
            Console.ReadLine();
        }

        //Randomized Prim's algorithm for generating a maze:
        /*
        Start with a grid full of walls.
        Pick a cell, mark it as part of the maze.Add the walls of the cell to the wall list.
        While there are walls in the list:
        Pick a random wall from the list.If only one of the cells that the wall divides is visited, then:
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
            var ranGen = new Random();
            //List of walls
            List<WallCell> wallList = new List<WallCell>();
            //grid of walls
            Cell[,] labyrinth = WallGrid(x, y);

            //pick a cell, mark it as part of the maze (automatically set as part of maze when instantiated)
            int a = ranGen.Next(0, x);
            int b = ranGen.Next(0, y);
            Cell cell = new Cell(a, b);
            labyrinth[a, b] = cell;
            cell.isFinal = true;

            //get the neighbours
            //top row doesnt have neighbours on top            
            if (b < y - 1)
            {
                cell.neighbours.Add(labyrinth[a, b + 1]);
            }
            //bottom row has no neighbours under
            if (b > 0)
            {
                cell.neighbours.Add(labyrinth[a, b - 1]);
            }
            //left column has no neighbours on the left
            if (a > 0)
            {
                cell.neighbours.Add(labyrinth[a - 1, b]);
            }
            //right column has no neighbours to the right
            if (a < x - 1)
            {
                cell.neighbours.Add(labyrinth[a + 1, b]);
            }

            for (int i = 0; i < labyrinth[a, b].neighbours.Count(); i++)
            {
                //if the cell isn't already a part of the labyrinth,
                //and it has not been added to the walllist yet
                if (!cell.neighbours[i].isLabyrinth && !cell.visited)
                {
                    wallList.Add((WallCell)cell.neighbours[i]);
                }
            }
            cell.visited = true;

            //while there are walls on the list
            while (wallList.Count > 0)
            {
                //Pick a random wall from the list.
                var random = new Random();
                int index = random.Next(wallList.Count);
                WallCell randWall = wallList[index];

                int rwx = randWall.x;
                int rwy = randWall.y;

                //for highlighting
                randWall.isHighLighted = true;

                //get the neighbours
                //bottom row has no neighbours under
                if (rwy > 0)
                {
                    randWall.neighbours.Add(labyrinth[rwx, rwy - 1]);
                }
                //top row doesnt have neighbours on top
                if (rwy < y - 1)
                {
                    randWall.neighbours.Add(labyrinth[rwx, rwy + 1]);
                }
                //left column has no neighbours on the left
                if (rwx > 0)
                {
                    randWall.neighbours.Add(labyrinth[rwx - 1, rwy]);
                }
                //right column ha no neighbours to the right
                if (rwx < x - 1)
                {
                    randWall.neighbours.Add(labyrinth[rwx + 1, rwy]);
                }

                //If only one of the two cells that the wall divides is visited, then:
                //Make the wall a passage and mark the unvisited cell as part of the maze.

                //Amount of neighbours that are part of the labyrinth:
                int nNeighbours = 0;
                for (int i = 0; i < randWall.neighbours.Count(); i++)
                {
                    if (randWall.neighbours[i].isLabyrinth)
                    {
                        nNeighbours++;
                    }
                }

                if (nNeighbours > 1)
                {
                    //too many neighbouring labyrinth cells, remove the wall from the list
                    wallList.RemoveAt(index);
                    //this wallblock has been checked
                    randWall.visited = true;
                    randWall.isFinal = true;
                }

                //Make the wall a passage and mark the unvisited cell as part of the maze.
                if (nNeighbours == 1)
                {
                    //Add the neighboring walls of the new cell to the wall list
                    int nNeighbour = randWall.neighbours.Count();
                    for (int i = 0; i < nNeighbour; i++)
                    {
                        //if the neighbours are not part of the labyrinth, and the neighbours havent been checked yet
                        if (!randWall.neighbours[i].isLabyrinth)
                        {
                            if (!randWall.neighbours[i].visited)
                            {
                                //add to the list of walls
                                wallList.Add((WallCell)randWall.neighbours[i]);
                                randWall.neighbours[i].visited = true;
                                
                            }
                        }
                    }

                    //create a new cell that is part of the labyrinth here
                    labyrinth[rwx, rwy] = new Cell(rwx, rwy);
                    labyrinth[rwx, rwy].isFinal = true;

                    //remove the wall from the wall list
                    wallList.RemoveAt(index);                     

                    //highlighting
                    labyrinth[rwx, rwy].isHighLighted = true;
                }

                //for showing the progress
                PrintGrid(labyrinth, demoing);
                if (demoing) { System.Threading.Thread.Sleep(demoDelay); }
            }
            return labyrinth;
        }

        //Print the grid to console, with StringBuilder prints it all at once
        public static int PrintGrid(Cell[,] grid, bool isDemoing)
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
                    else if (grid[i, j].typeNro == 1 && grid[i, j].visited) { sb.Append("\u2593"); }
                    //full block
                    else if (grid[i, j].typeNro == 1) { sb.Append("\u2588"); }
                    //space
                    else if (grid[i, j].typeNro == 0) { sb.Append(" "); };
                }
                sb.AppendLine();
            }
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
        //List of the neighbouring cells
        public List<Cell> neighbours;
        //has the cell been through the algorithm yet
        public bool visited;
        //for highlighting
        public bool isHighLighted;
        //
        public bool isFinal;

        public Cell(int a, int b)
        {
            x = a;
            y = b;
            typeNro = 0;
            isLabyrinth = true;
            neighbours = new List<Cell>();
            visited = false;
            isHighLighted = false;
            bool isFinal = false;
        }
    }
    public class WallCell : Cell
    {
        public WallCell(int a, int b) : base(a, b)
        {
            typeNro = 1;
            isLabyrinth = false;
        }
    }
}
