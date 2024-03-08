using Microsoft.AspNetCore.Mvc;

namespace sudoku.Controllers;

[Route("api/[controller]")]
public class SudokuController : Controller
{
    [HttpGet("generate")]
    public IActionResult Generate()
    {
        return Ok("Generated");
    }
}