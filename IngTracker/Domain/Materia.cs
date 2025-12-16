namespace Domain;

public class Materia
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Semestre Semestre { get; set; }
    public List<Materia> Previas { get; set; }
    public Estado Estado { get; set; }
    public int Nota  { get; set; }
}