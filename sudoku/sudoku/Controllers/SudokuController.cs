using Microsoft.AspNetCore.Mvc;

namespace sudoku.Controllers;

[Route("api/[controller]")]
public class SudokuController : Controller
{
    [HttpGet("generate")]
    public IActionResult Generate([FromQuery] int difficulty = 1)
    {
        var generator = new Services.SudokuGenerator();
        var board = generator.Construct().RemoveNumbers(difficulty).GetBoard();
        
        //Turning the board into a string
        string boardString = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                    boardString += board[i, j].ToString();
            }
        }
        
        return Ok(boardString);
    }
}