using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void FindLongestPathToTarget(int[,] map, int Cx, int Cy, List<(int, int)> endpoints)
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        Dictionary<(int, int), List<(int, int)>> stepCoordinates = new Dictionary<(int, int), List<(int, int)>();

        List<(int, int)> longestPath = new List<(int, int)>();

        foreach (var endpoint in endpoints)
        {
            int Sx = endpoint.Item1;
            int Sy = endpoint.Item2;

            List<(int, int)> currentPath = new List<(int, int)>();
            int stepsInt = 0;
            bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)];

            FindLongestPathRecursive(map, Sx, Sy, Cx, Cy, stepsInt, stepCoordinates, currentPath, longestPath, visited);
        }

        Console.WriteLine("Longest Path to Target:");
        foreach (var point in longestPath)
        {
            Console.WriteLine($"({point.Item1}, {point.Item2})");
        }
    }

    static void FindLongestPathRecursive(int[,] map, int Sx, int Sy, int Cx, int Cy, int stepsInt,
        Dictionary<(int, int), List<(int, int)>> stepCoordinates, List<(int, int)> currentPath, List<(int, int)> longestPath, bool[,] visited)
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        visited[Sy, Sx] = true;
        currentPath.Add((Sx, Sy));

        if (Sx == Cx && Sy == Cy)
        {
            if (stepsInt > stepCoordinates.Count)
            {
                longestPath.Clear();
                longestPath.AddRange(currentPath);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            int newX = Sx + dx[i];
            int newY = Sy + dy[i];
            if (newX >= 0 && newY >= 0 && newX < map.GetLength(0) && newY < map.GetLength(1) && !visited[newY, newX])
            {
                int newValue = map[newY, newX];
                int oldValue = map[Sy, Sx];
                if (newValue < oldValue)
                {
                    FindLongestPathRecursive(map, newX, newY, Cx, Cy, stepsInt + 1, stepCoordinates, currentPath, longestPath, visited);
                }
            }
        }

        visited[Sy, Sx] = false;
        currentPath.RemoveAt(currentPath.Count - 1);
    }

    static void Main()
    {
        string file = "vstup.in";
        // Read from the file
        string mapSizeString = File.ReadLines(file).First();
        string secondLine = File.ReadLines(file).Skip(1).First();

        string[] dimensions = mapSizeString.Split(' ');

        int length = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);

        int[,] map = new int[length, height];

        string[] startCoordinates = secondLine.Split(' ');

        int Sx = int.Parse(startCoordinates[0]);
        int Sy = int.Parse(startCoordinates[1]);
        int Cx = int.Parse(startCoordinates[2]);
        int Cy = int.Parse(startCoordinates[3]);

        for (int i = 0; i < length; i++)
        {
            string tempValues = File.ReadLines(file).Skip(i + 2).First();
            string[] valuesString = tempValues.Split(' ');
            for (int j = 0; j < height; j++)
            {
                int value = int.Parse(valuesString[j]);
                map[i, j] = value;
            }
        }

        List<(int, int)> endpoints = new List<(int, int)>();
        Dictionary<(int, int), int> steps = new Dictionary<(int, int), int>();
        Dictionary<(int, int), List<(int, int)>> stepCoordinates = new Dictionary<(int, int), List<(int, int)>();
        List<(int, int)> prevPath = new List<(int, int)>();

        FindLongestPath(map, Sx, Sy, endpoints, steps, 0, stepCoordinates, prevPath);
        FindLongestPathToTarget(map, Cx, Cy, endpoints);
    }
}
