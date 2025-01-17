namespace Repositories;
using Utilities;
using Models;

public class ClienteRepository
{
    public static string Ruta = "data/cliente.json";
    public static List<Cliente> CargarClientes()
    {
        List<Cliente> listaClientes = new List<Cliente>();
        listaClientes = JsonUtility.CargarJson<List<Cliente>>(Ruta);
        return listaClientes;
    }
    public static void GuardarClientes(List<Cliente> listaClientes)
    {
        JsonUtility.GuardarJson<List<Cliente>>(Ruta, listaClientes);
    }
}
