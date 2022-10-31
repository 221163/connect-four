using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;

namespace connectFour
{
    internal class Program
    {
        public static string centerBufferL = "                        ";
        public static string centerBufferR = "                        ";

        public class Board
        {
            public int width;
            public int height;
            public int player = 1;
            public bool gameEnd = false;
            public string identity = "x";
            string[,] gameBoard = null;

            public Board(int inputWidth, int inputHeight)
            {
                this.width = inputWidth;
                this.height = inputHeight;

                this.gameBoard = new string[this.width, this.height];

                for (int i = 0; i < this.width; i++)
                {
                    for (int j = 0; j < this.height; j++)
                    {
                        gameBoard[i, j] = " ";
                    }
                    ;
                }
                ;
            }

            public void placeToColumn(int column, Board currentBoard)
            {
                if (this.player == 1)
                {
                    this.identity = "x";
                }
                else
                {
                    this.identity = "O";
                }
                ;

                int floor = 0;
                bool placed = false;

                while (floor + 1 < this.height && !placed)
                {
                    if (this.gameBoard[column - 1, floor + 1] == " ")
                    {
                        this.gameBoard[column - 1, floor] = this.identity;
                        renderScreen(currentBoard);
                        Thread.Sleep(200);
                        this.gameBoard[column - 1, floor] = " ";
                        Console.Clear();
                    }
                    else
                    {
                        this.gameBoard[column - 1, floor] = this.identity;
                        placed = true;
                    }
                    ;

                    floor++;
                }
                ;

                if (this.gameBoard[column - 1, floor] == " " && !placed)
                {
                    this.gameBoard[column - 1, floor] = this.identity;
                }
                ;
            }

            public bool checkColumn(int column)
            {
                if (this.gameBoard[column - 1, 0] == " ")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public string drawSpot(int widthInt, int heightInt)
            {
                return this.gameBoard[widthInt, heightInt].ToString();
            }

            public string getRow(int row)
            {
                string final = "";

                for (int i = 0; i < this.width; i++)
                {
                    if (i + 2 > this.width)
                    {
                        final += " " + drawSpot(i, row);
                    }
                    else
                    {
                        final += " " + drawSpot(i, row) + " |";
                    }
                }
                ;

                return final;
            }

            public string getTop()
            {
                string final = "";

                for (int i = 0; i < this.width; i++)
                {
                    if (i + 2 > this.width)
                    {
                        final += " " + (i + 1);
                    }
                    else
                    {
                        final += " " + (i + 1) + " |";
                    }
                    ;
                }
                ;

                return final;
            }

            public void renderScreen(Board board)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(centerBufferL + board.getTop() + centerBufferR);

                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < board.height; i++)
                {
                    Console.WriteLine(centerBufferL + board.getRow(i) + centerBufferR);
                }
                ;
            }

            public bool checkForWin(Board board)
            {
                bool windCondition = false;

                for (int i = 0; i < board.width; i++)
                {
                    if (!windCondition)
                    {
                        for (int x = 0; x < board.height; x++)
                        {
                            if (!windCondition)
                            {
                                // Down Check
                                bool checkFor(string item)
                                {
                                    bool final = false;
                                    string currentCheck = "";
                                    int amount = 0;

                                    while (currentCheck != item) { }

                                    if (amount > 3)
                                    {
                                        final = true;
                                    }
                                    ;

                                    return final;
                                }
                                ;

                                checkFor("x");
                                checkFor("O");
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                return windCondition;
            }
        };

        static void Main(string[] args)
        {
            static void renderScreen(Board board)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(centerBufferL + board.getTop() + centerBufferR);

                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < board.height; i++)
                {
                    Console.WriteLine(centerBufferL + board.getRow(i) + centerBufferR);
                }
                ;

                Console.Write(
                    $"\n{centerBufferL}It is player: {board.player}'s turn.\n{centerBufferL}Choose a column : "
                );
            }
            ;

            static void runGame()
            {
                Console.WriteLine(centerBufferL + "Loading..." + centerBufferR);

                Board globalBoard = new Board(7, 6);

                string message = " ";

                while (!globalBoard.gameEnd)
                {
                    bool valid = false;
                    int input = 0;

                    while (!valid)
                    {
                        try
                        {
                            renderScreen(globalBoard);

                            if (message != " ")
                            {
                                Console.WriteLine(centerBufferL + message);
                            }

                            input = Convert.ToInt32(Console.ReadLine());

                            if (globalBoard.checkColumn(input))
                            {
                                valid = true;
                                message = " ";
                            }
                            else
                            {
                                message = "Column is full!";
                            }
                            ;
                        }
                        catch { }
                        ;
                    }
                    ;

                    globalBoard.placeToColumn(input, globalBoard);

                    if (globalBoard.player == 1)
                    {
                        globalBoard.player = 2;
                    }
                    else
                    {
                        globalBoard.player = 1;
                    }
                    ;
                }
                ;
            }
            ;

            Console.SetWindowSize(75, 30);
            Console.Title = "Connect Four";
            runGame();
        }
    }
}
