using System.Text.Json.Serialization;
using Repositories;

namespace Models;

public class Cliente : Usuario
{
    public List<Pedido>? HistoricoPedidos { get; set; }

    // Constructor para crear un nuevo usuario
    public Cliente(string correo, string contrasena) : base(correo, contrasena)
    {
        HistoricoPedidos = new List<Pedido>();
    }

    // Constructor para cargar usuario del .json
    [JsonConstructor]
    public Cliente(int id, string correo, string hashContrasena, byte[] saltContrasena, DateTime fechaCreacion, List<Pedido> historicoPedidos) : base(id, correo, hashContrasena, saltContrasena, fechaCreacion)
    {
        HistoricoPedidos = historicoPedidos;
        if (historicoPedidos == null || !historicoPedidos.Any())
        {
            HistoricoPedidos = new List<Pedido>();
        }

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

    public Pedido HacerPedido(int idCliente, List<Tuple<Cafe, int>> productos, bool clienteSatisfecho)
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
        if (HistoricoPedidos != null)
        {
            HistoricoPedidos.Add(pedido);
        }
        else
        {
            HistoricoPedidos = new List<Pedido>();
            HistoricoPedidos.Add(pedido);
        }
        return pedido;
    }

    public static Cliente RegistrarCliente(string correo, string contrasena)
    {
        Cliente cliente = new Cliente(correo, contrasena);

        List<Cliente> listaClientes = new List<Cliente>();
        List<Admin> listaAdmin = new List<Admin>();

        if (File.Exists(ClienteRepository.Ruta))
        {
            listaClientes = ClienteRepository.CargarClientes();
            listaAdmin = AdminRepository.CargarAdmin();
            foreach (Cliente c in listaClientes)
            {
                if (c.Correo == correo)
                {
                    throw new Exception("Este correo ya está registrado.");
                }
            }
            foreach (Admin a in listaAdmin)
            {
                if (a.Correo == correo)
                {
                    throw new Exception("Este correo ya está registrado.");
                }
            }
        }

        listaClientes.Add(cliente);
        ClienteRepository.GuardarClientes(listaClientes);
        return cliente;
    }
}
