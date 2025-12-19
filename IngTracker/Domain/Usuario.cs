namespace Domain;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public int CarreraId { get; set; }
    public Carrera Carrera { get; set; }
    public List<UsuarioMateria> UsuariosMaterias { get; set; }
}
