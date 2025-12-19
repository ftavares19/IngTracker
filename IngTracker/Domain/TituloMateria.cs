namespace Domain;

public class TituloMateria
{
    public int TituloId { get; set; }
    public Titulo Titulo { get; set; }
    public int MateriaId { get; set; }
    public Materia Materia { get; set; }
}
