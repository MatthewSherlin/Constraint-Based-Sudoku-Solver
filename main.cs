using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuSolver : MonoBehaviour
{
    private int[,] SudokuGrid;
    private GameObject[,] SudokuVisualGrid;
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private RectTransform ParentPanel;

    void Start()
    {
        int[,] SudokuGrid = new int[,] {
                { 0, 0, 0, 2, 6, 0, 7, 0, 1},
                { 6, 8, 0, 0, 7, 0, 0, 9, 0},
                { 1, 9, 0, 0, 0, 4, 5, 0, 0},
                { 8, 2, 0, 1, 0, 0, 0, 4, 0},
                { 0, 0, 4, 6, 0, 2, 9, 0, 0},
                { 0, 5, 0, 0, 0, 3, 0, 2, 8},
                { 0, 0, 9, 3, 0, 0, 0, 7, 4},
                { 0, 4, 0, 0, 5, 0, 0, 3, 6},
                { 7, 0, 3, 0, 1, 8, 0, 0, 0}
            };

        GenerateGrid();
        // if SolveSudoku returns true
        if(SolveSudoku(SudokuGrid, 0, 0)){
            console.WriteLine("Sudoku Board Completed.");
            print_board(SudokuGrid);
        }
        // if SolveSudoku returns false
        else console.WriteLine("Sudoku can not be completed.");
    }

    void GenerateGrid()
    {
        SudokuVisualGrid = new GameObject[9, 9];

        for (int x = 0; x < 9; ++x)
        {
            for (int y = 0; y < 9; ++y)
            {
                SudokuVisualGrid[x,y] = Instantiate(TilePrefab, new Vector3(x, y), Quaternion.identity);
                SudokuVisualGrid[x, y].transform.SetParent(ParentPanel, false);
                SudokuVisualGrid[x, y].name = $"Tile {x} {y}";
                TextMeshProUGUI buttonText = SudokuVisualGrid[x, y].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = SudokuGrid[x, y].ToString();
            }
        }
    }

    //main solving driver
    void SolveSudoku(int[,] sudoku, int row, int column)
    {
        //if column becomes 9, move to next row and reset column
        if(column == 9){
            row += 1;
            column = 0;
        }
        //if current index contains 0, go to next column
        if(sudoku[row, column] != 0){
            return SolveSudoku(sudoku, row, ++column);
        }

        //size of rows and columns
        for(int i = 0; i < 9; i++){
            //if constraints pass, index is equal to number in for loop
            if(RowConstraint(sudoku, row, i) && ColConstraint(sudoku, column, i) && SubSquareConstraint(sudoku)){
                sudoku[row,column] = i;
                //check for next possibility
                if(SolveSudoku(sudoku, row, column+1)) return true;
            }
            else{
                //if constraints don't pass, set index to 0 and go again.
                sudoku[row,column] = 0;
            }
        }
        //if returns false, unable to solve sudoku board
        return false;
    }

    ////////////////////////////////////////////////////////////////////////////
    //contraints
    bool RowConstraint(int[,] sudoku, int row, int number)
    {
        for (int x = 0; x < 9; ++x){
            if(sudoku[row, x] == number)
                return false;
            
        }
        return true;
    }
    
    bool ColConstraint(int[,] sudoku, int column, int number)
    {
        for (int x = 0; x < 9; ++x){
            if(sudoku[x, column] == number)
                return false;
            
        }
        return true;
    }
    
    bool SubSquareConstraint(int[,] sudoku, int row, int column)
    {
        int x = row / 3;
        int y = column / 3;
        for(int i = 0; i < 3; ++i){
            for(int j = 0; j < 3; ++j){
                return false;
            }
        }
        return true;
    }

      void print_board(int[,] board){
        for(int i = 0; i < 9; ++i){
            if(i % 3 == 0 && i != 0){
                Console.WriteLine("- - - - - - - - - - - - - ");
            }
            for(int j = 0; j < 9; ++j){
                if(j % 3 == 0 && j != 0)
                {
                    Console.Write(" | ");
                }
                if(j == 0)
                    Console.Write(board[i,j]);
                else
                    Console.Write(board[i,j] + " ");
                
            }
        }
    }
    //
    ////////////////////////////////////////////////////////////////////////////

}