namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaTodos(IExpedienteRepositorio repo){
    public void EjecutarConsultarTodos(){
        List<Expediente>? lista = new List<Expediente>();
        lista = repo.consultaTodos();
        int i = 1;
        string str = "";
        foreach (Expediente e in lista)
        {
            str += $"Expediente {i++} \n";
            str += e.ToString();
        }
        Console.WriteLine(str);
        lista = null;
    }
}