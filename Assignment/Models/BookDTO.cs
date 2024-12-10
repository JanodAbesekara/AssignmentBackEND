namespace Assignment.Models
{
    public class BookDTO
    {
        public required Guid Id { get; set; } 
        public required string Title { get; set; } 

        public required string Author { get; set; } 

        public string ISBN { get; set; } 

        public required DateTime PublicationDate { get; set; } 
    }
}
