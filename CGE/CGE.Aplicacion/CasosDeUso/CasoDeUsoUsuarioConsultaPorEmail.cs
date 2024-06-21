namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioConsultaPorEmail(IUsuarioRepositorio repo)
{
    public bool EjecutarConsultaEmail (string mail){
        return repo.ConsultaUsuarioEmail(mail);
    }
}