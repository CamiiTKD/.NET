using CGE;
using CGE.Aplicacion;
using CGE.Repositorios;
using System.Threading; //para time sleep, no sera lo mas comodo para el usuario pero es mejor que nada

CGESqlite.Inicializar();

using (var context = new CGEContext()){

const int idUsuario = 1;

//tramite y expediente
Expediente expediente = new Expediente("Esta es una carátula", idUsuario);
Tramite tramite = new Tramite(0, EtiquetaTramite.Pase_A_Estudio, "un contenido", idUsuario); //1 xq es el primer tramite

//repositorios
IExpedienteRepositorio repoExpediente = new Repositorio();
ITramiteRepositorio repoTtramite = new Repositorio();

//validadores
TramiteValidador validadorTtramite = new TramiteValidador();
ExpedienteValidador validadorExpediente = new ExpedienteValidador();

//servicios
ServicioActualizacionEstado servicioEstado = new ServicioActualizacionEstado();
IServicioAutorizacion servicioAutorizacion = new ServicioAutorizacionProvisorio();

//caso de expediente Alta
CasoDeUsoExpedienteAlta CU_Expediente_Alta = new CasoDeUsoExpedienteAlta(repoExpediente, validadorExpediente, servicioAutorizacion);

//caso de Tramite Alta
CasoDeUsoTramiteAlta CU_Tramite_Alta = new CasoDeUsoTramiteAlta(repoTtramite, repoExpediente, validadorTtramite, servicioEstado, servicioAutorizacion);

//caso de expediente consulta por ID
CasoDeUsoExpedienteConsultaPorId CU_Expediente_Consulta_Id = new CasoDeUsoExpedienteConsultaPorId(repoExpediente);

//caso de consultar por todos
CasoDeUsoExpedienteConsultaTodos CU_Expediente_ConsultaTodos = new CasoDeUsoExpedienteConsultaTodos(repoExpediente);

//caso de consulta por etiqueta
CasoDeUsoTramiteConsultaPorEtiqueta CU_Tramite_ConsultaEtiqueta_Tramite = new CasoDeUsoTramiteConsultaPorEtiqueta(repoTtramite);

//caso de Tramite Baja
CasoDeUsoTramiteBaja CU_Tramite_Baja = new CasoDeUsoTramiteBaja(repoTtramite, repoExpediente, servicioEstado, servicioAutorizacion);

//caso de Expediente Baja
CasoDeUsoExpedienteBaja CU_Expediente_Baja = new CasoDeUsoExpedienteBaja(repoExpediente, repoTtramite, servicioAutorizacion);

//caso de Expediente Modificacion
CasoDeUsoExpedienteModificacion CU_Expediente_Modificacion = new CasoDeUsoExpedienteModificacion(repoExpediente, validadorExpediente, servicioAutorizacion, servicioEstado);

//caso de Tramite Modificacion
CasoDeUsoTramiteModificacion CU_Tramite_Modificacion = new CasoDeUsoTramiteModificacion(repoTtramite, repoExpediente, servicioEstado, servicioAutorizacion);


bool salir = false;
while (!salir)
{
    ImprimirOpciones();
    int opcion;
    string str = System.Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(str)) opcion = -1; // Para entrar en el catch
    try
    {
        opcion = int.Parse(str);
        switch (opcion)
        {
            case 0: salir = true; break;
            case 1: caso1(); break;
            case 2: caso2(); break;
            case 3: caso3(); break;
            case 4: caso4(); break;
            case 5: caso5(); break;
            case 6: caso6(); break;
            case 7: caso7(); break;
            case 8: caso8(); break;
            case 9: caso9(); break;
            default: System.Console.WriteLine("Opcion invalida."); break; 
        }
        System.Console.WriteLine("-------------------");
    }
    catch
    {
        System.Console.WriteLine("Ingrese un numero valido");
        Thread.Sleep(1500);
    }
}



void ImprimirOpciones()
{
    System.Console.WriteLine("Ingrese una de las siguientes opciones");
    System.Console.WriteLine("[1] Dar de alta un expediente");
    System.Console.WriteLine("[2] Dar de baja un expediente");
    System.Console.WriteLine("[3] Imprimir un expediente");
    System.Console.WriteLine("[4] Imprimir todos los expedientes");
    System.Console.WriteLine("[5] Modificar un expediente");
    System.Console.WriteLine("[6] Dar de alta un tramite");
    System.Console.WriteLine("[7] Dar de baja un tramite");
    System.Console.WriteLine("[8] Imprimir tramites con la etiqueta solicitada");
    System.Console.WriteLine("[9] Modificar un tramite");
    System.Console.WriteLine("[0] salir");
    System.Console.Write("Opcion: ");
}

void caso1()
{
    try
    {
    System.Console.Write("Ingrese la caratula del expediente: ");
    string? caratula = System.Console.ReadLine() ?? "";
    if (string.IsNullOrEmpty(caratula)) throw new Exception("Caratula invalida");
    expediente.caratula = caratula;
    CU_Expediente_Alta.EjecutarExpedienteAlta(expediente,idUsuario);
    System.Console.WriteLine("Expediente creado correctamente");
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    finally
    {
        Thread.Sleep(2000);
    }
}

void caso2()
{
    try
    {
        System.Console.Write("Ingrese la ID del expediente a eliminar: ");
        string strAux = System.Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(strAux)) throw new Exception("ID ingresada invalida");
        int idEliminar = int.Parse(strAux);
        CU_Expediente_Baja.EjecutarExpedienteBaja(idEliminar,idUsuario);
        System.Console.WriteLine("Expediente eliminado con exito");
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    finally
    {
        Thread.Sleep(2000);
    }
}

void caso3()
{
   try
   {
        System.Console.Write("Ingrese la ID del expediente a buscar: ");
        string strAux = System.Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(strAux)) throw new Exception("ID invalida");
        int idBuscar = int.Parse(strAux);
        CU_Expediente_Consulta_Id.consultaPorId(idBuscar);
        System.Console.WriteLine("Expediente encontrado");
        Thread.Sleep(5000);
   }
   catch (Exception ex)
   {
        System.Console.WriteLine(ex.Message);
        Thread.Sleep(2000);
   }
}

void caso4()
{
    CU_Expediente_ConsultaTodos.EjecutarConsultarTodos();
    System.Console.WriteLine("Expedientes encontrados.");
    Thread.Sleep(10000);
}

void caso5()
{
    try
    {
        System.Console.Write("Ingrese la ID del expediente a modificar: ");
        string strAux = System.Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(strAux)) throw new Exception("");
        int idBuscar = int.Parse(strAux);
        CU_Expediente_Modificacion.EjecutarModificacionExpediente(idBuscar, idUsuario);
        System.Console.WriteLine("Expediente modificado correctamente");
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    finally
    {
        Thread.Sleep(2000);
    }
}

void caso6()
{
    try
    {
        System.Console.Write("ingrese el contenido del tramite: ");
        string contenido = System.Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(contenido)) throw new Exception("contenido invalido");
        System.Console.Write("Ingrese la ID del expediente al que asociar el tramite: ");
        string expedienteId = System.Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(expedienteId)) throw new Exception("ID del expdiente Invalida");
        int expeId = int.Parse(expedienteId);
        tramite.tipo = ElegirEtiqueta();
        tramite.ExpedienteId = expeId;
        tramite.contenido = contenido;
        CU_Tramite_Alta.EjecutarTramiteAlta(tramite, idUsuario);
        System.Console.WriteLine("Tramite creado con exito");
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    finally
    {
        Thread.Sleep(2000);
    }
}

void caso7()
{
    try
    {
        System.Console.Write("Ingrese la ID del tramite a eliminar: ");
        string strAux = System.Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(strAux)) throw new Exception("ID invalida");
        int idEliminar = int.Parse(strAux);
        CU_Tramite_Baja.EjecutarTramiteBaja(idEliminar,idUsuario);
        System.Console.WriteLine("tramite eliminado con exito");
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    finally
    {
        Thread.Sleep(2000);
    }
}

void caso8()
{
    try
    {
        EtiquetaTramite etiquetaBuscar = ElegirEtiqueta();
        CU_Tramite_ConsultaEtiqueta_Tramite.EjecutarConsultaPorEtiqueta(etiquetaBuscar);
        System.Console.WriteLine("Tramites Encontrados");
        Thread.Sleep(7000);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
        Thread.Sleep(2000);
    }
}

void caso9()
{
    try
    {
        System.Console.Write("Ingrese la ID del tramite a modificar: ");
        string strAux = System.Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(strAux)) throw new Exception("ID invalida");
        int idModificar = int.Parse(strAux);
        CU_Tramite_Modificacion.EjecutarModificacionTramite(idModificar,idUsuario);
        System.Console.WriteLine("tramite modificado con exito");
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    finally
    {
        Thread.Sleep(2000);
    }
}

//reciclado de caso de uso tramite
EtiquetaTramite ElegirEtiqueta()
{
    System.Console.WriteLine("Seleccione la etiqueta deseada:");
    System.Console.WriteLine("[1] Escrito presentado.");
    System.Console.WriteLine("[2] Pase a estudio.");
    System.Console.WriteLine("[3] Despacho.");
    System.Console.WriteLine("[4] Resolución.");
    System.Console.WriteLine("[5] Notificación.");
    System.Console.WriteLine("[6] Pase al archivo.");
    System.Console.Write("Opcion: ");

    string strAuxiliar = System.Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(strAuxiliar)) throw new Exception("asd");
    int opcion = Convert.ToInt32(strAuxiliar);
    switch (opcion)
    {
        case 1:
            return EtiquetaTramite.Escrito_Presentado;
        case 2:
            return EtiquetaTramite.Pase_A_Estudio;
        case 3:
            return EtiquetaTramite.Despacho;
        case 4:
            return EtiquetaTramite.Resolucion;
        case 5:
            return EtiquetaTramite.Notificacion;
        case 6:
            return EtiquetaTramite.Pase_Al_Archivo;
        default:
            throw new Exception("Opcion invalida");
    }
}
}