namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioConsultaPorId(IUsuarioRepositorio repo)
{
    public Usuario? EjecutarConsulta (int idUsuario){
        return repo.ConsultaUsuario(idUsuario);
    }
}