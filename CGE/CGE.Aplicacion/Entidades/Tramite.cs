namespace CGE.Aplicacion;
public class Tramite
{
    public int Id { get; set; } //Se autoincrementa en la base de datos no?

    public int ExpedienteId { get; set; }
    public EtiquetaTramite Tipo { get; set; }
    public string? Contenido { get; set; }
    public DateTime Creacion { get; set; }
    public DateTime UltimaModificacion { get; set; }
    public int IdUsuario { get; set; }
    public Tramite()
    {

    }
    public Tramite(int expedienteId, EtiquetaTramite tipo, string? contenido, int idUsuario) //Al crear el tramite por primera vez ??
    {
        ExpedienteId = expedienteId;
        Tipo = tipo;
        Contenido = contenido;
        Creacion = DateTime.Now;
        UltimaModificacion = Creacion;
        IdUsuario = idUsuario;
    }

    public Tramite(int id, int expedienteId, EtiquetaTramite tipo, string? contenido, DateTime creacion, DateTime modificacion, int idUsuario) //Usado en repoTra.consultaPorTramiteId.
    {
        Id = id;
        ExpedienteId = expedienteId;
        Tipo = tipo;
        Contenido = contenido;
        Creacion = creacion;
        UltimaModificacion = modificacion;
        IdUsuario = idUsuario;
    }

    public Tramite(Tramite t)
    {
        Id = t.Id;
        ExpedienteId = t.ExpedienteId;
        Tipo = t.Tipo;
        Contenido = t.Contenido;
        Creacion = t.Creacion;
        UltimaModificacion = t.UltimaModificacion;
        IdUsuario = t.IdUsuario;
    }

    public void ModificarUltimaFecha(int ID)
    {
        UltimaModificacion = DateTime.Now;
        IdUsuario = ID;
    }
    public override string ToString()
    {
        return $"------------------\n ID: {Id}\n ID expediente asociado {ExpedienteId}\n Etiqueta: {Tipo}\n Contenido: {Contenido}\n Creacion: {Creacion}\n Ultima modificacion: {UltimaModificacion}\n ID ultima modificacion: {IdUsuario}\n------------------\n";
    }
}