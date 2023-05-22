namespace api_ventas.Models.Objects
{
    public class Respuesta
    {
        public bool resultado { get; set; }
        public object respuesta { get; set; }
        public Errores? Error { get; set; }
        public Respuesta() => resultado = true;
    }
    public class Errores
    {
        public string mensajeError { get; set; }
        public Errores(string mensajeError) => this.mensajeError = mensajeError;
    }
}
