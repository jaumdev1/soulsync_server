namespace soulsync.Domain
{
    public class Playground
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int AdministradorPrincipalId { get; set; }
        public Usuario AdministradorPrincipal { get; set; }
        public List<UsuarioPlayground> UsuariosPlayground { get; set; }
        public List<Administrador> Administradores { get; set; }
    }
}
