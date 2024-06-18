namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repo)
{
    public void consultaPorId(int id)
    {
        Expediente? e = repo.consultaPorId(id);
        if (e != null)
        {
            Console.WriteLine(e.ToString());
        }
    }
}