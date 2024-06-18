namespace CGE.Aplicacion;
public class Expediente
{  //revisar los posibles null en string y list
    public int Id { get; set; }
    public string? Caratula { get; set; }
    public DateTime Creacion { get; set; }
    public DateTime UltimaModificacion { get; set; }
    public int IdUsuario { get; set; } //Último usuario en modificar el expediente.
    public EstadoExpediente Estado { get; set; }
    public List<Tramite>? Tramites { get; set; }
    public Expediente()
    {

    }
    public Expediente(string? caratula, int idUsuario)
    {
        Caratula = caratula;
        Creacion = DateTime.Now;
        UltimaModificacion = Creacion;
        IdUsuario = idUsuario;
        Estado = EstadoExpediente.Recien_Iniciado;
        Tramites = new List<Tramite>();
    }

    public Expediente(int id, string caratula, DateTime creacion, DateTime ultimaModificacion, int idUsuario, EstadoExpediente estado)
    {
        Id = id;
        Caratula = caratula;
        Creacion = creacion;
        UltimaModificacion = ultimaModificacion;
        IdUsuario = idUsuario;
        Estado = estado;
    }

    public Expediente(Expediente e)
    {
        Id = e.Id;
        Caratula = e.Caratula;
        Creacion = e.Creacion;
        UltimaModificacion = e.UltimaModificacion;
        IdUsuario = e.IdUsuario;
        Estado = e.Estado;
    }

    public void ModificarEstado(EstadoExpediente estado) => Estado = estado;
    public void EliminarDeLista(Tramite tramite)
    {
        if (Tramites != null)
        {
            if ((Tramites.Count() > 0))
            {
                Tramites.Remove(tramite);
            }
        }
    }

    public void AgregarEnLista(Tramite tramite)
    {
        if (Tramites == null)
        {
            Tramites = new List<Tramite>();
        }
        Tramites.Add(tramite);
    }
    public void ModificarCaratula(string caratula) => Caratula = caratula;
    public void ModificarUltimaFecha(int ID)
    {
        UltimaModificacion = DateTime.Now;
        IdUsuario = ID;
    }

    public override string ToString()
    {
        int i = 1;
        string str = $"------------------------------------\n ID Expediente: {Id}\n Caratula: {Caratula}\n Creacion: {Creacion}\n Ultima Modificacion: {UltimaModificacion}\n ID de ultima modificacion: {IdUsuario}\n Estado: {Estado}\n-----------------------------------\n";
        if ((Tramites != null) && (Tramites.Count != 0))
        {
            foreach (Tramite t in Tramites)
            {
                str += $"tramite {i++}\n";
                str += t.ToString();
            }
        }
        return str;
    }
}