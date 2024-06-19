namespace CGE.Repositorios;

using Aplicacion;


public class Repositorio : IExpedienteRepositorio, ITramiteRepositorio, IUsuarioRepositorio
{
    public CGEContext contexto;
    public static int idExpediente;
    public static int idTramite;
    public Repositorio(CGEContext contexto)
    {
        this.contexto = contexto;
    }

    // >>>>>>>>>>>>>>>>>>>>>>>EXPEDIENTES<<<<<<<<<<<<<<<<<<<<<<<<

    public Expediente? consultaPorId(int id)
    {
        var expedienteEncontrado = contexto.Expedientes.Find(id);
        if (expedienteEncontrado != null)
        {
            Expediente expe = new Expediente(expedienteEncontrado) { Tramites = getTramitesPorIdExpediente(id) };
            return expe;
        }
        else
        {
            return null;
        }
    }

    public List<Expediente> consultaTodos()
    {
        List<Expediente> ListaExpedientes = contexto.Expedientes.ToList();
        return ListaExpedientes;
    }

    public void darDeAltaExpediente(Expediente expediente)
    {
        contexto.Add(expediente);
        contexto.SaveChanges();
    }

    public void darDeBajaExpediente(int IdExpediente)
    {
        var expediente = contexto.Expedientes.Find(IdExpediente);
        if (expediente != null)
        {
            contexto.Expedientes.Remove(expediente);
            contexto.SaveChanges();
        }
        else
        {
            new RepositorioException
            ($"No existe el expediente con ID: {IdExpediente}. No es posible eliminar");
        }
    }

    public void ModificarExpediente(Expediente expediente)
    { //Revisar, muchos errores en Ejecutar y raro de modificar sin UI. No estoy seguro de como hcaer.
        var expedienteExistente = contexto.Expedientes.Find(expediente.Id);
        if (expedienteExistente != null)
        {
            expedienteExistente.Caratula = expediente.Caratula;
            expedienteExistente.UltimaModificacion = expediente.UltimaModificacion;
            expedienteExistente.IdUsuario = expediente.IdUsuario;
            expedienteExistente.Estado = expediente.Estado;
            contexto.SaveChanges(); //REVISAR AL FINAL.
        }
        else
        {
            new RepositorioException
            ($"No existe el expediente con ID: {expediente.Id}, que desea modificar.");
        }
    }

    // >>>>>>>>>>>>>>>>>>>>>>>TRAMITES<<<<<<<<<<<<<<<<<<<<<<<<

    public Tramite? consultaPorTramiteId(int idTramite)
    {
        var tramite = contexto.Tramites.Find(idTramite);
        if (tramite != null)
        {
            Tramite tramiteEncontrado = new Tramite(tramite);
            return tramiteEncontrado;
        }
        else
        {
            return null;
        }
    }

    public List<Tramite> ConsultaEtiqueta(EtiquetaTramite etiqueta)
    {
        List<Tramite> listaTramites = contexto.Tramites.
                                Where(t => t.Tipo == etiqueta)
                                .ToList();
        return listaTramites;
    }

    public void darDeAltaTramite(Tramite tramite)
    {
        contexto.Add(tramite);
        contexto.SaveChanges();
    }
    public void darDeBajaTramite(int idTramite)
    {
        var tramite = contexto.Tramites.Find(idTramite); //No requiere indicar tipo Tramite porque asume el tipo de retorno.
        if (tramite != null)
        {
            contexto.Tramites.Remove(tramite);
            contexto.SaveChanges();
        }
        else
        {
            new RepositorioException($"No existe el tramite con id: {idTramite}, que desea eliminar.");
        }
    }


    public void ModificarTramite(Tramite tramite)
    // NO SE PUEDE hacer tramiteExistente=tramite porque 
    //solo modificaríamos la referencia y no el tramite en la base de datos!!
    {
        var tramiteExistente = contexto.Tramites.Find(tramite.Id); //Si no lo encuentra devuelve null.
        if (tramiteExistente != null)
        { //Si lo encuentra modifico lo pertinente, lo demás no necesita modificarse.
            tramiteExistente.Tipo = tramite.Tipo;
            tramiteExistente.Contenido = tramite.Contenido;
            tramiteExistente.UltimaModificacion = tramite.UltimaModificacion;
            tramiteExistente.IdUsuario = tramite.IdUsuario;
            contexto.SaveChanges(); //Reflejo los cambios.
        }
    }


    // --------------------------------PRIVATE----------------------------------------

    private List<Tramite> getTramitesPorIdExpediente(int id)
    {
        List<Tramite> listaTramites = contexto.Tramites
                                .Where(tramite => tramite.ExpedienteId == id)
                                .ToList();
        return listaTramites;
    }


    // >>>>>>>>>>>>>>>>>>>>>>>USUARIOS<<<<<<<<<<<<<<<<<<<<<<<<

    public List<Usuario> consultaUsuarios()
    {
        Console.WriteLine("Entró al método de consulta usuarios.");
        return contexto.Usuarios.ToList();
    }
    public void darDeAltaUsuario(Usuario u)
    {
        u.contraseña=FuncionHash(u.contraseña);
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
        usuarioModificar = usuario;
        contexto.SaveChanges();
        //NOSE SI ANDA XD
    }
    public Usuario? ConsultaUsuario(int Id)
    {
        var usuario = contexto.Usuarios.Where(
                        u => u.id == Id).SingleOrDefault();
        return usuario;
    }
    public Usuario RetornarUsuario (string mail,string contraseña){
        var usuario = contexto.Usuarios.Where(
                                u=> u.email==mail).SingleOrDefault();
        contraseña=FuncionHash(contraseña);
        if (usuario.contraseña==contraseña){
            return usuario;
        }
        return null;
    }
    private string FuncionHash (string contraseña){
        using (SHA256 sha256Hash = SHA256.Create())
        {
        // Computar el hash - retorna un array de bytes
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(u.contraseña));

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