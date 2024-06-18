namespace CGE.Aplicacion;
public class ServicioActualizacionEstado
{
    public void ActualizarEstado(Expediente expediente)         
    {
        if (expediente.Tramites != null && expediente.Tramites.Count() > 0)
        { 
            int cantidadTramites = expediente.Tramites.Count();
            Tramite tramiteTemporal = expediente.Tramites[cantidadTramites - 1];

            switch (tramiteTemporal.Tipo)
            {
                case EtiquetaTramite.Resolucion:
                    expediente.Estado = EstadoExpediente.Con_Resolucion;
                    break;
                case EtiquetaTramite.Pase_A_Estudio:
                    expediente.Estado = EstadoExpediente.Para_Resolver;
                    break;
                case EtiquetaTramite.Pase_Al_Archivo:
                    expediente.Estado = EstadoExpediente.Finalizado;
                    break;
                default:
                    break;
            }
        }
    }
}