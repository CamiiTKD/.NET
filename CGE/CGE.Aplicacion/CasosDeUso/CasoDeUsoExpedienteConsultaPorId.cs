namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repo){
    public Expediente? EjecutarConsultarPorId(int id){
        return repo.consultaPorId(id);
    }
}