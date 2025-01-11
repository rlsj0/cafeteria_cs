using System.Text.Json.Serialization;

namespace Models;

public class Pedido
{
    public int Id { get; set; }
    public static int nextId { get; set; }
    public int IdCliente { get; set; }
    public List<Tuple<Cafe, int>> Productos { get; set; }
    public decimal PrecioTotal { get; set; }
    public DateTime Fecha { get; set; }
    public bool ClienteSatisfecho { get; set; }

    // Constructor para crear nuevo pedido
    public Pedido(int idCliente, List<Tuple<Cafe, int>> productos, bool clienteSatisfecho)
    {
        Id = ++nextId;
        IdCliente = idCliente;
        Productos = productos;
        ClienteSatisfecho = clienteSatisfecho;
        Fecha = DateTime.Now;

        PrecioTotal = CalcularPrecioTotal();
    }

    // Constructor para cargar un pedido del json
    [JsonConstructor]
    public Pedido(int id, int idCliente, List<Tuple<Cafe, int>> productos, decimal precioTotal, DateTime fecha, bool clienteSatisfecho)
    {
        Id = id;
        IdCliente = idCliente;
        Productos = productos;
        PrecioTotal = precioTotal;
        Fecha = fecha;
        ClienteSatisfecho = clienteSatisfecho;

        if (nextId <= id)
        {
            nextId = id;
        }
    }

    private decimal CalcularPrecioTotal()
    {
        decimal precioTotal = 0;
        foreach (Tuple<Cafe, int> tupla in Productos)
        {
            decimal precioProducto = tupla.Item2 * tupla.Item1.Precio;
            precioTotal = precioTotal + precioProducto;
        }
        return precioTotal;
    }

    public void MostrarDetalles()
    {
        string fecha = Fecha.ToString("dd/MM/yyyy");
        string satisfaccion = ClienteSatisfecho ? "Sí" : "No";
        string texto = $"ID del pedido: {Id}\n\tID del cliente: {IdCliente}" +
            $"\n\tFecha: {fecha}\n\t¿Cliente satisfecho? {satisfaccion}" +
            $"\n\tProductos:";
        Console.WriteLine(texto);

        foreach (Tuple<Cafe, int> tupla in Productos)
        {
            Console.WriteLine($"\t\t{tupla.Item1.Variedad}, {tupla.Item1.Tipo}: " +
                    $"{tupla.Item2} x {tupla.Item1.Precio:C}");
        }
        Console.WriteLine($"Precio total: {PrecioTotal:C}");
    }

}
