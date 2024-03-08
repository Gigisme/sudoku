namespace sudoku.Services;

public class SudokuGenerator
{
    private const int SIZE = 9;
    private int[,] _board;
    private readonly Random _random = new();

    public SudokuGenerator Construct()
    {
        _board = new int[SIZE, SIZE];
        FillDiagonal();
        FillRemaining(0, 3);
        return this;
    }
    
    /// <summary>
    /// Removes a certain number of cells from the Sudoku board based on the difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level of the Sudoku game. It can be 1, 2, or 3.</param>
    /// <returns>Returns the current instance of the SudokuGenerator class.</returns>
    /// <remarks>
    /// The method works as follows:
    /// 1. Based on the difficulty level, it determines the number of cells to remove from the Sudoku board. 
    ///    - For difficulty level 1, it removes 20 cells.
    ///    - For difficulty level 2, it removes 40 cells.
    ///    - For difficulty level 3, it removes 60 cells.
    /// 2. It then removes the determined number of cells by setting their values to 0. The cells are chosen randomly.
    /// </remarks>
    public SudokuGenerator RemoveNumbers(int difficulty)
    {
        var cellsToRemove = 0;
        switch (difficulty)
        {
            case 1:
                cellsToRemove = 20;
                break;
            case 2:
                cellsToRemove = 40;
                break;
            case 3:
                cellsToRemove = 60;
                break;
        }
        
        for (var i = 0; i < cellsToRemove; i++)
        {
            var row = _random.Next(0, SIZE);
            var col = _random.Next(0, SIZE);
            _board[row, col] = 0;
        }

        return this;
    }
    
    public int[,] GetBoard()
    {
        return _board;
    }
    

    private void FillDiagonal()
    {
        for (var i = 0; i < SIZE; i += 3) FillBox(i, i);
    }

    private bool IsSafe(int row, int col, int num)
    {

        var startRow = row - row % 3;
        var startCol = col - col % 3;

        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            if (_board[i + startRow, j + startCol] == num)
                return false;
        
        for (var x = 0; x < SIZE; x++)
            if (_board[row, x] == num || _board[x, col] == num)
                return false;

        return true;
    }

    private bool FillRemaining(int i, int j)
    {
        if (i == SIZE - 1 && j == SIZE) return true;

        if (j == SIZE)
        {
            i++;
            j = 0;
        }

        if (_board[i, j] != 0) return FillRemaining(i, j + 1);

        for (var num = 1; num <= SIZE; num++)
            if (IsSafe(i, j, num))
            {
                _board[i, j] = num;
                if (FillRemaining(i, j + 1)) return true;
                _board[i, j] = 0;
            }

        return false;
    }

    private void FillBox(int row, int col)
    {
        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
        {
            int num;
            do
            {
                num = _random.Next(1, SIZE + 1);
            } while (!IsSafe(row + i, col + j, num));

            _board[row + i, col + j] = num;
        }
    }
}