namespace CGE.Aplicacion;
public class CasoDeUsoTramiteConsultaPorId(ITramiteRepositorio repoT){
    public Tramite? EjecutarConsultarPorId(int id){
        return repoT.consultaPorTramiteId(id);
    }
}