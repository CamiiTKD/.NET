namespace CGE.Aplicacion;
public interface ITramiteRepositorio
{
    public Tramite? consultaPorTramiteId(int idTramite);
    public void darDeAltaTramite(Tramite tramite);
    public void darDeBajaTramite(int idTramite); // Lo convierto en un bool para evitar más verificaciones en caso de uso Tramite Baja.
    public void ModificarTramite(Tramite tramite);
    public List<Tramite> ConsultaEtiqueta(EtiquetaTramite etiqueta);
    public List<Tramite> GetTramitesPorIdExpediente(int id);
    public List<Tramite>? ConsultarTodos();
}