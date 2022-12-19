using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;

namespace Chess {
    public class Knight {
        public int x, y;
        public int dis;
        public List<int> knightMoves;

        public Knight(int x, int y, int dis, List<int> t) {
            this.x = x;
            this.y = y;
            this.dis = dis;
            this.knightMoves = t;
        }
    }
    public class  Game {
        private static bool isInside(int x, int y, int N) {
            if (x >= 0 && x <= N && y >= 0 && y <= N)
                return true;
            return false;
        }

        // Method returns minimum step
        // to reach target position
        public static List<int> minStepToReachTarget(int[] knightPos,
                                        int[] targetPos, int N) {
            // x and y direction, where a knight can move
            int[] dx = { -2, -1, 1, 2, -2, -1, 1, 2 };
            int[] dy = { -1, -2, -2, -1, 1, 2, 2, 1 };

            // queue for storing states of knight in board
            Queue<Knight> q = new Queue<Knight>();

            // push starting position of knight with 0 distance


            q.Enqueue(new Knight(0, 0, 0, new List<int>()));

            Knight currentKnight;
            int x, y;
            List<int> tempList = new List<int>();
            bool[,] visit = new bool[N + 1, N + 1];

            for (int i = 1; i <= N; i++)
                for (int j = 1; j <= N; j++)
                    visit[i, j] = false;

            visit[knightPos[0], knightPos[1]] = true;

            while (q.Count != 0) {
                currentKnight = q.Peek();
                q.Dequeue();

                if (currentKnight.x == targetPos[0] && currentKnight.y == targetPos[1]) {
                    Console.WriteLine("minimum number of steps to reach target : " + currentKnight.dis);
                    Console.WriteLine("a detailed image containing the shortest path can be found in Your Desktop/Shortest-Path directory.\nThe process may take a while for large input...");
                    q.Clear();
                    return currentKnight.knightMoves;
                }

                for (int i = 0; i < 8; i++) {
                    tempList = new List<int>(currentKnight.knightMoves);

                    x = currentKnight.x + dx[i];
                    y = currentKnight.y + dy[i];

                    if (isInside(x, y, N) && !visit[x, y]) {
                        visit[x, y] = true;
                        tempList.Add(dx[i]);
                        tempList.Add(dy[i]);
                        q.Enqueue(new Knight(x, y, currentKnight.dis + 1, tempList));
                    }
                }
            }
            return new List<int>();
        }

        public static void convertOutputToImage(string result, string filename = "Shortest-Path") {
            Color fontColor = Color.Black;
            Color bgColor = Color.White;

            // Set the font and size
            Font font = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Pixel);

            // Set the text to be rendered

            // Measure the size of the text
            SizeF textSize = MeasureString(result, font);

            // Create a Bitmap to hold the text
            Bitmap bmp = new Bitmap((int)textSize.Width, (int)textSize.Height);

            // Create a Graphics object for drawing the text
            Graphics graphics = Graphics.FromImage(bmp);

            // Clear the Bitmap with the background color
            graphics.Clear(bgColor);

            // Create a Brush to draw the text with
            Brush brush = new SolidBrush(fontColor);

            // Draw the text onto the Bitmap
            graphics.DrawString(result, font, brush, 0, 0);

            // Create a directory for storing Knight path images in Desktop
            string imageOutput = @"C:\Users\Taki Academy\Desktop\Knight-Path-Images";
            DateTime currentTime = DateTime.Now;
            if (!Directory.Exists(imageOutput)){
                Directory.CreateDirectory(imageOutput);
            }

            // Save the Bitmap to a file with 
            bmp.Save($"{imageOutput}\\{currentTime.ToString("yyyy-MM-dd(HH-mm-ss)")}.png", ImageFormat.Png);

            // Dispose of the Bitmap and Graphics objects
            bmp.Dispose();
            graphics.Dispose();
        }

        private static SizeF MeasureString(string text, Font font) {
            // Create a Graphics object for measuring the text
            using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1))) {
                // Measure the size of the text
                return graphics.MeasureString(text, font);
            }
        }


    }

}