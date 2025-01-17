namespace ConsoleUI;
using Models;
using Services;
using Repositories;

public static class MenuPrincipal
{

    private static string TextoInicio = "MENU PRINCIPAL\n\n" +
            "Elige una opción:" +
            "\n\t(1) Ver productos" +
            "\n\t(2) Iniciar sesión" +
            "\n\t(3) Registrarse" +
            "\n\t(4) Salir" +
            "\nSu opción: ";

    private static int NumeroOpcionesInicio = 4;
    public static string ErrorInput = "El input no es correcto. Inténtelo de nuevo";

    public static void MenuInicio()
    {
        bool salirBucle = false;
        while (!salirBucle)
        {
            Console.WriteLine(TextoInicio);
            var input = Console.ReadLine();
            int inputParseado;
            if (!int.TryParse(input, out inputParseado) ||
                    inputParseado < 1 ||
                    inputParseado > NumeroOpcionesInicio)
            {
                Console.WriteLine(ErrorInput);
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
                    MenuInicioSesion();
                    continue;
                case 3:
                    if (PedirDatos(out string correo, out string contrasena))
                    {
                        try
                        {
                            Cliente.RegistrarCliente(correo, contrasena);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    continue;
                case 4:
                    Console.WriteLine("Saliendo...");
                    salirBucle = true;
                    continue;
                default:
                    Console.WriteLine(ErrorInput);
                    continue;
            }
        }
    }

    public static bool PedirDatos(out string correo, out string contrasena)
    {
        bool registroCorrecto = false;
        while (true)
        {
            correo = "";
            contrasena = "";
            Console.WriteLine("Introduzca su correo (pulse x para salir): ");
            // Pongo el "!" (null-forgiving operator) para evitar la advertencia.
            correo = Console.ReadLine()!;
            if (string.IsNullOrEmpty(correo))
            {
                Console.WriteLine(ErrorInput);
                continue;
            }

            if (correo == "x")
            {
                return registroCorrecto;
            }

            Console.WriteLine("Introduzca su contraseña (pulse x para salir): ");
            contrasena = Console.ReadLine()!;
            if (string.IsNullOrEmpty(contrasena))
            {
                Console.WriteLine(ErrorInput);
                continue;
            }

            if (contrasena == "x")
            {
                return registroCorrecto;
            }
            else
            {
                registroCorrecto = true;
                return registroCorrecto;
            }
        }
    }

    public static void MenuInicioSesion()
    {
        // bucle para recoger input
        while (MenuPrincipal.PedirDatos(out string correo, out string contrasena))
        {
            try
            {

                var listaClientes = ClienteRepository.CargarClientes();
                var listaAdmin = AdminRepository.CargarAdmin();

                List<Usuario> listaUsuarios = new List<Usuario>();
                foreach (Cliente cliente in listaClientes)
                {
                    listaUsuarios.Add(cliente);
                }

                foreach (Admin admin in listaAdmin)
                {
                    listaUsuarios.Add(admin);
                }

                if (!Sesion.CorreoExiste(correo, listaUsuarios))
                {
                    Console.WriteLine("Correo no registrado");
                    continue;
                }

                Sesion sesion = new Sesion(correo, contrasena, listaUsuarios);

                if (!sesion.EstaActiva)
                {
                    Console.WriteLine("Contraseña incorrecta");
                    continue;
                }

                if (sesion.EsAdmin)
                {
                    MenuAdmin.Menu(sesion);
                }
                else
                {
                    MenuCliente.Menu(sesion);
                }

                return;

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine("Parece que no hay usuarios registrados.");
                continue;
            }

        }
    }
}
