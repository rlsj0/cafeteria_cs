namespace Services;
using Models;

public class Sesion
{
    public bool EstaActiva { get; set; }
    public Usuario? UsuarioActivo { get; set; }
    public bool EsAdmin { get; set; }

    public Sesion(string correo, string contrasena, List<Usuario> listaUsuarios)
    {
        EstaActiva = false;
        EsAdmin = false;
        // Inicializo la  variable con un valor que no se va a usar
        Usuario usuarioLogueando = new Admin("correo", "contrasena");
        if (!CorreoExiste(correo, listaUsuarios))
        {
            return;
        }
        else
        {
            foreach (Usuario usuario in listaUsuarios)
            {
                if (usuario.Correo == correo)
                {
                    usuarioLogueando = usuario;
                }
            }
        }

        if (!usuarioLogueando.CompararHash(contrasena))
        {
            return;
        }

        if (usuarioLogueando.GetType() == typeof(Admin))
        {
            EsAdmin = true;
        }

        EstaActiva = true;
        UsuarioActivo = usuarioLogueando;
    }


    public void CerrarSesion()
    {
        EstaActiva = false;
    }

    public static bool CorreoExiste(string correo, List<Usuario> listaUsuarios)
    {
        bool contieneCorreo = false;

        foreach (Usuario usuario in listaUsuarios)
        {
            if (usuario.Correo == correo)
            {
                contieneCorreo = true;
            }
        }
        return contieneCorreo;
    }
}
