namespace Repositories;
using Utilities;
using Models;

public class CafeRepository
{
    public static string Ruta = "cafe.json";
    public static List<Cafe> CargarCafes()
    {
        List<Cafe> listaCafes = new List<Cafe>();
        try
        {
            listaCafes = JsonUtility.CargarJson<List<Cafe>>(Ruta);
        }
        catch (FileNotFoundException e)
        {
            // Gestionar excepcion desde la interfaz
            throw e;
        }
        return listaCafes;
    }
    public static void GuardarCafes(List<Cafe> listaCafes)
    {
        JsonUtility.GuardarJson<List<Cafe>>(Ruta, listaCafes);
    }
}
