using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] SudokuGrid = new int[,] {
                { 0, 2, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 6, 0, 0, 0, 0, 3},
                { 0, 7, 4, 0, 8, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 3, 0, 0, 2},
                { 0, 8, 0, 0, 4, 0, 0, 1, 0},
                { 6, 0, 0, 5, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 1, 0, 7, 8, 0},
                { 5, 0, 0, 0, 0, 9, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 4, 0}
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
        //main solving driver
        public static bool SolveSudoku(int[,] sudoku, int row, int column)
        {
            //if column becomes 9, move to next row and reset column
            if (column == 9)
            {
                row += 1;
                if (row == 9) return true;
                column = 0;
                
            }
            //size of rows and columns
            if (sudoku[row, column] != 0) return SolveSudoku(sudoku, row, column + 1);
            for (int i = 1; i <= 9; i++)
            {
                //if constraints pass, index is equal to number in for loop
                if (RowConstraint(sudoku, row, i) && ColConstraint(sudoku, column, i) && SubSquareConstraint(sudoku, row, column, i))
                {
                    sudoku[row, column] = i;
                    if (SolveSudoku(sudoku, row, column + 1)) return true;
                }
                else
                {
                    //if constraints don't pass, set index to 0 and go again.
                    sudoku[row, column] = 0;
                }
            }
            //if returns false, unable to solve sudoku board
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////
        //contraints
        public static bool RowConstraint(int[,] sudoku, int row, int number)
        {
            //for length of row, check each index for number
            for (int i = 0; i < 9; ++i)
            {
                if (number == sudoku[row, i]) return false;
            }
            return true;
        }

        public static bool ColConstraint(int[,] sudoku, int column, int number)
        {
            //for length of row, check each index for number
            for (int i = 0; i < 9; ++i)
            {
                if (number == sudoku[i, column]) return false;
            }
            return true;
        }

        ////////////////////
        /////needs fixing
        ///////////////////
        public static bool SubSquareConstraint(int[,] sudoku, int row, int column, int num)
        {
            int x = row / 3;
            int y = column / 3;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (num == sudoku[3 * x + (i/3), 3 * y + (j/3)])
                        return false;
                }
            }
            return true;
        }

        //
        ////////////////////////////////////////////////////////////////////////////

        public static void print_board(int[,] bo)
        {
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
                        Console.WriteLine(bo[i, j]);
                    else
                        Console.Write(bo[i, j] + " ");
                }
            }
        }
    }

}
