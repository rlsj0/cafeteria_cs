namespace Repositories;
using Utilities;
using Models;

public class ClienteRepository
{
    public static string Ruta = "cliente.json";
    public static List<Cliente> CargarClientes()
    {
        List<Cliente> listaClientes = new List<Cliente>();
        try
        {
            listaClientes = JsonUtility.CargarJson<List<Cliente>>(Ruta);
        }
        catch (FileNotFoundException e)
        {
            // Gestionar excepcion desde la interfaz
            throw e;
        }
        return listaClientes;
    }
    public static void GuardarClientes(List<Cliente> listaClientes)
    {
        JsonUtility.GuardarJson<List<Cliente>>(Ruta, listaClientes);
    }
}
