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
    public class CuidadoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CuidadoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuidadoDto>>> Get()
        {
            var cuidados = await _context.Cuidados.ToListAsync();
            return _mapper.Map<List<CuidadoDto>>(cuidados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuidadoDto>> GetById(int id)
        {
            var cuidado = await _context.Cuidados.FindAsync(id);
            if (cuidado == null) return NotFound();
            return _mapper.Map<CuidadoDto>(cuidado);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CuidadoDto cuidadoDto)
        {
            var cuidado = _mapper.Map<Cuidado>(cuidadoDto);
            _context.Cuidados.Add(cuidado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cuidado.Id }, _mapper.Map<CuidadoDto>(cuidado));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CuidadoDto cuidadoDto)
        {
            if (id != cuidadoDto.Id) return BadRequest();

            var cuidado = _mapper.Map<Cuidado>(cuidadoDto);
            _context.Entry(cuidado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cuidado = await _context.Cuidados.FindAsync(id);
            if (cuidado == null) return NotFound();

            _context.Cuidados.Remove(cuidado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
