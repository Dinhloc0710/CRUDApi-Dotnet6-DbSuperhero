using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupperHeroController : ControllerBase
    {
       /* private static List<SupperHero> anhhungs = new List<SupperHero>
             {
                new SupperHero {
                    Id = 1,
                    Name = "Huấn Hoa Hồng",
                    FirstName = "Buồi",
                    LastName = "Huấn",
                    Place = "lào cai"
                },
                new SupperHero {
                    Id = 2,
                    Name = "Huấn Rose",
                    FirstName = "Buồi",
                    LastName = "Huấn",
                    Place = "lào cai"
                },
                new SupperHero {
                    Id = 3,
                    Name = "Huấn Pro max",
                    FirstName = "Buồi",
                    LastName = "Huấn",
                    Place = "lào cai"
                },
             };*/
        private readonly DataContext _context;
        public SupperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //public async Task<IActionResult>Get()
        public async Task<ActionResult<List<SupperHero>>>Get()
        {
            return Ok(await _context.SupperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        //public async Task<IActionResult>Get()
        public async Task<ActionResult<SupperHero>> GetSingleById(int id)
        {
            var hero = await _context.SupperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Anh hùng không tồn tại");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SupperHero>>> CreateHero(SupperHero supper)
        {
            _context.SupperHeroes.Add(supper);
            await _context.SaveChangesAsync();
            return Ok(await _context.SupperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SupperHero>>> UpdateHero(SupperHero request)
        {
            var dbHero = await _context.SupperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Anh hùng không tồn tại");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SupperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SupperHero>>> DeleteSingle(int id)
        {
            var dbHero = await _context.SupperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Anh hùng không tồn tại");


                 _context.SupperHeroes.Remove(dbHero);

            await _context.SaveChangesAsync();
            return Ok(await _context.SupperHeroes.ToListAsync());
        }
    }
}
