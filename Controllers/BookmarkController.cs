// BookmarkController.cs

using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BookmarkController : ControllerBase
{
    private readonly BookmarkService _bookmarkService;

    public BookmarkController(BookmarkService bookmarkService)
    {
        _bookmarkService = bookmarkService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddItemToBookmark([FromBody] BookmarkRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.ItemName) || string.IsNullOrWhiteSpace(request.ItemType))
        {
            return BadRequest("Invalid request parameters");
        }

        var result = await _bookmarkService.AddItemToBookmarkAsync(request.UserId, request.ItemName, request.ItemType);

        if (result)
        {
            return Ok("Item added to bookmark successfully");
        }

        return BadRequest("Failed to add item to bookmark");
    }
    [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var bookmarks = await _bookmarkService.GetByUserIdAsync(userId);

            if (bookmarks == null)
            {
                return NotFound();
            }

            return Ok(bookmarks);
        }
}

public class BookmarkRequest
{
    public string UserId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
}
