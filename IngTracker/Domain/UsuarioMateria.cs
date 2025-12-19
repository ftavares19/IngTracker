namespace Domain;

public class UsuarioMateria
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int MateriaId { get; set; }
    public Materia Materia { get; set; }
    public Estado Estado { get; set; }
    public int? Nota { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaAprobacion { get; set; }
}
