namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repo){
    public void consultaPorId(int id){
        Console.WriteLine(repo.consultaPorId(id).ToString());
    }
}