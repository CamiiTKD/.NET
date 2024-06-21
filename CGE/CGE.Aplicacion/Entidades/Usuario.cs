namespace CGE.Aplicacion;

public class Usuario {
    private int _id;
    public int id { get => _id; set => _id = value;}
    private string? _nombre;
    public string? nombre { get => _nombre; set => _nombre = value;}
    private string? _apellido;
    public string? apellido { get => _apellido; set => _apellido = value;}
    private string? _email;
    public string? email { get => _email; set => _email = value;}
    private string? _contraseña;
    public string? contraseña { get => _contraseña; set => _contraseña = value;}
    public List<Permiso>? permisos { get; set; } = new List<Permiso>();

    public Usuario(){

    }
    public Usuario(string nombre, string apellido, string email, string contraseña){
        this._nombre = nombre;
        this._apellido = apellido;
        this._email = email;
        this._contraseña = contraseña;
        permisos = new List<Permiso>();
    }

}