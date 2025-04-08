using BibliotecaAPI.Entidades;

namespace BibliotecaAPI;

public interface IRespositorioValores
{
    IEnumerable<Valor> ObtenerValores();
    void InsertarValores(Valor valor);
}