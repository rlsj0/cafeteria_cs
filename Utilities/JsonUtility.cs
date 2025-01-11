using System.Text.Json;

namespace Utilities;

public class JsonUtility
{
    // Metodo para serializar y guardar
    // Este metodo es un "generic method": necesita que se especifique el
    // tipo del parametro al llamarlo

    public static void GuardarJson<T>(string ruta, T lista)
    {
        try
        {
            // Serializar
            string fichero = JsonSerializer.Serialize(lista);
            // Escribir el fichero
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine(fichero);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al serializar y escribir el JSON: {e}");
        }
    }

    // Metodo para deserializar y cargar (devuelve lista)
    public static T CargarJson<T>(string ruta)
    {
        try
        {
            // Chequear si existe la ruta
            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException($"No se encontró el fichero: {ruta}");
                // Cuando se recoja esta excepcion: decirle al usuario que aun no hay clientes,
                // pedidos, etc.
            }

            var linea = "";
            string fichero = "";
            using (StreamReader sr = new StreamReader(ruta))
            {
                while ((linea = sr.ReadLine()) != null)
                {
                    fichero = fichero + linea;
                }
            }
            T listaDeObjetos = JsonSerializer.Deserialize<T>(fichero)!;
            return listaDeObjetos;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al serializar y escribir el JSON: {e}");
        }
    }
}
