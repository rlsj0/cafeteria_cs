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
    // Hacer un metodo que tome una array que proximamente nos pasara la clase del .json, e imprimir
    public void VerClientes(List<Usuario> listaCliente)
    {
        foreach (Usuario cliente in listaCliente)
        {
            string fecha = cliente.FechaCreacion.ToString();
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

    // TODO: Ver total de pedidos
    // Hacer un metodo que tome una array que proximamente nos pasara la clase del .json, e imprimir
    // public void VerTotalPedidos(){}
}
