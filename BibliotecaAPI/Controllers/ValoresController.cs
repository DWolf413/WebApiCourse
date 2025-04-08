using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("api/valores")]
public class ValoresController : ControllerBase
{
    private readonly IRespositorioValores _respositorioValores;
    private readonly ServicioTransient _transient1;
    private readonly ServicioTransient _transient2;
    private readonly ServicioScoped _scoped1;
    private readonly ServicioScoped _scoped2;
    private readonly ServicioSingleton _singleton;


    // public ValoresController(RespositorioValores respositorioValores)
    // {
    //     _respositorioValores = respositorioValores; // La rsponsabilidad de instanciar esta dependencia se le ha dado yha quien llama a la calse
    // }
    
    public ValoresController(IRespositorioValores respositorioValores,
        ServicioTransient transient1,
        ServicioTransient transient2,
        ServicioScoped scoped1,
        ServicioScoped scoped2,
        ServicioSingleton singleton)
    {
        _respositorioValores = respositorioValores; // La rsponsabilidad de instanciar esta dependencia se le ha dado yha quien llama a la calse
        _transient1 = transient1;
        _transient2 = transient2;
        _scoped1 = scoped1;
        _scoped2 = scoped2;
        _singleton = singleton;
    }

    [HttpGet("servicios-tiempos-de-vida")]
    public IActionResult GetServiciosTiempoDeVida()
    {
        return Ok(new
        {
            Transients = new
            {
                _transient1 = _transient1.ObtenerGuid,
                _transient2 = _transient2.ObtenerGuid
            },
            
            Scopes = new
            {
                _scoped1 = _scoped1.ObtenerGuid,
                _scoped2 = _scoped2.ObtenerGuid
            },
            Singleton = _singleton.ObtenerGuid
        });
    }
    
    
    [HttpGet]
    public IEnumerable<Valor> Get()
    {
        //var respostorioValores = new RespositorioValores(); //Acoplamiento Valores
        return _respositorioValores.ObtenerValores();
    }

    [HttpPost]
    public IActionResult Post(Valor valor)
    {
        _respositorioValores.InsertarValores(valor);
        return Ok();
    }
}