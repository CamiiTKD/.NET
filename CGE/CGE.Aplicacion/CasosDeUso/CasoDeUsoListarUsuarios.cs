namespace CGE.Aplicacion;
public class CasoDeUsoListarUsuarios(IUsuarioRepositorio repo, IServicioAutorizacion servicio){

    public List<Usuario> EjecutarListado (){
        return repo.consultaUsuarios();
    }

}