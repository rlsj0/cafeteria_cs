namespace Models;

public abstract class Producto
{
    public int Id { get; private set; }
    public static int nextId { get; set; } = 0;
    public decimal Precio { get; set; }
    public int CantidadStock { get; set; }
    public int NumeroCompras { get; set; }
    public bool EsComercioJusto { get; private set; }

    // Constructor para crear un nuevo producto
    public Producto(decimal precio, int cantidadStock, bool esComercioJusto)
    {
        Id = ++nextId;
        Precio = precio;
        CantidadStock = cantidadStock;
        NumeroCompras = 0;
        EsComercioJusto = esComercioJusto;
    }

    public Producto(int id, decimal precio, int cantidadStock, int numeroCompras, bool esComercioJusto)
    {
        Id = id;
        Precio = precio;
        CantidadStock = cantidadStock;
        NumeroCompras = numeroCompras;
        EsComercioJusto = esComercioJusto;

        if (id >= nextId)
        {
            nextId = id;
        }
    }

    public abstract void ModificarStock();

    public abstract void MostrarInformacion();
}
