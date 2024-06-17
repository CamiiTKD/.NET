namespace CGE.Aplicacion;
public class Tramite
{
    private int _id;
    public int Id { get => _id; set => _id = value; } //setter público para delegar a repo la carga de IDs de trámite.
    private int _expedienteId;
    public int ExpedienteId { get => _expedienteId;  set => _expedienteId = value; }
    private EtiquetaTramite _tipo;
    public EtiquetaTramite tipo { get => _tipo;  set => _tipo = value; }
    private string? _contenido;
    public string? contenido { get => _contenido;  set => _contenido = value; }
    private DateTime _creacion;
    public DateTime creacion { get => _creacion;  set => _creacion = value; }
    private DateTime _ultimaModificacion;
    public DateTime ultimaModificacion { get => _ultimaModificacion;  set => _ultimaModificacion = value; }
    private int _idUsuario;
    public int IdUsuario { get => _idUsuario;  set => _idUsuario = value; }
    public Tramite()
    {

    }
    public Tramite(int expedienteId, EtiquetaTramite tipo, string? contenido, int idUsuario) //Al crear el tramite por primera vez ??
    {
        _expedienteId = expedienteId;
        _tipo = tipo;
        _contenido = contenido;
        _creacion = DateTime.Now;
        _ultimaModificacion = creacion;
        _idUsuario = idUsuario;
    }

    public Tramite(int id, int expedienteId, EtiquetaTramite tipo, string? contenido, DateTime creacion, DateTime modificacion, int idUsuario) //Usado en repoTra.consultaPorTramiteId.
    {
        _id = id;
        _expedienteId = expedienteId;
        _tipo = tipo;
        _contenido = contenido;
        _creacion = creacion;
        _ultimaModificacion = modificacion;
        _idUsuario = idUsuario;
    }

    public void ModificarUltimaFecha(int ID)
    {
        ultimaModificacion = DateTime.Now;
        IdUsuario = ID;
    }
    public override string ToString()
    {
        return $"------------------\n ID: {Id}\n ID expediente asociado {ExpedienteId}\n Etiqueta: {tipo}\n Contenido: {contenido}\n Creacion: {creacion}\n Ultima modificacion: {ultimaModificacion}\n ID ultima modificacion: {IdUsuario}\n------------------\n";
    }
}