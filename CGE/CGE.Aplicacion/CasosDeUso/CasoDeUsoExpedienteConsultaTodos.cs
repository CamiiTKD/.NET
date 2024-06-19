namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaTodos(IExpedienteRepositorio repo)
{
    public List<Expediente> EjecutarConsultarTodos()
    {
        List<Expediente>? lista;
        lista = repo.consultaTodos();
        return lista;
    }
}