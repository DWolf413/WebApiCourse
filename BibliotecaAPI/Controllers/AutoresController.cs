using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("api/autores")]
public class AutoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AutoresController(ApplicationDbContext context) //Inyección de dependecias
    {
        _context = context;
    }
    
    /*[HttpGet]
    public IEnumerable<Autor> Get()
    {
        return new List<Autor>
        {
            new Autor { Id = 1, Nombre = "David" },
            new Autor { Id = 2, Nombre = "John" }
        };
    }*/
    
    [HttpGet]
    public async Task<IEnumerable<Autor>> Get()
    {
        return await _context.Autores.ToListAsync();
    }

    [HttpGet("{id:int}")] //api/autores/2
    public async Task<ActionResult<Autor>> Get(int id)
    {
        var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);

        if (autor is null)
        {
            return NotFound();
        }
        
        return autor;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Autor autor)
    {
        _context.Add(autor);
        await _context.SaveChangesAsync(); //No me quedo frezze hago otras cosas hasta 
        return Ok();
    }

    [HttpPut("{id:int}")] //api/autores/id
    public async Task<ActionResult> Put(int id, Autor autor)
    {
        if (id != autor.Id)
        {
            return BadRequest("Los Ids deben de coincidir");
        }
        _context.Update(autor);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var registroBorrado = await _context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

        if (registroBorrado == 0)
        {
            return NotFound();
        }

        return Ok();
    }
}

