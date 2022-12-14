// See https://aka.ms/new-console-template for more information
using Chess;

int xCoordinates, yCoordinates;
Console.Write("Enter Target X-axis coordinates : ");
int.TryParse(Console.ReadLine(), out xCoordinates);
Console.Write("\nEnter Target Y-axis coordinates : ");
int.TryParse(Console.ReadLine(), out yCoordinates);

Knight knight = new Knight(Math.Max(xCoordinates, yCoordinates)+5, xCoordinates, yCoordinates);
knight.displayBoard();
Thread.Sleep(1000);
Console.Clear();
knight.moves();
knight.displayBoard();
Console.ResetColor();
Console.ReadKey();