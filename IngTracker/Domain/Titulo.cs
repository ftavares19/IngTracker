namespace Domain;

public interface Titulo
{
    public string Nombre { get; set; }
    public List<Materia> Materias { get; set; }
    public int CantidadMaterias { get; set; }
    public int Promedio  { get; set; }
}