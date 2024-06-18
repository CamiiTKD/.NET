namespace CGE.Aplicacion;
public class ExpedienteValidador{
    public bool Validar(Expediente expediente) {
        string mensajeError = "";
        if (string.IsNullOrWhiteSpace(expediente.Caratula)) {
            mensajeError = "La caratula debe contener texto.\n";
        }
        if (expediente.Id < 0) {
            mensajeError += "El id debe ser mayor o igual a cero.\n";
        }
        if (mensajeError != ""){
            new ValidacionException(mensajeError);
        }
        return true;
    }
}