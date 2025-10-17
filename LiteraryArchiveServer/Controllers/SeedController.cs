using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteraryArchiveServer.Data;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using pleaseworkplease;





namespace LiteraryArchiveServer.Controllers


{
    [Route("api/[controller]")]

    [ApiController]
    public class SeedController(StarterBaseContext context, IHostEnvironment enviornment) : ControllerBase


    {
        string _pathName = Path.Combine(enviornment.ContentRootPath, "Data/bookdata.csv");
        [HttpPost("Genres")]
        public async Task<ActionResult> PostGenres()
        {
            Dictionary<string, Genre> genres = await context.Genres.AsNoTracking().
                ToDictionaryAsync(c => c.Genre1, StringComparer.OrdinalIgnoreCase);
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, HeaderValidated = null
            };
            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);
            List<AddCSVDATA> records = csv.GetRecords<AddCSVDATA>().ToList();

            foreach (AddCSVDATA record in records) {
                if (!genres.ContainsKey(record.genre))
                {
                    Genre genreadd = new()
                    {
                        Keyword = record.keyword,
                        Rating = record.rating,
                        Genre1 = record.genre

                    };
                    genres.Add(genreadd.Keyword, genreadd);
                    await context.Genres.AddAsync(genreadd);
                }
            }
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Novels")]
        public async Task<ActionResult> PostNovels()
        {
            Dictionary<string, Genre> genres = await context.Genres.AsNoTracking()
                .ToDictionaryAsync(c => c.Genre1, StringComparer.OrdinalIgnoreCase);
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);
            List<AddCSVDATA> records = csv.GetRecords<AddCSVDATA>().ToList();
            int novel_count = 0;

            foreach (AddCSVDATA record in records) {
                if (genres.ContainsKey(record.genre)) {
                    Novel novel = new()
                    {
                        Isbn = record.isbn,
                        Title = record.title,
                        Author = record.author,
                        Genre = genres[record.genre].Id
                    };
                    novel_count++;
                    await context.Novels.AddAsync(novel);
                }
                
            }
            await context.SaveChangesAsync();
            return new JsonResult(novel_count);
        }
    }

}