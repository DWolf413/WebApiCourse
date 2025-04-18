﻿using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("api/autores")]
public class AutoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AutoresController> _logger;

    public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger) //Inyección de dependecias
    {
        _context = context;
        _logger = logger;
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
    
    [HttpGet("/listado-de-autores")] //Ignoro ruta del controlador
    [HttpGet]
    public async Task<IEnumerable<Autor>> Get()
    {
        _logger.LogTrace("Listado de autores");
        _logger.LogInformation("Listado de autores");
        return await _context.Autores.ToListAsync();
    }

    // [HttpGet("{id:int}")] //api/autores/2
    // public async Task<ActionResult<Autor>> Get(int id)
    // {
    //     var autor = await _context.Autores
    //         .Include(x => x.Libros)
    //         .FirstOrDefaultAsync(x => x.Id == id);
    //
    //     if (autor is null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     return autor;
    // }
    
    [HttpGet("{id:int}")] //api/autores/2?incluirLibreos=false
    public async Task<ActionResult<Autor>> Get([FromRoute] int id, [FromQuery] bool incluirLibros) //Model Bindig
    {
        var autor = await _context.Autores
            .Include(x => x.Libros)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (autor is null)
        {
            return NotFound();
        }
        
        return autor;
    }

    [HttpGet("primero")] //api/autores/primero
    public async Task<Autor> GetPrimerAutor()
    {
        return await _context.Autores.FirstAsync();
    }

    [HttpGet("{nombre:alpha}")] //Restricción de variable de ruta
    public async Task<IEnumerable<Autor>> Get(string nombre)
    {
        return await _context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
    } 

    [HttpGet("{parametro1}/{parametros2?}")] // /api/David/Logacho
    //public ActionResult Get(string parametro1, string? parametros2)
    public IActionResult Get(string parametro1, string parametros2 = "Valor por defecto")
    {
        return Ok(new {parametro1, parametros2});
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Autor autor)
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

