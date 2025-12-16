namespace Domain;

public class Ayudante : Titulo
{
    public string Nombre { get; set; }
    public List<Materia> Materias { get; set; }
    public int CantidadMaterias { get; set; } = 15;
    public int Promedio { get; set; }
}