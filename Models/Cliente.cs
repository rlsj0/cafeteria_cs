namespace Models;

public class Cliente : Usuario
{
    public List<Pedido> HistoricoPedidos { get; set; }

    // Constructor para crear un nuevo usuario
    public Cliente(string correo, string contrasena) : base(correo, contrasena)
    {
        HistoricoPedidos = new List<Pedido>();
    }

    // Constructor para cargar usuario del .json
    public Cliente(int id, string correo, string hashContrasena, byte[] saltContrasena, DateTime fechaCreacion, List<Pedido> historicoPedidos) : base(id, correo, hashContrasena, saltContrasena, fechaCreacion)
    {
        HistoricoPedidos = new List<Pedido>();
    }

    public void VerPedidos()
    {
        if (HistoricoPedidos == null || !HistoricoPedidos.Any())
        {
            Console.WriteLine("Aún no has hecho ningún pedido");
            return;
        }
        foreach (Pedido pedido in HistoricoPedidos)
        {
            pedido.MostrarDetalles();
        }
    }

    public void HacerPedido(int idCliente, List<Tuple<Cafe, int>> productos, bool clienteSatisfecho)
    {

        foreach (Tuple<Cafe, int> tupla in productos)
        {
            try
            {
                tupla.Item1.RestarStock(tupla.Item2);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error: {e}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Pedido incorrecto (no hay tanto stock).");
                Console.WriteLine($"Error: {e}");
            }
        }

        Pedido pedido = new Pedido(idCliente, productos, clienteSatisfecho);
        HistoricoPedidos.Add(pedido);
    }
}
