namespace CGE.Aplicacion;
public enum EtiquetaTramite{
    Escrito_Presentado,
    Pase_A_Estudio,
    Despacho,
    Resolucion,
    Notificacion,
    Pase_Al_Archivo
}

/*
● Cuando la etiqueta del último trámite es "Resolución", 
se produce un cambio automático al estado
"Con resolución".

● Cuando la etiqueta del último trámite es "Pase a estudio",
 se produce un cambio automático al
estado "Para resolver".

● Cuando la etiqueta del último trámite es "Pase al Archivo", 
se produce un cambio automático al
estado "Finalizado".
*/