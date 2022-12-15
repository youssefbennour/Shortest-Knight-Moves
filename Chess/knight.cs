using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Chess {
    class Knight {
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
                    this.boardMatrix[i, j] = "*";
                }
            }
            this.boardMatrix[this.limite - 1, 0] = "K";
            this.boardMatrix[this.limite - this.targetArea.y - 1, this.targetArea.x] = "T";
        }

        public void displayBoard() {

            Console.Clear();
            for (int i = 0; i < this.limite; i++) {
                for (int j = 0; j < this.limite; j++) {
                    if (boardMatrix[i, j] == "T") {
                        Console.ForegroundColor = ConsoleColor.Red;

                    } else if (boardMatrix[i, j] != "*") {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    } else {

                    }
                    Console.Write($" {boardMatrix[i, j]}");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

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