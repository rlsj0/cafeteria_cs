using Services;
using Repositories;
using Models;

namespace ConsoleUI;

public class MenuAdmin
{
    public static string TextoMenu = "\nSelecciona una opción:" +
                            "\n\t(1) Ver productos" +
                            "\n\t(2) Añadir producto" +
                            "\n\t(3) Editar producto" +
                            "\n\t(4) Ver total de pedidos" +
                            "\n\t(5) Ver clientes" +
                            "\n\t(6) Eliminar cliente" +
                            "\n\t(7) Cerrar sesión";

    public static int NumeroOpcionesInicio { get; private set; } = 7;

    public static void Menu(Sesion sesion)
    {
        if (sesion.UsuarioActivo == null)
        {
            return;
        }

        // Convertimos el Usuario de Sesion en User.
        int idUsuario = sesion.UsuarioActivo.Id;
        // Pillamos el usuario con el mismo ID
        var listaUsuarios = AdminRepository.CargarAdmin();
        Admin admin = listaUsuarios.FirstOrDefault(p => p.Id == idUsuario)!;

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
                    Cafe cafeNuevo;
                    var tipo = "";
                    var variedad = "";
                    decimal precio;
                    int cantidadStock;
                    bool esComercioJusto = false;
                    while (true)
                    {
                        Console.Write("\nEscribe la variedad: ");
                        variedad = Console.ReadLine();
                        if (variedad == null || variedad == "")
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }
                        break;
                    }
                    while (true)
                    {
                        Console.Write("\nEscribe el tipo: ");
                        tipo = Console.ReadLine();
                        if (tipo == null || tipo == null)
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }
                        break;
                    }
                    while (true)
                    {
                        Console.Write("\nEscribe el precio: ");
                        var inputPrecio = Console.ReadLine();
                        if (!decimal.TryParse(inputPrecio, out precio) || precio <= 0)
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }
                        break;
                    }
                    while (true)
                    {
                        Console.Write("\nEscribe la cantidad en stock: ");
                        var inputCantidad = Console.ReadLine();
                        if (!int.TryParse(inputCantidad, out cantidadStock) || cantidadStock < 0)
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }
                        break;
                    }
                    bool repetirBucle = false;
                    while (repetirBucle)
                    {
                        Console.Write("\n¿Es comercio justo? (y/n) ");
                        var inputComercioJusto = Console.ReadLine();
                        switch (inputComercioJusto)
                        {
                            case "y":
                                esComercioJusto = true;
                                continue;
                            case "n":
                                esComercioJusto = false;
                                continue;
                            default:
                                Console.WriteLine(MenuPrincipal.ErrorInput);
                                repetirBucle = true;
                                break;
                        }
                    }
                    cafeNuevo = new Cafe(precio, cantidadStock, esComercioJusto, variedad, tipo);
                    var listaDeCafes = CafeRepository.CargarCafes();
                    listaDeCafes.Add(cafeNuevo);
                    CafeRepository.GuardarCafes(listaDeCafes);
                    Console.WriteLine($"\"{cafeNuevo.Variedad}, {cafeNuevo.Tipo}\" fue añadido correctamente");
                    continue;
                case 3:
                    var listaNuevaCafes = CafeRepository.CargarCafes();
                    List<int> listaIds = new List<int>();
                    int idElegido;
                    if (!listaNuevaCafes.Any())
                    {
                        Console.WriteLine("Parece que aún no hay productos.");
                        continue;
                    }
                    foreach (Cafe cafe in listaNuevaCafes)
                    {
                        cafe.MostrarInformacion();
                        listaIds.Add(cafe.Id);
                    }
                    while (true)
                    {
                        Console.Write("Escriba el ID del Producto que desea editar: ");
                        var inputId = Console.ReadLine();
                        if (!int.TryParse(inputId, out idElegido) || !listaIds.Contains(idElegido))
                        {
                            Console.WriteLine(MenuPrincipal.ErrorInput);
                            continue;
                        }
                        break;
                    }
                    Console.WriteLine("¿Qué deseas hacer con el item seleccionado?" +
                            "\n\t(a) Modificar stock" +
                            "\n\t(b) Modificar precio" +
                            "\n\t(c) Borrrar" +
                            "\n\t(d) Salir");
                    string opcionElegida = Console.ReadLine()!;
                    var listaFiltrada = new List<Cafe>();
                    switch (opcionElegida)
                    {
                        case "a":
                            Console.WriteLine("¿Qué desea hacer?" +
                                    "\n\t(a) Aumentar el stock" +
                                    "\n\t(b) Disminuir el stock" +
                                    "\n\t(c) Salir");
                            var opcionStock = Console.ReadLine();
                            var inputCuantoStock = "x";
                            int cuantoStock;
                            switch (opcionStock)
                            {
                                case "a":
                                    while (true)
                                    {
                                        Console.Write("¿Cuántas unidades sumar? ");
                                        inputCuantoStock = Console.ReadLine();
                                        if (!int.TryParse(inputCuantoStock, out cuantoStock))
                                        {
                                            Console.WriteLine(MenuPrincipal.ErrorInput);
                                            continue;
                                        }
                                        break;
                                    }
                                    foreach (Cafe cafe in listaNuevaCafes)
                                    {
                                        if (cafe.Id == idElegido)
                                        {
                                            try
                                            {
                                                cafe.SumarStock(cuantoStock);
                                            }
                                            catch (ArgumentException e)
                                            {
                                                Console.WriteLine(e);
                                                break;
                                            }
                                        }
                                        listaFiltrada.Add(cafe);
                                    }
                                    CafeRepository.GuardarCafes(listaNuevaCafes);
                                    continue;
                                case "b":
                                    while (true)
                                    {
                                        Console.Write("¿Cuántas unidades restar? ");
                                        inputCuantoStock = Console.ReadLine();
                                        if (!int.TryParse(inputCuantoStock, out cuantoStock))
                                        {
                                            Console.WriteLine(MenuPrincipal.ErrorInput);
                                            continue;
                                        }
                                        break;
                                    }
                                    foreach (Cafe cafe in listaNuevaCafes)
                                    {
                                        if (cafe.Id == idElegido)
                                        {
                                            try
                                            {
                                                cafe.RestarStock(cuantoStock);
                                            }
                                            catch (ArgumentException e)
                                            {
                                                Console.WriteLine(e);
                                                break;
                                            }
                                            catch (InvalidOperationException a)
                                            {
                                                Console.WriteLine(a);
                                                break;
                                            }
                                        }
                                        listaFiltrada.Add(cafe);
                                    }
                                    CafeRepository.GuardarCafes(listaNuevaCafes);
                                    continue;
                                default:
                                    continue;
                            }
                        case "b":
                            string inputNuevoPrecio = "x";
                            decimal nuevoPrecio = 0;
                            while (true)
                            {
                                Console.Write("Nuevo precio (x para cancelar): ");
                                inputNuevoPrecio = Console.ReadLine()!;
                                if (inputNuevoPrecio == "x")
                                {
                                    break;
                                }
                                if (!decimal.TryParse(inputNuevoPrecio, out nuevoPrecio) || inputNuevoPrecio == null)
                                {
                                    Console.WriteLine(MenuPrincipal.ErrorInput);
                                    continue;
                                }
                                break;
                            }
                            if (inputNuevoPrecio == "x" || nuevoPrecio <= 0)
                            {
                                Console.WriteLine(MenuPrincipal.ErrorInput);
                                continue;
                            }
                            foreach (Cafe cafe in listaNuevaCafes)
                            {
                                if (cafe.Id == idElegido)
                                {
                                    admin.ModificarPrecio(cafe, nuevoPrecio);
                                }
                                listaFiltrada.Add(cafe);
                            }
                            CafeRepository.GuardarCafes(listaFiltrada);
                            Console.WriteLine("Cambios guardados.");
                            continue;
                        case "c":
                            foreach (Cafe cafe in listaNuevaCafes)
                            {
                                if (cafe.Id == idElegido)
                                {
                                    continue;
                                }
                                listaFiltrada.Add(cafe);
                            }
                            CafeRepository.GuardarCafes(listaFiltrada);
                            continue;
                        case "d":
                            continue;
                        default:
                            continue;
                    }
                case 4:
                    if (!File.Exists(PedidoRepository.Ruta))
                    {
                        Console.WriteLine("Parece que no hay pedidos");
                        continue;
                    }
                    var listaTotalPedidos = PedidoRepository.CargarPedidos();
                    if (listaTotalPedidos == null || !listaTotalPedidos.Any())
                    {
                        Console.WriteLine("Parece que no hay pedidos");
                        continue;
                    }
                    Console.WriteLine("\nPEDIDOS: \n");
                    foreach (Pedido pedido in listaTotalPedidos)
                    {
                        pedido.MostrarDetalles();
                    }
                    continue;
                case 5:
                    MostrarClientes(out List<Cliente> lista);
                    continue;
                case 6:
                    MostrarClientes(out List<Cliente> listaDeClientes);
                    if (listaDeClientes == null || !listaDeClientes.Any())
                    {
                        continue;
                    }
                    Console.WriteLine("Escriba el input del cliente que desea eliminar (introduzca x para cancelar)");
                    var opcion = Console.ReadLine();
                    if (opcion == null || opcion == "x")
                    {
                        continue;
                    }
                    if (!int.TryParse(opcion, out int idCliente))
                    {
                        Console.WriteLine("Input incorrecto");
                    }
                    bool idEncontrado = false;
                    Cliente clienteIdentificado;
                    foreach (Cliente cliente in listaDeClientes)
                    {
                        Console.WriteLine(cliente.Id);
                        if (cliente.Id == idCliente)
                        {
                            Console.WriteLine("Encontrado");
                            idEncontrado = true;
                            clienteIdentificado = cliente;
                            listaDeClientes.Remove(clienteIdentificado);
                            ClienteRepository.GuardarClientes(listaDeClientes);
                            Console.WriteLine($"El cliente con ID {cliente.Id}" +
                                    $" y correo {cliente.Correo} ha sido eliminado");
                            break;
                        }
                    }
                    if (!idEncontrado)
                    {
                        Console.WriteLine("No existe ningún ciente con ese ID");
                        continue;
                    }
                    continue;
                case 7:
                    Console.WriteLine("Cerrando sesión...");
                    sesion.CerrarSesion();
                    continue;
                default:
                    continue;
            }
        }
    }

    public static void MostrarClientes(out List<Cliente> listaTotalClientes)
    {
        listaTotalClientes = new List<Cliente>();
        if (!File.Exists(ClienteRepository.Ruta))
        {
            Console.WriteLine("Parece que no hay clientes");
            return;
        }
        listaTotalClientes = ClienteRepository.CargarClientes();
        if (listaTotalClientes == null || !listaTotalClientes.Any())
        {
            Console.WriteLine("Parece que no hay clientes");
            return;
        }
        Console.WriteLine("\nCLIENTES: \n");
        foreach (Cliente cliente in listaTotalClientes)
        {
            Console.WriteLine("ID: " + cliente.Id +
                    "\n\tCorreo: " + cliente.Correo +
                    "\n\tFecha creación: " + cliente.FechaCreacion);
        }
        return;
    }
}
