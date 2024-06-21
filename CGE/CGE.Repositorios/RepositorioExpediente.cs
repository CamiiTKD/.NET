namespace CGE.Repositorios;

using Aplicacion;
using System.Security.Cryptography;
using System.Text;


public class RepositorioExpediente : IExpedienteRepositorio
{
    public CGEContext contexto;

    public RepositorioExpediente(CGEContext contexto)
    {
        this.contexto = contexto;
    }

    // >>>>>>>>>>>>>>>>>>>>>>>EXPEDIENTES<<<<<<<<<<<<<<<<<<<<<<<<

    public Expediente? consultaPorId(int id)
    {
        var expedienteEncontrado = contexto.Expedientes.Find(id);
        if (expedienteEncontrado != null)
        {
            Expediente? expe = expedienteEncontrado; //saque new y constructor con expediente parametro.
            return expe;
        }
        else
        {
            return null;
        }
    }

    public List<Expediente> consultaTodos()
    {
        List<Expediente> ListaExpedientes = contexto.Expedientes.ToList();
        return ListaExpedientes;
    }

    public void darDeAltaExpediente(Expediente expediente)
    {
        contexto.Add(expediente);
        contexto.SaveChanges();
    }

    public void darDeBajaExpediente(int IdExpediente)
    {
        var expediente = contexto.Expedientes.Find(IdExpediente);
        if (expediente != null)
        {
            contexto.Expedientes.Remove(expediente);
            contexto.SaveChanges();
        }
        else
        {
            new RepositorioException
            ($"No existe el expediente con ID: {IdExpediente}. No es posible eliminar");
        }
    }

    public void ModificarExpediente(Expediente expediente, int id)
    {
        var expedienteExistente = contexto.Expedientes.Find(expediente.Id);
        if (expedienteExistente != null)
        {
            expedienteExistente.Caratula = expediente.Caratula;
            expedienteExistente.UltimaModificacion = DateTime.Now;
            expedienteExistente.IdUsuario = id;
            expedienteExistente.Estado = expediente.Estado;
            contexto.SaveChanges();
        }
        else
        {
            new RepositorioException
            ($"No existe el expediente con ID: {expediente.Id}, que desea modificar.");
        }
    }
}