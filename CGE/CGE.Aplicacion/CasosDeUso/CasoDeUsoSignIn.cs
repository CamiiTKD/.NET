namespace CGE.Aplicacion;
public class CasoDeUsoSignIn(IUsuarioRepositorio repo, IServicioAutorizacion servicio)
{
    public Usuario Ejecutar (string mail,string contraseña){
        return repo.RetornarUsuario(mail,contraseña);
    }
}