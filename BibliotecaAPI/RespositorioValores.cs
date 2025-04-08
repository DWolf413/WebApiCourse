using BibliotecaAPI.Entidades;

namespace BibliotecaAPI;

public class RespositorioValores : IRespositorioValores
{
    private List<Valor> _valores;

    public RespositorioValores()
    {
        _valores= new List<Valor>
        {
            new Valor { Id = 1, Nombre = "Valore 1" },
            new Valor { Id = 2, Nombre = "Valore 2" },
        };
    }
    public IEnumerable<Valor> ObtenerValores()
    {
        return _valores;
    }

    public void InsertarValores(Valor valor)
    {
        _valores.Add(valor);
    }
}