using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pleaseworkplease;





namespace LiteraryArchiveServer.Controllers


{
    [Route("api/[controller]")]

    [ApiController]
    public class SeedController(StarterBaseContext context) : ControllerBase


    {
        [HttpPost("Genres")]
        public async Task<ActionResult> PostGenres()
        {
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Novels")]
        public async Task<ActionResult> PostNovels()
        {
            await context.SaveChangesAsync();
            return Ok();
        }
    }

}