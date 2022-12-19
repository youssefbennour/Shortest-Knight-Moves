// See https://aka.ms/new-console-template for more information
using Chess;
using System.Drawing;
using System.Net.Http;



int[] knightPos = new int[2];
int[] targetPos = new int[2];
do {
    Console.WriteLine("Enter target x-coordinate (starts from 0) : ");
    int.TryParse(Console.ReadLine(), out targetPos[0]);
} while (targetPos[0] < 0);
do {
    Console.WriteLine("Enter target y-coordinate (starts from 0) : ");
    int.TryParse(Console.ReadLine(), out targetPos[1]);
} while (targetPos[1] < 0);

//determine grid size
int N = Math.Max(targetPos[0], targetPos[1]) + 1;


List<int> knightMoves;

//store knight moves to the target in the following form {xCord, yCord, xCord, yCord,...}
knightMoves = Game.minStepToReachTarget(knightPos, targetPos, N);

string[,] board = new string[N, N];

for (int i = 0; i < N; i++) {
    for (int j = 0; j < N; j++) {
        board[i, j] = " *";
    }
}

//The result contains the final path printed and step enumerated from (0,0) to (target X coordinate, target Y coordinate) 
string result = "";

//inital knight cooridnates
int x, y;
x = 0;
y = 0;

int counter = 1;
for (int i = 0; i < knightMoves.Count; i += 2) {
    x += knightMoves[i];
    y += knightMoves[i + 1];
    board[N - y - 1, x] = counter.ToString();
    
    counter++;
}
board[N-1, 0] = " K";

for (int i = 0; i < N; i++) {
    for (int j = 0; j < N; j++) {
        result += board[i, j];

    }
    result += "\n";
}

Game.convertOutputToImage(result);



