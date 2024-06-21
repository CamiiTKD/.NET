namespace CGE.Aplicacion;

public class CasoDeUsoTramiteConsultaPorId(ITramiteRepositorio repoT)
{
    public Tramite? Ejecutar(int Id)
    {
        Tramite? T = repoT.consultaPorTramiteId( Id );
        if ( T == null )
            new RepositorioException($"No existe un tramite con la Id:{Id}" );
        return T;
    }
}
