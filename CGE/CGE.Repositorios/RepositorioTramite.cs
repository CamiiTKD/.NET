namespace CGE.Repositorios;

using Aplicacion;
using System.Security.Cryptography;
using System.Text;

public class RepositorioTramite : ITramiteRepositorio
{

    public CGEContext contexto;
    public static int idTramite;

    public RepositorioTramite(CGEContext contexto)
    {
        this.contexto = contexto;
    }

    public List<Tramite>? ConsultarTodos()
    {
        var lista = contexto.Tramites.ToList();
        return lista;
    }
    public Tramite? consultaPorTramiteId(int idTramite)
    {
        var tramite = contexto.Tramites.Find(idTramite);
        if (tramite != null)
        {
            Tramite tramiteEncontrado = new Tramite(tramite);
            return tramiteEncontrado;
        }
        else
        {
            return null;
        }
    }

    public List<Tramite>? ConsultaEtiqueta(EtiquetaTramite etiqueta)
    {
        var listaTramites = contexto.Tramites.
                                Where(t => t.Tipo == etiqueta)
                                .ToList();
        return listaTramites;
    }

    public void darDeAltaTramite(Tramite tramite)
    {
        contexto.Add(tramite);
        contexto.SaveChanges();
    }

    public void darDeBajaTramite(int idTramite)
    {
        var tramite = contexto.Tramites.Find(idTramite); //No requiere indicar tipo Tramite porque asume el tipo de retorno.
        if (tramite != null)
        {
            contexto.Tramites.Remove(tramite);
            contexto.SaveChanges();
        }
        else
        {
            new RepositorioException($"No existe el tramite con id: {idTramite}, que desea eliminar.");
        }
    }


    public void ModificarTramite(Tramite tramite)
    // NO SE PUEDE hacer tramiteExistente=tramite porque 
    //solo modificaríamos la referencia y no el tramite en la base de datos!!
    {
        var tramiteExistente = contexto.Tramites.Find(tramite.Id); //Si no lo encuentra devuelve null.
        if (tramiteExistente != null)
        { //Si lo encuentra modifico lo pertinente, lo demás no necesita modificarse.
            tramiteExistente.Tipo = tramite.Tipo;
            tramiteExistente.Contenido = tramite.Contenido;
            tramiteExistente.UltimaModificacion = tramite.UltimaModificacion;
            tramiteExistente.IdUsuario = tramite.IdUsuario;
            contexto.SaveChanges(); //Reflejo los cambios.
        }
    }

    public List<Tramite> GetTramitesPorIdExpediente(int id)
    {
        List<Tramite> listaTramites = contexto.Tramites
                                .Where(tramite => tramite.ExpedienteId == id)
                                .ToList();
        return listaTramites;
    }
}