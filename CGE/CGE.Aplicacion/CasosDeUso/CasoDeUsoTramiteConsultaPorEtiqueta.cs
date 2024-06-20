namespace CGE.Aplicacion;
public class CasoDeUsoTramiteConsultaPorEtiqueta(ITramiteRepositorio repoTra)
{
    public List<Tramite>? EjecutarConsultaPorEtiqueta(EtiquetaTramite etiqueta)
    {
        List<Tramite>? lista = new List<Tramite>();
        lista = repoTra.ConsultaEtiqueta(etiqueta);
        return lista;
    }
}