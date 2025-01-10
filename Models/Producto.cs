namespace Models;

public abstract class Producto {
	public int Id {get; private set;}
	public static int nextId {get; set;} = 0;
	public decimal Precio {get; set}
	public int CantidadStock {get; set;}
	public int NumeroCompras {get; set;}
	public bool EsComercioJusto {get; private set;}

	// Constructor para crear un nuevo producto
	public class Producto (decimal precio, int cantidadStock, bool esComercioJusto) {
		Id = ++nextId;
		Precio = precio;
		CantidadStock = cantidadStock;
		NumeroCompras = 0;
		EsComercioJusto = esComercioJusto;
	}
	
	public ModificarStock();

	public MostrarInformacion();
}
