namespace CGE.Aplicacion;
public class RepositorioException : Exception {
    public RepositorioException(string mensajeError){
        throw new Exception(mensajeError);
    }
}