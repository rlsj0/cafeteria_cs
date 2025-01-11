namespace ConsoleUI;
using Models;
using Services;

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
    private static string ErrorInput = "El input no es correcto. Inténtelo de nuevo";

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
                    // TODO: metodo para ver todos los productos.
                    // (1) Recuperar los productos del almacenamiento
                    // (2) foreach en el que Cafe.MostrarInformacion()

                    Console.WriteLine("\nPulsa enter para continuar.");
                    var inputFalso = Console.ReadLine();
                    continue;
                case 2:
                    MenuInicioSesion();
                    continue;
                case 3:
                    // TODO: llamar a un metodo que se encargue del registro
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

    public static void MenuInicioSesion()
    {
        var correo = "";
        var contrasena = "";
        // bucle para recoger input
        while (true)
        {
            Console.WriteLine("Introduzca su correo (pulse x para salir): ");
            correo = Console.ReadLine();
            if (string.IsNullOrEmpty(correo))
            {
                Console.WriteLine(ErrorInput);
                continue;
            }

            if (correo == "x")
            {
                return;
            }

            Console.WriteLine("Introduzca su contraseña (pulse x para salir): ");
            correo = Console.ReadLine();
            if (string.IsNullOrEmpty(contrasena))
            {
                Console.WriteLine(ErrorInput);
                continue;
            }

            if (contrasena == "x")
            {
                return;
            }

            // TODO: Coger los usuarios existentes y devolver listaUsuarios
            /*Sesion.CorreoExiste(correo, listaUsarios);*/
            // Crear nueva sesión
            // Chequear si la sesion es de usuario o de admin y llamar al menú correspondiente

        }

    }
}
