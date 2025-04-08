using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("api/libros")]
public class LibrosController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LibrosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Libro>> Get()
    {
        return await _context.Libros.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Libro>> Get(int id)
    {
        var libro = await _context.Libros
            .Include(x => x.Autor)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (libro is null)
        {
            return NotFound();
        }
        
        return libro;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Libro libro)
    {
        var existeAutor = await _context.Libros.AnyAsync(x => x.Id == libro.AutorId);

        if (!existeAutor)
        {
            ModelState.AddModelError(nameof(libro.AutorId), $"El autor de id {libro.AutorId} no existe");
            return ValidationProblem();
            //return BadRequest($"El autor de id {libro.AutorId} no existe");
        }
        
        _context.Add(libro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Libro libro)
    {
        if (id != libro.Id)
        {
            return BadRequest("Los IDs debn de coincidir");
        }
        
        var existeAutor = await _context.Libros.AnyAsync(x => x.Id == libro.AutorId);

        if (!existeAutor)
        {
            return BadRequest($"El autor de id {libro.AutorId} no existe");
        }
        
        _context.Update(libro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var registrosBorrados = await _context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();
        
        if (registrosBorrados == 0)
        {
            return NotFound();
        }

        return Ok();
    }
}