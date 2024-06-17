namespace CGE.Aplicacion;
public class ValidacionException : Exception
{
    public ValidacionException(string mensajeError)
    {
        throw new Exception(mensajeError);
    }
}