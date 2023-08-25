namespace soulsync.Domain
{
    public class UsuarioPlayground
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PlaygroundId { get; set; }
        public Usuario Usuario { get; set; }
        public Playground Playground { get; set; }
    }
}
