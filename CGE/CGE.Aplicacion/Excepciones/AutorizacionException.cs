namespace CGE.Aplicacion;
public class AutorizacionException : Exception {
    public AutorizacionException(string mensajeError){
        throw new Exception(mensajeError);
    }
}