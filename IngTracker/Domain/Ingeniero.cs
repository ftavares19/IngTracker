namespace Domain;

public class Ingeniero : Titulo
{
    public string Nombre { get; set; }
    public List<Materia> Materias { get; set; }
    public int CantidadMaterias { get; set; } = 44;
    public int Promedio { get; set; }
}