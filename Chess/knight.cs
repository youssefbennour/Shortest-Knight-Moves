using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;

namespace Chess {
    class  Knight {
        public (int x, int y) cord;
        public int limite;
        public string[,] boardMatrix;
        public (int x, int y) targetArea;
        int numberOfmoves, paths;

        public Knight(int lim, int cordX, int cordY) {
            this.targetArea = (cordX, cordY);
            this.limite = lim;
            this.boardMatrix = new string[limite + 2, limite + 2];
            this.cord.x = 0;
            this.cord.y = 0;
            this.numberOfmoves = 0;
            this.paths = 1;

            for (int i = 0; i < this.limite; i++) {
                for (int j = 0; j < this.limite; j++) {
                    this.boardMatrix[i, j] = "0";
                }
            }
            this.boardMatrix[this.limite - 1, 0] = "K";
            this.boardMatrix[this.limite - this.targetArea.y - 1, this.targetArea.x] = " T";
        }

        public void displayBoard() {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("C:\\Users\\Taki Academy\\Desktop\\Savedpic\\Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception d)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(d.Message);
                return;
            }
            Console.SetOut(writer);
            Console.Clear();
            for (int i = 0; i < this.limite; i++) {
                for (int j = 0; j < this.limite; j++) {
                    if (boardMatrix[i, j] == "T") {
                        Console.ForegroundColor = ConsoleColor.Red;

                    } else if (boardMatrix[i, j] != "0") {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    } else {

                    }
                    Console.Write($" {boardMatrix[i, j]}");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");

            var doc = new Aspose.Words.Document("C:\\Users\\Taki Academy\\Desktop\\Savedpic\\Redirect.txt");

            doc.Watermark.Remove();
           
            //Aspose.Words.Document extractedPage = doc.ExtractPages(0, 1);
            doc.Save("C:\\Users\\Taki Academy\\Desktop\\Savedpic\\Doc.bmp");
            


        }


        public void moves() {
            int numberOfUps = (int)(this.targetArea.y / 2);

            for (int i = 1; i <= numberOfUps; i++) {
                moveUp(2);
                if (this.cord.x < this.targetArea.x) {

                    moveRight(1);

                } else if (this.cord.x > 0) {
                    moveLeft(1);
                } else {
                    moveRight(1);
                }

            }
            checkCollision();

            if (this.targetArea.y != this.cord.y) {
                moveRight(2);
                moveUp(1);
            }
            checkCollision();

            int verticalDirection = 0;
            while (Math.Abs(this.cord.x - this.targetArea.x) > 1) {
                if (this.cord.x < this.targetArea.x) {
                    if (verticalDirection % 2 == 0) {
                        moveUp(1);
                        moveRight(2);
                        verticalDirection++;
                    } else {
                        moveDown(1);
                        moveRight(2);
                        verticalDirection++;
                    }
                } else {
                    if (verticalDirection % 2 == 0) {
                        moveUp(1);
                        moveLeft(2);
                        verticalDirection++;
                    } else {
                        moveDown(1);
                        moveLeft(2);
                        verticalDirection++;
                    }
                }
            }
            checkCollision();


            if ((this.cord.x + 1) == this.targetArea.x && this.cord.y == this.targetArea.y) {
                moveUp(2); moveRight(3); moveDown(1); moveLeft(2); moveDown(1);
                checkCollision();
            } else if (this.cord.x == (this.targetArea.x + 1) && this.cord.y == this.targetArea.y) {
                moveRight(2); moveUp(2); moveLeft(3); moveDown(2);
                checkCollision();
            } else if (this.cord.x == (this.targetArea.x + 1) && this.cord.y == (this.targetArea.y + 1)) {
                moveUp(2); moveRight(1); moveRight(2); moveDown(1); moveLeft(2); moveDown(2); moveLeft(2);
                checkCollision();
            } else if (this.cord.x == (this.targetArea.x - 1) && this.cord.y == (this.targetArea.y + 1)) {
                moveRight(2); moveUp(1); moveLeft(1); moveDown(2);
                checkCollision();
            } else if (this.cord.x == (this.targetArea.x + 1) && this.cord.y == (this.targetArea.y - 1)) {
                moveLeft(1); moveUp(2); moveLeft(2); moveDown(1);
                checkCollision();
            } else if ((this.cord.x + 1) == this.targetArea.x && this.cord.y == (this.targetArea.y - 1)) {
                moveRight(2); moveUp(1); moveRight(2); moveUp(2); moveLeft(3); moveDown(1);
                checkCollision();
            } else if (this.cord.x == this.targetArea.x && this.cord.y == (this.targetArea.y - 1)) {
                moveRight(2); moveUp(3); moveLeft(2); moveDown(2);
                checkCollision();
            } else if (this.cord.x == this.targetArea.x && this.cord.y == (this.targetArea.y + 1)) {
                moveUp(2); moveRight(2); moveDown(3); moveLeft(2);
                checkCollision();
            }

        }

        private void moveUp(int steps) {
            for (int ups = 0; ups < steps; ++ups) {
                this.cord.y++;
                this.numberOfmoves++;
                if (this.numberOfmoves % 3 == 0) {
                    this.boardMatrix[this.limite - this.cord.y - 1, this.cord.x] = $"{this.paths}";
                    this.paths++;
                }
                //this.boardMatrix[this.limite - this.cord.y - 1, this.cord.x] = "↑";
            }
        }



        private void moveDown(int steps) {
            for (int downs = 0; downs < steps; ++downs) {
                this.cord.y--;
                this.numberOfmoves++;

                if (this.numberOfmoves % 3 == 0) {
                    this.boardMatrix[this.limite - this.cord.y - 1, this.cord.x] = $"{this.paths}";
                    this.paths++;
                }

            }
        }

        private void moveLeft(int steps) {
            for (int lefts = 0; lefts < steps; ++lefts) {
                this.cord.x--;
                this.numberOfmoves++;

                if (this.numberOfmoves % 3 == 0) {
                    this.boardMatrix[this.limite - this.cord.y - 1, this.cord.x] = $"{this.paths}";
                    this.paths++;
                }

            }
        }

        private void moveRight(int steps) {
            for (int rights = 0; rights < steps; ++rights) {
                this.cord.x++;
                this.numberOfmoves++;

                if (this.numberOfmoves % 3 == 0) {
                    this.boardMatrix[this.limite - this.cord.y - 1, this.cord.x] = $"{this.paths}";
                    this.paths++;
                }

            }
        }

        private void checkCollision() {
            if (this.cord.x == this.targetArea.x && this.cord.y == this.targetArea.y) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tareget reached");
                displayBoard();
                Thread.Sleep(1000);
                Console.ResetColor();
                Environment.Exit(0);
            }
        }

    }

}