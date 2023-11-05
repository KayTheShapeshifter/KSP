using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Drawing;

class Program
{
    static int stepsIntMax = 0;
    static int[] dx = { -1, 1, 0, 0 };
    static int[] dy = { 0, 0, -1, 1 };
    static List<(int, int)> pathMax = new List<(int, int)>();
    static void FindLongestPathDown(int[,]map, int Sx, int Sy, int stepsInt,List<(int,int)> path, List<(int,int)> prevPath,int Cx,int Cy)
    {
       
        bool hasLowerValueNeighbor = false;

        List<(int, int)> currentPath = new List<(int, int)>();
        if (prevPath.Count > 0)
        {
            foreach (var element in prevPath)
            {
                currentPath.Add(element);
            }
        }
        currentPath.Add((Sx, Sy));

        for (int i = 0; i < 4; i++)
        {
            int newX = Sx + dx[i];
            int newY = Sy + dy[i];
            if (newX >= 0 && newY >= 0 && newX < map.GetLength(0) && newY < map.GetLength(1))
            {
                if (Cx==Sx && Cy==Sy)
                {
                    if (stepsInt > stepsIntMax)
                    {
                        pathMax = currentPath;
                        stepsIntMax = stepsInt;
                    }// Store the entire path
                    
                    return;
                }
                int newValue = map[newY, newX];
                int oldValue = map[Sy, Sx];
                if (newValue < oldValue)
                {
                    hasLowerValueNeighbor = true;
                    // Add the current coordinates to the current path

                    FindLongestPathDown(map, newX, newY, stepsInt + 1, path, currentPath, Cx, Cy);
                    if(stepsInt+1>map.Length)
                    {
                        Console.WriteLine("Something is fucky");
                    }
                }
            }
        }
        if (hasLowerValueNeighbor !=true)
        {
            return;
        }

    }
    static void FindLongestPath(int[,] map, int Sx, int Sy, List<(int, int)> endpoints, Dictionary<(int, int), int> steps, int stepsInt, Dictionary<(int, int), List<(int, int)>> stepCoordinates, List<(int, int)> prevPath)
    {
        
        bool hasHigherValueNeighbor = false;

        List<(int, int)> currentPath = new List<(int, int)>();
        if (prevPath.Count > 0) {
            foreach (var element in prevPath) {
                currentPath.Add(element);
            }
        }
        currentPath.Add((Sx, Sy));
        for (int i = 0; i < 4; i++)
        {
            int newX = Sx + dx[i];
            int newY = Sy + dy[i];
            if (newX >= 0 && newY >= 0 && newX < map.GetLength(0) && newY < map.GetLength(1))
            {
                int newValue = map[newY, newX];
                int oldValue = map[Sy, Sx];
                if (newValue > oldValue)
                {
                    hasHigherValueNeighbor = true;
                    // Add the current coordinates to the current path
                    
                    FindLongestPath(map, newX, newY, endpoints, steps, stepsInt + 1, stepCoordinates, currentPath);
                }
            }
        }
        if (!hasHigherValueNeighbor)
        {
            if (!endpoints.Contains((Sx, Sy)))
            {
                endpoints.Add((Sx, Sy));
                steps[(Sx, Sy)] = stepsInt;
                // Store the entire path to this endpoint in the dictionary
                stepCoordinates[(Sx, Sy)] = new List<(int, int)>(currentPath);
            }
            else if (steps[(Sx,Sy)] < stepsInt)
            {
                steps[(Sx, Sy)] = stepsInt;
                stepCoordinates[(Sx, Sy)] = new List<(int, int)>(currentPath);
            }
        }
    }
    static void Main()
    {
        string file = "vstup.in";
        // Čtení ze souboru
        string mapSizeString = File.ReadLines(file).First();
        string secondLine = File.ReadLines(file).Skip(1).First();
        

        string[] dimensions = mapSizeString.Split(' ');

        // Parsování délky a výšky
        int length = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);

        // Vytvoření pole o dané délce a výšce
        int[,] map = new int[length, height];

        // Zde můžete provést další operace s tímto polem
        Console.WriteLine("Pole o délce {0} a výšce {1} bylo vytvořeno.", length, height);

        string[] startCoordinates = secondLine.Split(' ');

        int Sx = int.Parse(startCoordinates[0]);
        int Sy = int.Parse(startCoordinates[1]);
        int Cx = int.Parse(startCoordinates[2]);
        int Cy = int.Parse(startCoordinates[3]);
        // Display Sx and Sy
        Console.WriteLine("Sx " + Sx + ", Sy: "+ Sy);
        // Display Cx and Cy
        Console.WriteLine("Cx: {0}, Cy: {1}", Cx, Cy);

        for (int i = 0; i < length; i++)
        {
                string tempValues = File.ReadLines(file).Skip(i + 2).First();
                string[] valuesString = tempValues.Split(' ');
                for (int j = 0; j < height; j++)
                {
                    int value = int.Parse(valuesString[j]);

                    map[i, j] = value;
                    Console.Write(map[i, j] + " ");
                }
                // Filling the map with the value at the specified coordinates
            Console.WriteLine();
        }
        Console.WriteLine("Map filled with values.");
        // Displaying the map

        List<(int, int)> endpoints = new List<(int, int)>();
        Dictionary<(int, int), int> steps = new Dictionary<(int, int), int>();
        Dictionary<(int, int), List<(int, int)>> stepCoordinates = new Dictionary<(int, int), List<(int, int)>>(); 
        List<(int, int)> prevPath = new List<(int, int)>();
        List<(int,int)> path = new List<(int, int)>();

        FindLongestPath(map, Sx, Sy, endpoints, steps, 0, stepCoordinates,prevPath);

        Console.WriteLine("Endpoint coordinates:");
        foreach (var endpoint in endpoints)
        {
            Console.WriteLine("Endpoint: (" + endpoint.Item1 + ", " + endpoint.Item2 + "), Number of steps: " + steps[endpoint]);
            foreach (var i in stepCoordinates[(endpoint.Item1, endpoint.Item2)])
            {
                Console.WriteLine(i.ToString());
            }
        }
        foreach (var endpoint in endpoints)
        {
            FindLongestPathDown(map, endpoint.Item1, endpoint.Item2, 0, path, prevPath, Cx, Cy);
        }
        for (int i = 0; i < pathMax.Count; i++)
        {
            Console.WriteLine(pathMax[i].Item1 + " "+ pathMax[i].Item2);
        }
        Console.WriteLine("Steps: "+ stepsIntMax);
    }
}
