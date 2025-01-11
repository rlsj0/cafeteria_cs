namespace Repositories;
using Utilities;
using Models;

public class AdminRepository
{
    public static string Ruta = "admin.json";
    public static List<Admin> CargarAdmin()
    {
        List<Admin> listaAdmin = new List<Admin>();
        try
        {
            listaAdmin = JsonUtility.CargarJson<List<Admin>>(Ruta);
        }
        catch (FileNotFoundException e)
        {
            // Gestionar excepcion desde la interfaz
            throw e;
        }
        return listaAdmin;
    }
    public static void GuardarAdmin(List<Admin> listaAdmin)
    {
        JsonUtility.GuardarJson<List<Admin>>(Ruta, listaAdmin);
    }
}
