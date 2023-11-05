using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string file = "vstup.in";
        string mapSizeString = File.ReadLines(file).First();
        string secondLine = File.ReadLines(file).Skip(1).First();

        string[] dimensions = mapSizeString.Split(' ');
        int length = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);
        int[,] map = new int[length, height];

        Console.WriteLine("Pole o délce {0} a výšce {1} bylo vytvořeno.", length, height);

        string[] startCoordinates = secondLine.Split(' ');
        int Sx = int.Parse(startCoordinates[0]);
        int Sy = int.Parse(startCoordinates[1]);
        int Cx = int.Parse(startCoordinates[2]);
        int Cy = int.Parse(startCoordinates[3]);

        Console.WriteLine("Sx " + Sx + ", Sy: " + Sy);
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
            Console.WriteLine();
        }

        Console.WriteLine("Map filled with values.");

        List<(int, int)> longestPath = FindLongestPath(map, Sx, Sy, Cx, Cy);

        Console.WriteLine("The longest path length is: " + longestPath.Count);
        Console.WriteLine("Sequence of moves:");
        foreach (var position in longestPath)
        {
            Console.WriteLine("Move to: (" + position.Item1 + ", " + position.Item2 + ")");
        }

        Console.ReadKey();
    }

    static List<(int, int)> FindLongestPath(int[,] map, int Sx, int Sy, int Cx, int Cy)
    {
        List<(int, int)> longestPath = new List<(int, int)>();
        List<(int, int)> currentPath = new List<(int, int)>();

        FindPathUp(map, Sx, Sy, Cx, Cy, currentPath, ref longestPath, int.MinValue);
        return longestPath;
    }

    static void FindPathUp(int[,] map, int x, int y, int Cx, int Cy, List<(int, int)> currentPath, ref List<(int, int)> longestPath, int lastValue)
    {
        if (x == Cx && y == Cy)
        {
            if (currentPath.Count > longestPath.Count)
            {
                longestPath.Clear();
                longestPath.AddRange(currentPath);
            }
            return;
        }

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (newX >= 0 && newX < map.GetLength(0) && newY >= 0 && newY < map.GetLength(1) && map[newX, newY] != -1 && map[newX, newY] != lastValue && map[newX, newY] < map[x, y])
            {
                currentPath.Add((newX, newY));
                FindPathDown(map, newX, newY, Cx, Cy, currentPath, ref longestPath, map[x, y]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }

    static void FindPathDown(int[,] map, int x, int y, int Cx, int Cy, List<(int, int)> currentPath, ref List<(int, int)> longestPath, int lastValue)
    {
        if (x == Cx && y == Cy)
        {
            if (currentPath.Count > longestPath.Count)
            {
                longestPath.Clear();
                longestPath.AddRange(currentPath);
            }
            return;
        }

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (newX >= 0 && newX < map.GetLength(0) && newY >= 0 && newY < map.GetLength(1) && map[newX, newY] != -1 && map[newX, newY] != lastValue && map[newX, newY] > map[x, y])
            {
                currentPath.Add((newX, newY));
                FindPathUp(map, newX, newY, Cx, Cy, currentPath, ref longestPath, map[x, y]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }
}
