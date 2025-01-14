using Models;
using Repositories;
using Services;

namespace ConsoleUI;

public class MenuCliente
{
    public static string TextoMenu = "\nSelecciona una opción:" +
                            "\n\t(1) Ver productos" +
                            "\n\t(2) Buscar producto" +
                            "\n\t(3) Hacer pedido" +
                            "\n\t(4) Ver pedidos" +
                            "\n\t(5) Cerrar sesion";

    public static int NumeroOpcionesInicio = 5;

    public static void Menu(Sesion sesion)
    {
        if (sesion.UsuarioActivo == null)
        {
            return;
        }
        // Convertimos el Usuario de Sesion en Cliente.
        /*Cliente cliente = (Cliente)sesion.UsuarioActivo;*/
        int idUsuario = sesion.UsuarioActivo.Id;
        // Pillamos el cliente con el mismo ID
        var listaClientes = ClienteRepository.CargarClientes();
        Cliente cliente = listaClientes.FirstOrDefault(p => p.Id == idUsuario)!;

        while (sesion.EstaActiva)
        {
            Console.WriteLine(TextoMenu);
            var input = Console.ReadLine();
            int inputParseado;
            if (!int.TryParse(input, out inputParseado) ||
                    inputParseado < 1 ||
                    inputParseado > NumeroOpcionesInicio)
            {
                Console.WriteLine(MenuPrincipal.ErrorInput);
                continue;
            }

            switch (inputParseado)
            {
                case 1:
                    try
                    {
                        var listaCafes = CafeRepository.CargarCafes();
                        foreach (Cafe cafe in listaCafes)
                        {
                            cafe.MostrarInformacion();
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine("Error: " + e);
                        Console.WriteLine("Parece que aún no hay ningún producto.");
                    }

                    Console.WriteLine("\nPulsa enter para continuar.");
                    var inputFalso = Console.ReadLine();
                    continue;
                case 2:
                    if (!File.Exists(CafeRepository.Ruta))
                    {
                        Console.WriteLine("Parece que aún no hay productos");
                        continue;
                    }
                    var listaCafe = CafeRepository.CargarCafes();

                    Console.WriteLine("Introduce tu búsqueda: ");
                    var buscar = Console.ReadLine();
                    if (buscar == null)
                    {
                        Console.WriteLine("Input incorrecto.");
                        Console.WriteLine("Pulse enter para continuar");
                        Console.ReadLine();
                        continue;
                    }
                    var listaResultado = cliente.BuscarCafe(buscar, listaCafe);
                    if (!listaResultado.Any())
                    {
                        Console.WriteLine("No hay resultados");
                        Console.WriteLine("Pulse enter para continuar");
                        Console.ReadLine();
                        continue;
                    }
                    Console.WriteLine("\nRESULTADOS:\n");

                    foreach (Cafe cafe in listaResultado)
                    {
                        cafe.MostrarInformacion();
                    }
                    Console.WriteLine("Pulse enter para continuar");
                    Console.ReadLine();
                    continue;

                case 3:
                    if (!File.Exists(CafeRepository.Ruta))
                    {
                        Console.WriteLine("Error: Parece que aún no hay productos");
                        continue;
                    }
                    var listaDeCafe = CafeRepository.CargarCafes();
                    List<int> listaDeIds = new List<int>();

                    List<Tuple<Cafe, int>> listaDeProductos = new List<Tuple<Cafe, int>>();
                    while (true)
                    {
                        Console.WriteLine("\nPRODUCTOS\n");

                        foreach (Cafe cafe in listaDeCafe)
                        {
                            cafe.MostrarInformacion();
                            listaDeIds.Add(cafe.Id);
                        }
                        Console.WriteLine("\nIntroduzca el ID del producto que desea comprar (introduzca x para cancelar): ");
                        var idParaComprar = Console.ReadLine();
                        if (idParaComprar == null)
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }
                        else if (idParaComprar == "x")
                        {
                            continue;
                        }
                        if (!int.TryParse(idParaComprar, out int opcionParseada))
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }

                        bool idExiste = false;
                        bool hayExistencias = false;
                        Cafe cafeSeleccionado;
                        foreach (int numero in listaDeIds)
                        {
                            if (numero != opcionParseada)
                            {
                                continue;
                            }
                            idExiste = true;
                            cafeSeleccionado = listaDeCafe.FirstOrDefault(cafe => cafe.Id == opcionParseada)!;
                            if (cafeSeleccionado.CantidadStock > 0)
                            {
                                hayExistencias = true;
                            }
                            break;
                        }
                        if (!idExiste)
                        {
                            Console.WriteLine("El id no es correcto");
                            continue;
                        }
                        if (!hayExistencias)
                        {

                            Console.WriteLine("No hay existencias de este producto.");
                            Console.WriteLine("Pulsa enter para continuar.");
                            Console.ReadLine();
                            continue;
                        }

                        cafeSeleccionado = listaDeCafe.FirstOrDefault(cafe => cafe.Id == opcionParseada)!;
                        Console.WriteLine("Introduzca la cantidad a comprar (pulse x para cancelar): ");
                        var cantidadParaComprar = Console.ReadLine();
                        if (cantidadParaComprar == null || cantidadParaComprar == "x")
                        {
                            break;
                        }

                        if (!int.TryParse(cantidadParaComprar, out int cantidad) || cantidad <= 0)
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            break;
                        }
                        if (cafeSeleccionado.CantidadStock < cantidad)
                        {
                            Console.WriteLine("No hay tantas existencias");
                            Console.WriteLine("Pulsa enter para continuar.");
                            Console.ReadLine();
                            continue;
                        }

                        Tuple<Cafe, int> tuplaCafeCantidad = new Tuple<Cafe, int>(cafeSeleccionado, cantidad);
                        listaDeProductos.Add(tuplaCafeCantidad);

                        var otroProducto = "";

                        while (true)
                        {
                            Console.Write("¿Desea añadir otro producto? (y/n) ");
                            otroProducto = Console.ReadLine();
                            if (otroProducto == null || (otroProducto != "y" && otroProducto != "n"))
                            {
                                Console.WriteLine(MenuPrincipal.ErrorInput);
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (otroProducto != "y")
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }

                    }
                    if (listaDeProductos == null || !listaDeProductos.Any())
                    {
                        Console.WriteLine("No ha comprado ningún producto");
                    }
                    else
                    {
                        Console.WriteLine("\nEste es su pedido:\n");
                        foreach (var tupla in listaDeProductos)
                        {
                            string nombre = $"{tupla.Item1.Variedad}, {tupla.Item1.Tipo}";
                            string precioUnitario = $"{tupla.Item1.Precio:C}";
                            decimal precioProdutco = tupla.Item2 * tupla.Item1.Precio;
                            Console.WriteLine($"- {nombre} ({precioUnitario}) x {tupla.Item2} =  {precioProdutco:C}");
                        }
                        Console.WriteLine("¿Desea proceder con el pedido? (y/n)");
                        var proceder = Console.ReadLine();
                        if (proceder == null || proceder != "y")
                        {
                            continue;
                        }

                        bool clienteSatisfecho = true;
                        Console.WriteLine("¿Está satisfecho con el servicio? (y/n)");
                        var satisfaccion = Console.ReadLine();
                        if (satisfaccion == null || satisfaccion != "y")
                        {
                            clienteSatisfecho = false;
                        }

                        // Elimino el cliente antes de cambiar sus valores
                        var totalClientes = ClienteRepository.CargarClientes();
                        // No funciona totalClientes.Remove(cliente); (quizas porque
                        // estaria comparando la referencia)
                        var item = totalClientes.SingleOrDefault(x => x.Id == cliente.Id);
                        if (item != null)
                        {
                            totalClientes.Remove(item);
                        }

                        Pedido nuevoPedido = cliente.HacerPedido(cliente.Id, listaDeProductos, clienteSatisfecho);

                        var listaTotalPedidos = PedidoRepository.CargarPedidos();
                        listaTotalPedidos.Add(nuevoPedido);
                        PedidoRepository.GuardarPedidos(listaTotalPedidos);

                        // Guardar de nuevo este cliente con el HistoricoPedidos nuevo
                        totalClientes.Add(cliente);
                        ClienteRepository.GuardarClientes(totalClientes);
                    }
                    continue;
                case 4:
                    cliente.VerPedidos();
                    Console.WriteLine("\nPulsa enter para continuar");
                    var falseInput = Console.ReadLine();
                    continue;
                case 5:
                    Console.WriteLine("Cerrando sesión...");
                    sesion.CerrarSesion();
                    continue;

                default:
                    continue;
            }

        }
    }
}
