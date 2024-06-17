namespace CGE.Repositorios;

using Aplicacion;


public class Repositorio : IExpedienteRepositorio, ITramiteRepositorio
{
    readonly string nombreArchivoExpedientes = "..\\CGE.Repositorios\\BaseDeDatos_Expedientes.txt";
    readonly string nombreArchivoTramites = "..\\CGE.Repositorios\\BaseDeDatos_Tramites.txt";
    public static int idExpediente;
    public static int idTramite;
    public Repositorio()
    {
        idExpediente = buscarUltimoIdExpediente();
        idTramite = buscarUltimoIdTramite();
    }
    //................EXPEDIENTE...................
    public Expediente consultaPorId(int id)
    {
        Expediente expediente = new Expediente();
        using (StreamReader sr = new StreamReader(nombreArchivoExpedientes))
        {
            string? line;
            bool encontre = false;
            while ((!sr.EndOfStream) && (!encontre))
            {
                line = sr.ReadLine();
                if (line != null){
                    if (!line.Equals(id.ToString())) for (int i = 0; i < 5; i++) sr.ReadLine();
                    else
                    {
                        expediente.Id = int.Parse(line);
                        expediente.caratula = sr.ReadLine() ?? "";
                        expediente.creacion = DateTime.Parse(sr.ReadLine() ?? "");
                        expediente.ultimaModificacion = DateTime.Parse(sr.ReadLine() ?? "");
                        expediente.IdUsuario = int.Parse(sr.ReadLine() ?? "");
                        expediente.estado = Enum.Parse<EstadoExpediente>(sr.ReadLine() ?? "");        
                        encontre = true;
                    }
                }
            }
            if (encontre)
            {
                expediente.tramites = getTramitesPorIdExpediente(expediente.Id);
                return expediente;
            }
            else throw new RepositorioException($"El expediente con la ID {id} no existe");
        }
    }

    public List<Expediente> consultaTodos()
    {
        List<Expediente> ListaExpedientes = new List<Expediente>();
        using var sr = new StreamReader(nombreArchivoExpedientes);
        while (!sr.EndOfStream)
        {
            int id = int.Parse(sr.ReadLine() ?? "");
            string caratula = sr.ReadLine() ?? "";
            DateTime creacion = DateTime.Parse(sr.ReadLine() ?? "");
            DateTime ultimaModificacion = DateTime.Parse(sr.ReadLine() ?? "");
            int idUsuario = int.Parse(sr.ReadLine() ?? "");
            EstadoExpediente estado = Enum.Parse<EstadoExpediente>(sr.ReadLine() ?? "");
            var expediente = new Expediente(id, caratula, creacion, ultimaModificacion, idUsuario, estado);
            ListaExpedientes.Add(expediente);
        }
        if (ListaExpedientes.Count() == 0)
        {
            throw new RepositorioException("No existe ningun expediente.");
        }
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
                if(datoLeido == null){
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
    {
        string archivoTemporal = Path.GetTempFileName();
        string idExpediente = Convert.ToString(expediente.Id);
        string? datoLeido;
        using (StreamReader sr = new StreamReader(nombreArchivoExpedientes))
        using (StreamWriter sw = new StreamWriter(archivoTemporal))
            while (!sr.EndOfStream)
            {
                datoLeido = sr.ReadLine(); //Leo ID
                if (datoLeido == null){
                    throw new Exception("No se leyó ningún dato"); //no podemos utilizar el RepositorioException porque sigue tirando el warning
                }
                if (datoLeido.Equals(idExpediente)) //Es el expediente D:
                {
                    sw.WriteLine(datoLeido);    //Escribo la ID.
                    datoLeido = sr.ReadLine();  //Muevo el puntero (caratula).
                    sw.WriteLine(expediente.caratula);  // Actualizo caratula.
                    datoLeido = sr.ReadLine(); // Muevo el puntero (creacion).
                    sw.WriteLine(datoLeido);  //No modifico
                    datoLeido = sr.ReadLine(); // Muevo el puntero (ultima modificacion).
                    sw.WriteLine(Convert.ToString(expediente.ultimaModificacion)); //Actualizo.
                    datoLeido = sr.ReadLine(); //Muevo el puntero (ultima ID).
                    sw.WriteLine(Convert.ToString(expediente.IdUsuario)); //Actualizo.
                    datoLeido = sr.ReadLine(); //Muevo el puntero (estado).
                    sw.WriteLine(Convert.ToString(expediente.estado)); //Actualizo el estado.
                }
                else    //No es el expediente así que escribo todo :D
                {
                    sw.WriteLine(datoLeido);
                    for (int i = 0; i < 5; i++)
                    {
                        datoLeido = sr.ReadLine();
                        sw.WriteLine(datoLeido);
                    }
                }
            }
        File.Copy(archivoTemporal, nombreArchivoExpedientes, true); // Copio el temporal cargado con los nuevos datos en el anterior (true indica que se sobreescribe).
        File.Delete(archivoTemporal); //Elimino el temporal para no tener copias innecesarias.
    }
    public int buscarUltimoIdExpediente(){
        string? id = null; //le asignamos null porque sino tira error de compilacion en la variable id.
        using (StreamReader sr = new StreamReader(nombreArchivoExpedientes)){
            string? str = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(str)){
                return 0;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while(!sr.EndOfStream)
            {
                id = sr.ReadLine();
                for(int i=0;i<5;i++){
                    sr.ReadLine();
                }
            }
            if(id != null){
                return int.Parse(id);
            }
            new RepositorioException("no leyó nada");
            return -1; //no llega acá, sale en la exception
        }
    }
    //.....................TRAMITE........................
   public Tramite consultaPorTramiteId(int idTramite)
    {
        string? idEncontrado;
        string? datoLeido;
        using var sr = new StreamReader(nombreArchivoTramites);
        while (!sr.EndOfStream)
        {
            idEncontrado = sr.ReadLine();
            if (idEncontrado == null)
            {
                throw new Exception("No se leyó ningún dato");
            }
            if (idEncontrado.Equals((idTramite).ToString()))
            {
                datoLeido = sr.ReadLine();
                if (datoLeido == null)
                {
                    throw new Exception("No se leyó ningún dato");
                }
                int idExpediente = int.Parse(datoLeido);
                EtiquetaTramite etq = Enum.Parse<EtiquetaTramite>(sr.ReadLine() ?? "");
                string contenido = sr.ReadLine() ?? "";
                DateTime creacion = DateTime.Parse(sr.ReadLine() ?? "");
                DateTime modificacion  = DateTime.Parse(sr.ReadLine() ?? "");
                int idModificador = int.Parse(sr.ReadLine() ?? "");
                Tramite tramiteEncontrado = new Tramite(idTramite, idExpediente, etq, contenido, creacion, modificacion, idModificador);
                return tramiteEncontrado;
            }
            else for (int i = 0; i < 6; i++) sr.ReadLine();
        }
        throw new RepositorioException("Tramite no encontrado.");
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
        idTramite = buscarUltimoIdTramite() + 1;
        tramite.Id = idTramite;
        using var sw = new StreamWriter(nombreArchivoTramites, true);
        sw.WriteLine(tramite.Id);
        sw.WriteLine(tramite.ExpedienteId);
        sw.WriteLine(tramite.tipo);
        sw.WriteLine(tramite.contenido);
        sw.WriteLine(tramite.creacion);
        sw.WriteLine(tramite.ultimaModificacion);
        sw.WriteLine(tramite.IdUsuario);
    }
    public void darDeBajaTramite(int idTramite)
    {
        string archivoTemporal = Path.GetTempFileName();
        string IdTramiteBuscado = Convert.ToString(idTramite);
        string? datoLeido = null;
        bool pudeBorrarlo = false;
        using (var sr = new StreamReader(nombreArchivoTramites))
        using (var sw = new StreamWriter(archivoTemporal)){
            while (!sr.EndOfStream)
            {
                datoLeido = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(datoLeido)){
                    throw new Exception("No se leyó ningún dato");
                }
                if (datoLeido.Equals(IdTramiteBuscado))
                {
                    for (int i = 0; i < 6; i++) datoLeido = sr.ReadLine();
                        pudeBorrarlo = true;
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        sw.WriteLine(datoLeido);
                        datoLeido = sr.ReadLine();
                    }
                    sw.WriteLine(datoLeido);
                }
            }
        }
        File.Copy(archivoTemporal, nombreArchivoTramites, true);
        File.Delete(archivoTemporal);
        if (!pudeBorrarlo) new RepositorioException($"No existe el tramite con id: {idTramite}, que desea eliminar.");
    }

    public void ModificarTramite(Tramite tramite)
    {
        string archivoTemporal = Path.GetTempFileName();
        string idTramite = Convert.ToString(tramite.Id);
        string? datoLeido;
        using (StreamReader sr = new StreamReader(nombreArchivoTramites))
        using (StreamWriter sw = new StreamWriter(archivoTemporal))
        while (!sr.EndOfStream)
        {
            datoLeido = sr.ReadLine(); //Leo ID
            if (datoLeido == null)
            {
                throw new Exception("No se leyó ningún dato");
            }
            if (datoLeido.Equals(idTramite)) //Es el expediente D:
            {
                sw.WriteLine(datoLeido);    //Escribo la IdTramite.
                datoLeido = sr.ReadLine();  //Muevo el puntero IdExpediente.
                sw.WriteLine(datoLeido);  // Mantengo
                datoLeido = sr.ReadLine(); // Muevo el puntero a tipo.
                sw.WriteLine(tramite.tipo);  //Modifico
                datoLeido = sr.ReadLine(); // Muevo el puntero a contenido.
                sw.WriteLine(tramite.contenido); //Actualizo.
                datoLeido = sr.ReadLine(); //Muevo el puntero a creacion.
                sw.WriteLine(datoLeido); //Mantengo.
                datoLeido = sr.ReadLine(); //Muevo el puntero a ultima modificacion.
                sw.WriteLine(tramite.ultimaModificacion);
                datoLeido = sr.ReadLine();//Muevo el puntero a IdModificadora.
                sw.WriteLine(tramite.IdUsuario);
            }
            else
            {
                sw.WriteLine(datoLeido);
                for (int i = 0; i < 6; i++)
                {
                    datoLeido = sr.ReadLine();
                    sw.WriteLine(datoLeido);
                }
            }
        }
        File.Copy(archivoTemporal, nombreArchivoTramites, true); // Copio el temporal cargado con los nuevos datos en el anterior (true indica que se sobreescribe).
        File.Delete(archivoTemporal); //Elimino el temporal para no tener copias innecesarias.
    }


    // --------------------------------PRIVATE----------------------------------------

    private List<Tramite> getTramitesPorIdExpediente(int id)
    //Modificado, versión anterior comentada debajo. Se rompía el encapsulamiento mediante asiganciones sin usar constructor.
    {
        List<Tramite> lista = new List<Tramite>();
        EtiquetaTramite etq;
        string contenido;
        DateTime creacion;
        DateTime ultimaModificacion;
        int IdUsuarioModificador;

        using (StreamReader sr = new StreamReader(nombreArchivoTramites))
        {
            while (!sr.EndOfStream)
            {
                string? idTramite = sr.ReadLine();
                string? idExpediente = sr.ReadLine();
                if(idExpediente == null){
                    throw new Exception("No se leyó ningún dato."); //nunca va a tirar esta excepcion.
                }
                if (!idExpediente.Equals(id.ToString())) for (int i = 0; i < 5; i++) sr.ReadLine();
                else
                {
                    etq = Enum.Parse<EtiquetaTramite>(sr.ReadLine() ?? ""); //nunca van a asignarse comillas porque nunca va a ser null
                    contenido = (sr.ReadLine() ?? "");
                    creacion = DateTime.Parse(sr.ReadLine() ?? "");
                    ultimaModificacion = DateTime.Parse(sr.ReadLine() ?? "");
                    IdUsuarioModificador = Convert.ToInt32(sr.ReadLine() ?? "");
                    Tramite t = new Tramite(Convert.ToInt32(idTramite), id, etq, contenido, creacion, ultimaModificacion, IdUsuarioModificador);
                    lista.Add(t);
                }
            }
        }
        return lista;
    }

    private int buscarUltimoIdTramite()
    {
        string? id = null;
        using (StreamReader sr = new StreamReader(nombreArchivoTramites)){
            string? str = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(str)){
                return 0;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while(!sr.EndOfStream)
            {
                id = sr.ReadLine();
                for(int i=0;i<6;i++){
                    sr.ReadLine();
                }
            }
            if(id != null){
                return int.Parse(id);
            }
            new RepositorioException("no leyó nada");
            return -1; //no llega acá, sale en la exception
    }
}
}