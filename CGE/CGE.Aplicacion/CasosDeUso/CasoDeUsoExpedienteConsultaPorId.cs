namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repoE,ITramiteRepositorio repoT){
    public Expediente? EjecutarConsultarPorId(int id){
        Expediente? exp = repoE.consultaPorId(id);
        if (exp!=null) 
            exp.Tramites = repoT.GetTramitesPorIdExpediente(exp.Id);
        return exp;
    }
}