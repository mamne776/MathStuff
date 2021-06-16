﻿using System;
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
            int demoDelay = 100;

            //size of the grid
            int width = 120;
            int height = 50;

            if (demoing)
            {
                width = 120;
                height = 50;
            }

            Console.SetWindowSize(121, 51);
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

            int a = rand1.Next(x - 1);
            int b = rand1.Next(y - 1);

            //pick a cell, mark it as part of the maze
            //if you change these values, remember to change the neighbours too!
            Cell cell = new Cell(a, b);
            startMaze[a, b] = cell;

            //get the neighbours
            //top row doesnt have neighbours on top            
            if (b < y - 1)
            {
                startMaze[a, b].neighbours.Add(startMaze[a, b + 1]);
            }
            //bottom row has no neighbours under
            if (b > 0)
            {
                startMaze[a, b].neighbours.Add(startMaze[a, b - 1]);
            }
            //left column has no neighbours on the left
            if (a > 0)
            {
                startMaze[a, b].neighbours.Add(startMaze[a - 1, b]);
            }
            //right column ha no neighbours to the right
            if (a < x - 1)
            {
                startMaze[a, b].neighbours.Add(startMaze[a + 1, b]);
            }
            for (int i = 0; i < startMaze[a,b].neighbours.Count(); i++)
            {
                wallList.Add(startMaze[a, b].neighbours[i]);

            }
            //add the walls of the cell to the wall list
            //wallList.Add((WallCell)startMaze[0, 1]);
            //wallList.Add((WallCell)startMaze[1, 0]);

            //while there are walls on the list
            while (wallList.Count > 0)
            {
                //Pick a random wall from the list.
                var random = new Random();
                int index = random.Next(wallList.Count);
                Cell randWall = wallList[index];

                int rwx = randWall.x;
                int rwy = randWall.y;

                //for highlighting
                randWall.isHighLighted = true;

                //get the neighbours
                //top row doesnt have neighbours on top
                if (rwy < y - 1)
                {
                    randWall.neighbours.Add(startMaze[rwx, rwy + 1]);
                }
                //bottom row has no neighbours under
                if (rwy > 0)
                {
                    randWall.neighbours.Add(startMaze[rwx, rwy - 1]);
                }
                //left column has no neighbours on the left
                if (rwx > 0)
                {
                    randWall.neighbours.Add(startMaze[rwx - 1, rwy]);
                }
                //right column ha no neighbours to the right
                if (rwx < x - 1)
                {
                    randWall.neighbours.Add(startMaze[rwx + 1, rwy]);
                }

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
                }

                //Make the wall a passage and mark the unvisited cell as part of the maze.
                if (lbn == 1)
                {
                    //Add the neighboring walls of the cell to the wall list
                    int nC = startMaze[rwx, rwy].neighbours.Count();
                    for (int i = 0; i < nC; i++)
                    {
                        //if not part of the labyrinth
                        if (!startMaze[rwx, rwy].neighbours[i].isLabyrinth)
                        {
                            //add to the list of walls
                            wallList.Add(startMaze[rwx, rwy].neighbours[i]);
                        }
                    }

                    //Replace the Wall with a Cell 
                    startMaze[rwx, rwy] = new Cell(rwx, rwy);
                    //remove the wall from the wall list
                    wallList.RemoveAt(index);
                    //highlighting
                    startMaze[rwx, rwy].isHighLighted = true;
                }

                PrintGrid(startMaze, false, demoing);
                if (demoing) { System.Threading.Thread.Sleep(demoDelay); }
            }
            return startMaze;
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
        //for highlighting
        public bool isHighLighted;

        public Cell(int a, int b)
        {
            x = a;
            y = b;
            typeNro = 0;
            isLabyrinth = true;
            neighbours = new List<Cell>();
            isHighLighted = false;
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