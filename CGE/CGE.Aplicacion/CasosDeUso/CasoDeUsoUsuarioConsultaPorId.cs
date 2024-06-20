namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioConsultaPorId(IUsuarioRepositorio repo, IServicioAutorizacion servicio)
{
    public Usuario? EjecutarConsulta (int idUsuario){
        return repo.ConsultaUsuario(idUsuario);
    }
}