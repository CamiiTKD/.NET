using System.Runtime.CompilerServices;

namespace CGE.Aplicacion;
public interface IExpedienteRepositorio
{
    public Expediente consultaPorId(int id);
    public List<Expediente> consultaTodos();
    public void darDeAltaExpediente(Expediente expediente);
    public void ModificarExpediente(Expediente expediente);
    public void darDeBajaExpediente(int IdExpediente);
}