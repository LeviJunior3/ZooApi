using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooApi.Data;
using ZooApi.Dtos;
using ZooApi.Models;

namespace ZooApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AnimalController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> Get()
        {
            var animais = await _context.Animais
                .Include(a => a.AnimaisCuidados)
                    .ThenInclude(ac => ac.Cuidado)
                .ToListAsync();

            return _mapper.Map<List<AnimalDto>>(animais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDto>> GetById(int id)
        {
            var animal = await _context.Animais
                .Include(a => a.AnimaisCuidados)
                    .ThenInclude(ac => ac.Cuidado)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null) return NotFound();

            return _mapper.Map<AnimalDto>(animal);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AnimalDto animalDto)
        {
            var animal = _mapper.Map<Animal>(animalDto);

            animal.AnimaisCuidados = new List<AnimalCuidado>();

            if (animalDto.CuidadoIds != null && animalDto.CuidadoIds.Any())
            {
                foreach (var cuidadoId in animalDto.CuidadoIds)
                {
                    var cuidado = await _context.Cuidados.FindAsync(cuidadoId);
                    if (cuidado != null)
                    {
                        animal.AnimaisCuidados.Add(new AnimalCuidado
                        {
                            Animal = animal,
                            Cuidado = cuidado
                        });
                    }
                }
            }

            _context.Animais.Add(animal);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<AnimalDto>(animal);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AnimalDto animalDto)
        {
            if (id != animalDto.Id) return BadRequest();

            var animal = await _context.Animais
                .Include(a => a.AnimaisCuidados)
                .ThenInclude(ac => ac.Cuidado)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null) return NotFound();

            _mapper.Map(animalDto, animal);

            animal.AnimaisCuidados.Clear();

            if (animalDto.CuidadoIds != null && animalDto.CuidadoIds.Any())
            {
                foreach (var cuidadoId in animalDto.CuidadoIds)
                {
                    var cuidado = await _context.Cuidados.FindAsync(cuidadoId);
                    if (cuidado != null)
                    {
                        animal.AnimaisCuidados.Add(new AnimalCuidado
                        {
                            Animal = animal,
                            Cuidado = cuidado
                        });
                    }
                }
            }

            _context.Entry(animal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _context.Animais
                .Include(a => a.AnimaisCuidados)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null) return NotFound();

            _context.AnimaisCuidados.RemoveRange(animal.AnimaisCuidados);

            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}