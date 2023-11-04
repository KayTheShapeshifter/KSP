using System;
using System.IO;
using System.Collections.Generic;

class Program
{

    static void FindLongestPath(int[,] map, int Sx, int Sy, int Cx, int Cy, List<(int, int)> currentPath, ref List<(int, int)> longestPath)
    {
        // If the current position matches the ending coordinates, update the longest path
        if (Sx == Cx && Sy == Cy)
        {
            if (currentPath.Count > longestPath.Count)
            {
                longestPath.Clear();
                longestPath.AddRange(currentPath);
            }
            return; // Terminate the recursion
        }

        // Define possible neighbor positions (up, down, left, right)
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        foreach (int i in Enumerable.Range(0, 4))
        {
            int newX = Sx + dx[i];
            int newY = Sy + dy[i];

            // Check if the neighbor is within the map boundaries
            if (newX >= 0 && newX < map.GetLength(0) && newY >= 0 && newY < map.GetLength(1))
            {
                // Check if the neighbor has a higher value
                if (map[newX, newY] > map[Sx, Sy])
                {
                    // Move to the neighbor with the higher value
                    currentPath.Add((newX, newY));
                    FindLongestPath(map, newX, newY, Cx, Cy, currentPath, ref longestPath);
                    currentPath.RemoveAt(currentPath.Count - 1); // Remove the last position for backtracking
                }
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
        List<(int, int)> currentPath = new List<(int, int)>();
        List<(int, int)> longestPath = new List<(int, int)>();

        FindLongestPath(map, Sx, Sy, Cx, Cy, currentPath, ref longestPath);
        Console.WriteLine("The longest path length is: " + longestPath.Count);
        Console.WriteLine("Sequence of moves:");
        foreach (var position in longestPath)
        {
            Console.WriteLine("Move to: (" + position.Item1 + ", " + position.Item2 + ")");
        }
        Console.ReadKey();

    }
}
