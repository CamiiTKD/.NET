namespace CGE.Aplicacion;

public class CasoDeUsoTramiteConsultaTodos(ITramiteRepositorio repo)
{
    public List<Tramite>? EjecutarConsultarTodos()
    {
        List<Tramite>? lista;
        lista = repo.ConsultarTodos();
        return lista;
    }
}