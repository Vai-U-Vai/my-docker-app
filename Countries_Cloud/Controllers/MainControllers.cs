using Countries_Cloud.DB;
using Countries_Cloud.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Countries_Cloud.Controllers
{
    [Route("api")]
    [ApiController]
    public class MainControllers : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MainControllers(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Server is running");
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetAllAsync()
        {
            var countries = await _db.Set<Country>().ToListAsync();
            return Ok(countries);
        }

        [HttpGet("country/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var country = await _db.Set<Country>().FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // Получить страну по коду
        [HttpGet("country/{code:alpha}")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var country = await _db.Set<Country>()
                .FirstOrDefaultAsync(c =>
                    c.Alpha2Code == code ||
                    c.Alpha3Code == code ||
                    c.NumericCode == code);

            if (country == null)
            {
                return NotFound($"Country with code '{code}' not found.");
            }

            return Ok(country);
        }
        [HttpPost("country")]
        public async Task<IActionResult> AddAsync([FromBody]Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _db.AddAsync(country);
            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                var locationUrl = $"{Request.Scheme}://{Request.Host}/api/country/{country.Id}";
                return Created(locationUrl, country);
            }
            else
            {
                return BadRequest("Не удалось добавить страну.");
            }
        }

        // Удалить страну
        [HttpDelete("country/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Некорректный идентификатор.");
            }
            try
            {


                var country = await _db.Set<Country>().FindAsync(id);
                if (country == null)
                {
                    return NotFound($"Не удалось найти страну с id {id}");
                }
                _db.Set<Country>().Remove(country);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при удалении страны.");
            }
        }
    }
}
