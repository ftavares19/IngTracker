namespace Domain;

public class Carrera
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public List<Titulo> Titulos { get; set; }
    public List<Materia> Materias { get; set; }
}
