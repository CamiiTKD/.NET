using CGE.UI.Components;

//agregamos estas directivas using
using CGE.Repositorios;
using CGE.Aplicacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add our own services to the container. 
CGEContext contexto = new CGEContext();
// EXPEDIENTE
builder.Services.AddTransient<CasoDeUsoExpedienteAlta>();
builder.Services.AddTransient<CasoDeUsoExpedienteBaja>();
builder.Services.AddTransient<CasoDeUsoExpedienteConsultaPorId>();
builder.Services.AddTransient<CasoDeUsoExpedienteConsultaTodos>();
builder.Services.AddTransient<CasoDeUsoExpedienteModificacion>();
// TRAMITE
builder.Services.AddTransient<CasoDeUsoTramiteAlta>();
builder.Services.AddTransient<CasoDeUsoTramiteBaja>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaPorEtiqueta>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaPorId>();
builder.Services.AddTransient<CasoDeUsoTramiteModificacion>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaTodos>();
// USUARIO
builder.Services.AddTransient<CasoDeUsoUsuarioAlta>();
builder.Services.AddTransient<CasoDeUsoUsuarioBaja>();
builder.Services.AddTransient<CasoDeUsoListarUsuarios>();
builder.Services.AddTransient<CasoDeUsoUsuarioModificacion>();
builder.Services.AddTransient<CasoDeUsoUsuarioConsultaPorId>();
builder.Services.AddTransient<CasoDeUsoUsuarioConsultaPorEmail>();
builder.Services.AddTransient<CasoDeUsoSignIn>();
// VALIDADOR
builder.Services.AddSingleton<ExpedienteValidador>();
builder.Services.AddSingleton<TramiteValidador>();
// SERVICIO
builder.Services.AddSingleton<ServicioActualizacionEstado>();
builder.Services.AddSingleton<IServicioAutorizacion, ServicioAutorizacion>();
// REPOSITORIO
builder.Services.AddScoped<IExpedienteRepositorio, RepositorioExpediente>(ExpedienteRepo => new RepositorioExpediente(contexto));
builder.Services.AddScoped<ITramiteRepositorio, RepositorioTramite>(TramiteRepo => new RepositorioTramite(contexto));
builder.Services.AddScoped<IUsuarioRepositorio, RepositorioUsuario>(UsuarioRepo => new RepositorioUsuario(contexto));

//STORAGE
builder.Services.AddSingleton<ServicioSesion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
