namespace CGE.Aplicacion;
public class Expediente
{  //revisar los posibles null en string y list
    private int _id;
    public int Id { get => _id ; set => _id=value; }
    private string? _caratula;
    public string? caratula { get=>_caratula; set=> _caratula=value; }
    private DateTime _creacion;
    public DateTime creacion { get=> _creacion; set=> _creacion=value; }
    private DateTime _ultimaModificacion;
    public DateTime ultimaModificacion { get=>_ultimaModificacion; set=> _ultimaModificacion=value; }
    private int _idUsuario;
    public int IdUsuario { get=>_idUsuario; set=>_idUsuario=value; } //Último usuario en modificar el expediente.
    private EstadoExpediente _estado;
    public EstadoExpediente estado { get=>_estado; set=>_estado=value; }
    public List<Tramite>? tramites { get; set; }
    public Expediente()
    {

    }
    public Expediente(string? caratula, int idUsuario)
    {
        this.caratula = caratula;
        this.creacion = DateTime.Now;
        this.ultimaModificacion = creacion;
        this.IdUsuario = idUsuario;
        this.estado = EstadoExpediente.Recien_Iniciado;
        tramites = new List<Tramite>();
    }

    public Expediente (int id, string caratula, DateTime creacion, DateTime ultimaModificacion, int idUsuario, EstadoExpediente estado){
        _id = id;
        _caratula = caratula;
        _creacion = creacion;
        _ultimaModificacion = ultimaModificacion;
        _idUsuario = idUsuario;
        _estado = estado;
    }

    public void ModificarEstado(EstadoExpediente estado) => this.estado = estado;
    public void EliminarDeLista(Tramite tramite){
        if(tramites != null){
            if ((tramites.Count()>0)){
                tramites.Remove(tramite);
            }
        }
    }

    public void AgregarEnLista(Tramite tramite){
        if (tramites == null){
        tramites = new List<Tramite>();
        }
        tramites.Add(tramite);
    }
    public void ModificarCaratula(string caratula) => this.caratula = caratula;
    public void ModificarUltimaFecha(int ID)
    {
        ultimaModificacion = DateTime.Now;
        IdUsuario = ID;
    }

    public override string ToString()
    {
        int i = 1;
        string str = $"------------------------------------\n ID Expediente: {Id}\n Caratula: {caratula}\n Creacion: {creacion}\n Ultima Modificacion: {ultimaModificacion}\n ID de ultima modificacion: {IdUsuario}\n Estado: {estado}\n-----------------------------------\n";
        if((tramites != null)&&(tramites.Count != 0)){
            foreach(Tramite t in tramites)
            {
                str += $"tramite {i++}\n";
                str += t.ToString();
            }
        }
        return str;
    }
}