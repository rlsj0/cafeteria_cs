namespace Models;

public class Admin : Usuario
{

    // Constructor para crear un admin
    public Admin(string correo, string contrasena) : base(correo, contrasena) { }

    // Constructor para cargar el json

    public Admin(int id, string correo, string hashContrasena, byte[] saltContrasena, DateTime fechaCreacion) : base(id, correo, hashContrasena, saltContrasena, fechaCreacion) { }

    public void ModificarPrecio(Cafe cafe, decimal nuevoPrecio)
    {
        cafe.Precio = nuevoPrecio;
    }

    public void AnadirStock(Cafe cafe, int stock)
    {
        cafe.CantidadStock += stock;
    }

    // Ver clientes
    public void VerClientes(List<Usuario> listaCliente)
    {
        foreach (Usuario cliente in listaCliente)
        {
            string fecha = cliente.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
            Console.WriteLine($"({cliente.Id}):\n\t{cliente.Correo}\n\t{fecha}");
        }
    }

    // Borrar cliente
    public List<Usuario> BorrarCliente(List<Usuario> listaCliente, int idCliente)
    {
        List<Usuario> lista = listaCliente;
        bool clienteExiste = false;
        foreach (Usuario cliente in listaCliente)
        {
            if (cliente.Id == idCliente)
            {
                lista.Remove(cliente);
                clienteExiste = true;
                break;
            }
            continue;
        }
        if (clienteExiste == false)
        {
            throw new Exception("El id no coincide con ningún cliente.");
        }

        return lista;
    }

    // Ver total de pedidos
    public void VerTotalPedidos(List<Pedido> lista)
    {
        if (lista == null || !lista.Any())
        {
            Console.WriteLine("No hay pedidos aún");
            return;
        }

        foreach (Pedido pedido in lista)
        {
            string fecha = pedido.Fecha.ToString("dd/MM/yyyy HH:mm");
            string satisfaccion = pedido.ClienteSatisfecho ? "Sí" : "No";
            string texto = $"{fecha}:\n\tId del cliente: {pedido.IdCliente}" +
                $"\n\t¿Cliente satisfecho? {satisfaccion}" +
                "\n\tProductos:";
            Console.WriteLine(texto);
            foreach (Tuple<Cafe, int> tupla in pedido.Productos)
            {
                string textoProducto = $"{tupla.Item2} x {tupla.Item1.Variedad}, " +
                $"{tupla.Item1.Tipo} = {tupla.Item1.Precio * tupla.Item2}";

                Console.WriteLine($"\t\t{textoProducto}");
            }
            Console.WriteLine($"\n\tPrecio total: {pedido.PrecioTotal}");
            Console.WriteLine("---");
        }
    }
}
