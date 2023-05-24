namespace ToyWorld.API.Models
{
    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public Guid ToyId { get; set; }
    }
}
