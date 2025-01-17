using System.Text;
using System.Text.Json;
using Models;
using Repositories;

namespace Configuration;

public class Initialization
{

    public static void TodasInicializaciones()
    {
        InicializarAdmin();
        InicializarProductos();
        InicializarClientes();
    }


    public static void InicializarClientes()
    {
        if (FicheroInexistente(ClienteRepository.Ruta))
        {
            Cliente cliente = new Cliente("correo", "contrasena");
            var listaCliente = new List<Cliente>();
            listaCliente.Add(cliente);
            ClienteRepository.GuardarClientes(listaCliente);
        }
    }

    public static void InicializarAdmin()
    {
        if (!FicheroInexistente(AdminRepository.Ruta))
        {
            Console.WriteLine("returning");
            return;

        }

        // Por seguridad, no guardariamos la contrasena como string sino como
        // hash + salt. La contrasena es "password".
        // Por problemas con el parseo de string a byte[] con el formateo
        // adecuado, copio el texto del json

        string textoJson = """
[{"Id":1,"Correo":"correoAdmin","HashContrasena":"CBD174F8AE9DCCB0E8469D3AB6EED85C804FBB1B1B367B7DDEC80427AA240476539B193D4528933E1CB505DEFCF5DDA3177771484C380E24E1C65D27EFE444E7","SaltContrasena":"610wtwHFfbGVJAdVQoIDyHdK5TFvngUzrjvc7XIoKR2K64qBu+3AMGsA3CPPdjbUV8MPu0hHBG2qSSpQ2K10Nw==","FechaCreacion":"2025-01-14T19:57:58.0659402+01:00"}]
""";

        List<Admin> listaAdmin = JsonSerializer.Deserialize<List<Admin>>(textoJson)!;

        AdminRepository.GuardarAdmin(listaAdmin);
    }

    public static void InicializarProductos()
    {
        if (!FicheroInexistente(CafeRepository.Ruta))
        {
            return;
        }
        Cafe brasilLargo = new Cafe(2.00m, 15, true, "Brasil", "Largo");
        Cafe brasilLeche = new Cafe(2.5m, 10, true, "Brasil", "Con leche");
        Cafe brasilCorto = new Cafe(1.80m, 20, true, "Brasil", "Solo");
        Cafe jamaicaCapuccino = new Cafe(4, 2.00m, 15, 4, true, "Jamaica", "Capuccino");
        Cafe etiopiaCorto = new Cafe(1.2m, 40, true, "Etiopía", "Solo");

        var listaCafes = new List<Cafe>();
        listaCafes.Add(brasilLargo);
        listaCafes.Add(brasilLeche);
        listaCafes.Add(brasilCorto);
        listaCafes.Add(jamaicaCapuccino);
        listaCafes.Add(etiopiaCorto);
        CafeRepository.GuardarCafes(listaCafes);
    }

    public static bool FicheroInexistente(string ruta)
    {
        bool ficheroNoExiste = true;
        if (File.Exists(ruta))
        {
            ficheroNoExiste = false;
        }
        return ficheroNoExiste;
    }
}
