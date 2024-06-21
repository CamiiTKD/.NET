namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repoE,ITramiteRepositorio repoT){
    public Expediente? EjecutarConsultarPorId(int id){
        Expediente? exp = repoE.consultaPorId(id);
        if (exp!=null) 
        {
            exp.Tramites = repoT.GetTramitesPorIdExpediente(exp.Id);
            return exp;
        }
        new RepositorioException($"No existe un Expediente con el ID:{id}");
        return exp;
    }
}