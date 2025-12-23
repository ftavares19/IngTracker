namespace Domain;

public class Titulo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int CantidadMaterias { get; set; }
    public int CarreraId { get; set; }
    public Carrera Carrera { get; set; }
}