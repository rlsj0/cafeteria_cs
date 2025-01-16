namespace Repositories;
using Utilities;
using Models;

public class PedidoRepository
{
    public static string Ruta = "pedido.json";
    public static List<Pedido> CargarPedidos()
    {
        List<Pedido> listaPedidos = new List<Pedido>();
        try
        {
            listaPedidos = JsonUtility.CargarJson<List<Pedido>>(Ruta);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            GuardarPedidos(listaPedidos);
            Console.WriteLine("Fichero creado, inténtelo de nuevo");
        }
        return listaPedidos;
    }
    public static void GuardarPedidos(List<Pedido> listaPedidos)
    {
        JsonUtility.GuardarJson<List<Pedido>>(Ruta, listaPedidos);
    }
}
