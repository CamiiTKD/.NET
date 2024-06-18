namespace CGE.Repositorios;

using Aplicacion;


public class Repositorio : IExpedienteRepositorio, ITramiteRepositorio, IUsuarioRepositorio
{
    readonly string nombreArchivoExpedientes = "..\\CGE.Repositorios\\BaseDeDatos_Expedientes.txt";
    readonly string nombreArchivoTramites = "..\\CGE.Repositorios\\BaseDeDatos_Tramites.txt";
    public CGEContext contexto;
    public static int idExpediente;
    public static int idTramite;
    public Repositorio(CGEContext contexto)
    {
        idExpediente = buscarUltimoIdExpediente();
        idTramite = buscarUltimoIdTramite();
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
        idExpediente = buscarUltimoIdExpediente() + 1;
        expediente.Id = idExpediente;
        using var sw = new StreamWriter(nombreArchivoExpedientes, true);
        sw.WriteLine(expediente.Id);
        sw.WriteLine(expediente.caratula);
        sw.WriteLine(expediente.creacion);
        sw.WriteLine(expediente.ultimaModificacion);
        sw.WriteLine(expediente.IdUsuario);
        sw.WriteLine(expediente.estado);
    }
    public void darDeBajaExpediente(int IdExpediente)
    {
        string archivoTemporal = Path.GetTempFileName();
        string ID = Convert.ToString(IdExpediente);
        string? datoLeido;
        using (StreamReader sr = new StreamReader(nombreArchivoExpedientes))
        using (StreamWriter sw = new StreamWriter(archivoTemporal))
            while (!sr.EndOfStream)
            {
                datoLeido = sr.ReadLine();
                if (datoLeido == null)
                {
                    throw new Exception("No se leyó ningún dato");
                }
                if (datoLeido.Equals(ID)) for (int i = 0; i < 5; i++) sr.ReadLine();
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sw.WriteLine(datoLeido);
                        datoLeido = sr.ReadLine();
                    }
                    sw.WriteLine(datoLeido);
                }
            }
        File.Copy(archivoTemporal, nombreArchivoExpedientes, true);
        File.Delete(archivoTemporal);
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

    public int buscarUltimoIdExpediente() //Se sigue necesitando en entrega final?
    {
        string? id = null; //le asignamos null porque sino tira error de compilacion en la variable id.
        using (StreamReader sr = new StreamReader(nombreArchivoExpedientes))
        {
            string? str = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while (!sr.EndOfStream)
            {
                id = sr.ReadLine();
                for (int i = 0; i < 5; i++)
                {
                    sr.ReadLine();
                }
            }
            if (id != null)
            {
                return int.Parse(id);
            }
            new RepositorioException("no leyó nada");
            return -1; //no llega acá, sale en la exception
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
        List<Tramite> lista = new List<Tramite>();
        using (StreamReader sr = new StreamReader(nombreArchivoTramites))
        {
            string? line;
            while (!sr.EndOfStream)
            {
                int idTramite = int.Parse(sr.ReadLine() ?? "");
                int idExpediente = int.Parse(sr.ReadLine() ?? "");
                line = sr.ReadLine();
                while ((!sr.EndOfStream) && (line != etiqueta.ToString()))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        sr.ReadLine();
                    }
                    if (!sr.EndOfStream)
                    {
                        idTramite = int.Parse(sr.ReadLine() ?? "");
                        idExpediente = int.Parse(sr.ReadLine() ?? "");
                        line = sr.ReadLine();
                    }
                }
                if (!sr.EndOfStream)
                {
                    EtiquetaTramite etq = Enum.Parse<EtiquetaTramite>(line ?? "");
                    string contenido = sr.ReadLine() ?? "";
                    DateTime creacion = DateTime.Parse(sr.ReadLine() ?? "");
                    DateTime ultimaMod = DateTime.Parse(sr.ReadLine() ?? "");
                    int idUsuario = int.Parse(sr.ReadLine() ?? "");
                    Tramite tramite = new Tramite(idTramite, idExpediente, etq, contenido, creacion, ultimaMod, idUsuario);
                    lista.Add(tramite);
                }
            }
            if (lista.Count() == 0)
            {
                throw new RepositorioException($"No existe ningun tramite con la etiqueta {etiqueta}");
            }
        }
        return lista;
    }
    public void darDeAltaTramite(Tramite tramite)
    {
        contexto.Add(tramite);
        contexto.SaveChanges();
        // idTramite = buscarUltimoIdTramite() + 1; TENGO QUE SEGUIR INCREMENTANDO O ES AUTOMÁTICO EN SQLITE?
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
        { //Si lo encuentra modifico lo pertinente.
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

    private int buscarUltimoIdTramite()
    {
        string? id = null;
        using (StreamReader sr = new StreamReader(nombreArchivoTramites))
        {
            string? str = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while (!sr.EndOfStream)
            {
                id = sr.ReadLine();
                for (int i = 0; i < 6; i++)
                {
                    sr.ReadLine();
                }
            }
            if (id != null)
            {
                return int.Parse(id);
            }
            new RepositorioException("no leyó nada");
            return -1; //no llega acá, sale en la exception
        }
    }

    // >>>>>>>>>>>>>>>>>>>>>>>USUARIOS<<<<<<<<<<<<<<<<<<<<<<<<

    public List<Usuario> consultaUsuarios()
    {
        Console.WriteLine("Entró al método de consulta usuarios.");
        return contexto.Usuarios.ToList();
    }
    public void darDeAltaUsuario(Usuario u)
    {
        contexto.Add(u);
        //falta codificar la contraseña
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

    }
}