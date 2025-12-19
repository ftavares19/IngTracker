namespace Domain;

public class Materia
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public Semestre Semestre { get; set; }
    public int CarreraId { get; set; }
    public Carrera Carrera { get; set; }
    public List<Materia> Previas { get; set; }
    public List<TituloMateria> TitulosMaterias { get; set; }
    public List<UsuarioMateria> UsuariosMaterias { get; set; }
}