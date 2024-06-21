namespace CGE.Repositorios;
using Aplicacion;
using System.Security.Cryptography;
using System.Text;
using CGE.Aplicacion;
using Microsoft.VisualBasic;

public class RepositorioUsuario : IUsuarioRepositorio
{
    public CGEContext contexto;
    public RepositorioUsuario(CGEContext contexto)
    {
        this.contexto = contexto;
    }


    public List<Usuario> consultaUsuarios()
    {
        return contexto.Usuarios.ToList();
    }


    public void darDeAltaUsuario(Usuario u)
    {
        if (!BuscarAdmin())
        {
            u.permisos.Add(Permiso.ExpedienteAlta);
            u.permisos.Add(Permiso.ExpedienteBaja);
            u.permisos.Add(Permiso.ExpedienteModificacion);
            u.permisos.Add(Permiso.TramiteAlta);
            u.permisos.Add(Permiso.TramiteBaja);
            u.permisos.Add(Permiso.TramiteModificacion);
            u.permisos.Add(Permiso.PermisoModificacion);
            u.permisos.Add(Permiso.UsuarioBaja);
            u.permisos.Add(Permiso.UsuarioModificacion);
        }
        u.contraseña = FuncionHash(u.contraseña);
        contexto.Add(u);
        contexto.SaveChanges();
    }

    public void darDeBajaUsuario(int idBorrar)
    {
        var usuarioBorrar = contexto.Usuarios.Where(a => a.id == idBorrar).SingleOrDefault();
        if (usuarioBorrar == null)
        {
            new RepositorioException($"el usuario con id: {idBorrar} no se encuentra registrado en la pagina.");
        }
        contexto.Remove(usuarioBorrar);
        contexto.SaveChanges();
    }


    public void ModificarUsuario(Usuario usuario)
    {
        var usuarioModificar = contexto.Usuarios.Where(
                                u => u.id == usuario.id).SingleOrDefault();
        if(usuarioModificar.contraseña != usuario.contraseña){          
            usuarioModificar.contraseña = FuncionHash(usuarioModificar.contraseña);
        }
        usuarioModificar = usuario;
        contexto.SaveChanges();
    }


    public Usuario? ConsultaUsuario(int Id)
    {
        var usuario = contexto.Usuarios.Where(
                        u => u.id == Id).SingleOrDefault();
        return usuario;
    }


    public Usuario? RetornarUsuario(string mail, string contraseña)
    {
        var usuario = contexto.Usuarios.Where(
                                u => u.email == mail).SingleOrDefault();
        if (usuario != null)
        {
            string contraseña2 = FuncionHash(contraseña);
            if (usuario.contraseña == contraseña2)
            {
                return usuario;
            }
        }
        return null;
    }


    private bool BuscarAdmin()
    {
        return contexto.Usuarios.Any(u => u.id == 1);
    }


    private string FuncionHash(string contraseña)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Computar el hash - retorna un array de bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

            // Convertir el array de bytes a una cadena hexadecimal
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}