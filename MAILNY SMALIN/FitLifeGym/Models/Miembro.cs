namespace FitLifeGym.Models
{
    public class Miembro
    {
        public string Cedula { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{NombreCompleto} (Cédula: {Cedula}) - Tel: {Telefono}";
        }
    }
}
