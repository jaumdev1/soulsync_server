namespace soulsync.Presentation.Models
{
    public class PlaygroundModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<int> OutrosAdministradoresIds { get; set; }
    }

    public class PlaygroundModelInvite{
        
        public int PlaygroundId { get; set; }
    
    }
}
