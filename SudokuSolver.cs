using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] SudokuGrid = new int[,] {
                {0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                {0, 0, 0, 6, 0, 0, 0, 0, 3}, 
                {0, 7, 4, 0, 8, 0, 0, 0, 0}, 
                {0, 0, 0, 0, 0, 3, 0, 0, 2}, 
                {0, 8, 0, 0, 4, 0, 0, 1, 0}, 
                {0, 0, 0, 5, 0, 0, 0, 0, 0}, 
                {0, 0, 0, 0, 1, 0, 7, 0, 0}, 
                {5, 0, 0, 0, 0, 9, 0, 0, 0}, 
                {0, 0, 0, 0, 0, 0, 0, 4, 0}
            };

            if(SudokuGrid == null){
                Console.WriteLine("Sudoku can not be solved.");
            }
            else{
                if (SolveSudoku(SudokuGrid, 0, 0))
            {
                Console.WriteLine("Sudoku Board Completed.");
                print_board(SudokuGrid);
            }
            // if SolveSudoku returns false
            else Console.WriteLine("Sudoku can not be solved.");
            }
        }



        //////////////////////////////////////////////////////////
        //main solving driver with backtracking
        public static bool SolveSudoku(int[,] sudoku, int row, int col)
        {
        bool filled = true;
        // checking for each index if it is 0
        for (int i = 0; i < 9; i++){
            for (int j = 0; j < 9; j++){
                //if index is 0, then set row and column to that index and filled is false
                if (sudoku[i, j] == 0){
                    row = i; col = j;
                    filled = false;
                    break;
                }
            }
            if (!filled) break;
        }
        // if there are no empty spaces left, then return true
        if (filled) return true;
        //size of rows and columns
        for (int i = 1; i <= 9; i++){
            //if constraints pass, index is equal to number in for loop
            if (RowConstraint(sudoku, row, i) && ColConstraint(sudoku, col, i) && SubSquareConstraint(sudoku, row, col, i))
            {
                //if constraints pass, then number added to index
                sudoku[row, col] = i;
                if (SolveSudoku(sudoku, row, col)) return true;
                //if constraints don't pass, set index to 0 and go again with new number.
                else sudoku[row, col] = 0;
            }
        }
        //if returns false, unable to solve sudoku board
        return false;
    }

        ////////////////////////////////////////////////////////////////////////////
        //contraints
        public static bool RowConstraint(int[,] sudoku, int row, int number){
            //for length of row, check each index for number
            for (int i = 0; i <= 8; ++i)
            {
                if (number == sudoku[row, i]) return false;
            }
            return true;
        }

        public static bool ColConstraint(int[,] sudoku, int column, int number){
            //for length of row, check each index for number
            for (int i = 0; i <= 8; ++i)
            {
                if (number == sudoku[i, column]) return false;
            }
            return true;
        }

        ////////////////////
        /////needs fixing
        ///////////////////
        public static bool SubSquareConstraint(int[,] sudoku, int row, int column, int num){
            for (int i = 0; i < 3; i++){
                for (int j = 0; j < 3; j++){
                    if (num == sudoku[i + (row-row%3), j + (column-column%3)]) return false;
                }
            }
        return true;
        }

        //
        ////////////////////////////////////////////////////////////////////////////

        public static void print_board(int[,] board){
            for (int i = 0; i < 9; ++i)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine("- - - - - - - - - - - - - ");
                }

                for (int j = 0; j < 9; ++j)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write(" | ");
                    }
                    if (j == 8)
                        Console.WriteLine(board[i, j]);
                    else
                        Console.Write(board[i, j] + " ");
                }
            }
        }
    }
}
