using System;


enum cellState
{
    _,
    X,
    O,
};
class Program
{
    const int boardSize = 3;
    const int cellNumberMinimum = 1;
    const int cellNumberMaximum = boardSize * boardSize;
    // C# does not allow to declare a constant array, the below portion is an alternative way to achieve it
    //static readonly int[] xx = new int[boardSize] { 1, 2, 3 };
    static void Main()
    {
 
        char currentPlayer = 'X';
        char nextPlayer = nextPlayerSelection('X') ;
        bool isGameOver = false;
        
        cellState[,] board = new cellState[boardSize,boardSize];

        resetBoard(board);
        displayBoard(board);

        do
        {
            int selectedCell = askForMove(currentPlayer, board);
            int selectedRow = (selectedCell-1) / boardSize;
            int selectedCol = (selectedCell-1) % boardSize;
            boardUpdate(currentPlayer, selectedRow, selectedCol, board);
            displayBoard(board);
            isGameOver = checkGameStatus(board, currentPlayer);
            currentPlayer = nextPlayerSelection(currentPlayer);
        } while (!isGameOver);
    }

    static bool checkGameStatus(cellState[,] board, char currentPlayer)
    {
        bool boardFull = checkBoardFull(board);
        bool hasWon = checkWinStatus(board);
        if (hasWon)
            Console.WriteLine(currentPlayer+" has Won");
        return (boardFull||hasWon); 
    }

    static bool checkWinStatus(cellState[,] board)
    {
        bool horizontalFalg = horizontalWin(board);
        bool verticalFalg = verticalWin(board);
        bool diagonalFlag = diagonalWin(board);
        bool subDiagonalFlag = subDiagonalWin(board);
        return (horizontalFalg||verticalFalg|| diagonalFlag|| subDiagonalFlag);
    }

    static bool subDiagonalWin(cellState[,] board)
    {
        bool flag = true;
        cellState cellPrev = cellState._;
        for (int dimension = 0; dimension < board.GetLength(0); dimension++)
        {
            if (board[dimension, (board.GetLength(0)-1-dimension)] == cellState._)
            {
                flag = false;
                break;
            }
            else if (dimension == 0)
            {
                cellPrev = board[dimension, (board.GetLength(0) - 1 - dimension)];
            }
            else
            {
                if (cellPrev == board[dimension, (board.GetLength(0) - 1 - dimension)])
                {
                    cellPrev = board[dimension, (board.GetLength(0) - 1 - dimension)];
                }
                else
                {
                    flag = false;
                    break;
                }
            }
        }
        return flag;
    }
    static bool diagonalWin(cellState[,] board)
    {
        bool flag = true;
        cellState cellPrev = cellState._;
        for (int dimension = 0; dimension < board.GetLength(0); dimension++)
        {
            if (board[dimension, dimension] == cellState._)
            {
                flag = false;
                break;
            }
            else if (dimension == 0)
            {
                cellPrev = board[dimension, dimension];
            }
            else
            {
                if (cellPrev == board[dimension, dimension])
                {
                    cellPrev = board[dimension, dimension];
                }
                else
                {
                    flag = false;
                    break;
                }
            }
        }
        return flag;
    }
    static bool verticalWin(cellState[,] board)
    {
        bool flag = false;
        int count=0;

        for (int col = 0; col < board.GetLength(1); col++)
        {
            cellState cellPrev = cellState._;
            count = 0;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                if (board[row, col] == cellState._)
                {
                    //flag = false;
                    break;
                }
                else if (row == 0)
                {
                    cellPrev = board[row, col];
                    count++;
                }
                else
                {
                    if (cellPrev == board[row, col])
                    {
                        cellPrev = board[row, col];
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (count == boardSize)
            {
                flag=true;
                break;
            }

        }
        return flag;
    }
    static bool horizontalWin(cellState[,] board)
    {
        int count = 0;
        bool flag = false;
        for (int row =0;row<board.GetLength(0); row++)
        {
            cellState cellPrev = cellState._;
            count = 0;
            
            for(int col=0;col<board.GetLength(1);col++)
            {
                if (board[row,col] == cellState._)
                {
                    //flag = false;
                    break;
                }
                else if(col==0)
                {
                    cellPrev = board[row,col];
                    count++;
                }
                else
                {
                    if(cellPrev == board[row,col])
                    {
                        cellPrev = board[row, col];
                        count++;
                    }
                    else
                    {
                        break; 
                    }
                }
            }
            if (count == boardSize)
            {
                flag = true;
                break;
            }
        }
        return flag;
    }
    static bool checkBoardFull(cellState[,] board)
    {
        bool flag = true;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i,j]==(cellState._))
                {
                    flag = false;
                    break;
                }
            }
           
        }
        return flag;
           }
    static void boardUpdate(char currentPlayer,int selectedRow, int SelectedColoumn, cellState[,] board)
    {
        if (currentPlayer == 'X')
            board[selectedRow, SelectedColoumn] = cellState.X;
        else
            board[selectedRow, SelectedColoumn] = cellState.O;
    }
    static string readString(string prompt)
    {
        string result;

        do
        {
            Console.Write(prompt);
            result = Console.ReadLine();
        } while (result == "");
        return result;
    }

    static int readInt(string prompt, int low, int high)
    {
        int result = 0;

        do
        {
            string intString = readString(prompt);
            int.TryParse(intString, out result);

        } while ((result < low) || (result > high));

        return result;
    }
    static int askForMove(char currentPlayer, cellState[,] board)
    {
        int moveCell;
        int row;
        int col;
        string userPrompt = currentPlayer + " to move. Choose a square:";
        do
        {
            moveCell = readInt(userPrompt, cellNumberMinimum, cellNumberMaximum);
            row= (moveCell-1)/boardSize;
            col = (moveCell - 1) % boardSize;

        } while (board[row,col] !=cellState._);
        return moveCell;
    }
    static char nextPlayerSelection(char currentPlayer)
    {
        if (currentPlayer == 'X')
        {
            return 'O';
        }
        else
        {
            return 'X';
        }
    }

    static void resetBoard(cellState[,] board)
    {
        for (int row = 0; row<board.GetLength(0);row++)
        {
            for (int col = 0; col<board.GetLength(1);col++)
            {
                board[row, col] = cellState._;
            }
        }
    }

    static void displayBoard(cellState[,] board)
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            displayLine(board.GetLength(0) * 5);
            for (int col = 0; col < board.GetLength(1); col++)
            {
                Console.Write(" | "+board[row,col]);
            }
            Console.Write(" | \n");
        }
        displayLine(board.GetLength(0) * 5);
    }

    static void displayLine(int lineLength)
    {
        for(int i =0; i < lineLength;i++)
        {
            Console.Write("-");
        }
        Console.Write('\n');
    }
}