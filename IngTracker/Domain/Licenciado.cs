namespace Domain;

public class Licenciado : Titulo
{
    public string Nombre { get; set; }
    public List<Materia> Materias { get; set; }
    public int CantidadMaterias { get; set; } = 39;
    public int Promedio { get; set; }
}