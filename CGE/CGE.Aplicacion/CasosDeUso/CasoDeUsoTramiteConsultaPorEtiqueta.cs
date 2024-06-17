namespace CGE.Aplicacion;
public class CasoDeUsoTramiteConsultaPorEtiqueta(ITramiteRepositorio repoTra)
{
    public void EjecutarConsultaPorEtiqueta(EtiquetaTramite etiqueta)
    {
        List<Tramite>? lista = new List<Tramite>();
        lista = repoTra.ConsultaEtiqueta(etiqueta);
        int i = 1;
        string str = "";
        foreach (Tramite tr in lista)
        {
            str += $"tramite {i++} \n";
            str += tr.ToString();
        }
        Console.WriteLine(str);
        lista = null;
    }
}