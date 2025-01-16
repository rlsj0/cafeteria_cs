namespace Repositories;
using Utilities;
using Models;

public class AdminRepository
{
    public static string Ruta = "admin.json";
    public static List<Admin> CargarAdmin()
    {
        List<Admin> listaAdmin = new List<Admin>();
        listaAdmin = JsonUtility.CargarJson<List<Admin>>(Ruta);
        return listaAdmin;
    }
    public static void GuardarAdmin(List<Admin> listaAdmin)
    {
        JsonUtility.GuardarJson<List<Admin>>(Ruta, listaAdmin);
    }
}
