namespace CGE.Aplicacion;
public class CasoDeUsoListarUsuarios(IUsuarioRepositorio repo){

    public List<Usuario> EjecutarListado (){
        return repo.consultaUsuarios();
    }

}