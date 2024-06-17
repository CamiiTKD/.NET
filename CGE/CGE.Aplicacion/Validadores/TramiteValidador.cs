namespace CGE.Aplicacion;
public class TramiteValidador
{
    public bool Validar(Tramite tramite)
    {
        string mensajeError = "";
        if (string.IsNullOrWhiteSpace(tramite.contenido))
        {
            mensajeError = "El contenido debe contener texto.\n";
        }
        if (tramite.Id < 0)
        {
            mensajeError += "El id debe ser mayor o igual a cero.\n";
        }
        if (mensajeError != "")
        {
            new ValidacionException(mensajeError);
        }
        return true;
    }
}