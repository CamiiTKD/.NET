namespace CGE.Aplicacion;
public class ServicioActualizacionEstado
{
    public void ActualizarEstado(Expediente expediente)         
    {
        if (expediente.tramites != null && expediente.tramites.Count() > 0)
        { 
            int cantidadTramites = expediente.tramites.Count();
            Tramite tramiteTemporal = expediente.tramites[cantidadTramites - 1];

            switch (tramiteTemporal.tipo)
            {
                case EtiquetaTramite.Resolucion:
                    expediente.estado = EstadoExpediente.Con_Resolucion;
                    break;
                case EtiquetaTramite.Pase_A_Estudio:
                    expediente.estado = EstadoExpediente.Para_Resolver;
                    break;
                case EtiquetaTramite.Pase_Al_Archivo:
                    expediente.estado = EstadoExpediente.Finalizado;
                    break;
                default:
                    break;
            }
        }
    }
}