namespace CGE.Aplicacion;
public class CasoDeUsoSignIn(IUsuarioRepositorio repo)
{
    public Usuario? Ejecutar (string mail,string contraseña){  
        return repo.RetornarUsuario(mail,contraseña);
    }
}