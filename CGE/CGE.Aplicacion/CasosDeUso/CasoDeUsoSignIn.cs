namespace CGE.Aplicacion;
public class CasoDeUsoSignIn(IUsuarioRepositorio repo, IServicioAutorizacion servicio)
{
    public Usuario EjecutarModificarUsuario (string mail,string contraseña){
        return repo.RetornarUsuario(mail,contraseña);
    }
}