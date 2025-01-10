namespace Models;

public class Cafe : Producto
{
    public string Variedad { get; set; }
    public string Tipo { get; set; }

    // Constructor para crear un nuevo cafe
    public Cafe(decimal precio, int cantidadStock, bool esComercioJusto, string variedad, string tipo) : base(precio, cantidadStock, esComercioJusto)
    {
        Variedad = variedad;
        Tipo = tipo;
    }

    // Constructor para recuperar los datos del .json
    public Cafe(int id, decimal precio, int cantidadStock, int numeroCompras, bool esComercioJusto, string variedad, string tipo) : base(id, precio, cantidadStock, numeroCompras, esComercioJusto)
    {
        Variedad = variedad;
        Tipo = tipo;
    }

    public override void MostrarInformacion()
    {
        string siEsComercioJusto = EsComercioJusto == true ? " (fair trade)" : "";
        Console.WriteLine($"({Id}) {Variedad}{siEsComercioJusto}, {Tipo}:\n\t{Precio:C}\n\tCantidad disponible: {CantidadStock}");
    }

    public override void SumarStock(int numero)
    {
        if (numero < 0)
        {
            throw new ArgumentException("El número a sumar no puede ser negativo.");
        }
        else
        {
            CantidadStock = CantidadStock + numero;
        }

    }
    public override void RestarStock(int numero)
    {
        if (numero < 0)
        {
            throw new ArgumentException("El número a restar no puede ser negativo.");
        }
        else if (CantidadStock - numero < 0)
        {
            throw new InvalidOperationException("El stock no puede ser menor que 0.");
        }
        else
        {
            CantidadStock = CantidadStock - numero;
        }
    }

}
