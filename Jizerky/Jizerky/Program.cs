using System;
using System.IO;

class Program
{
    static void Main()
    {
        string file = "vstup.in";
        string mapSizeString = File.ReadLines(file).First();
        string secondLine = File.ReadLines(file).Skip(1).First();


        // Čtení prvního řádku ze souboru

        // Rozdělení řádku na délku a výšku
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
                for (int j = 0; j < length; j++)
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

































        Console.ReadKey();

    }
}
