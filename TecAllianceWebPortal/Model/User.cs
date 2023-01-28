namespace TecAllianceWebPortal.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
