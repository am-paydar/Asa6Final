namespace Domain.Models
{
    public class ImageEntity : DomainEntity
    {
        public bool Flag { get; set; }
        public string? BigPath { get; set; }
        public string? TinyPath { get; set; }
    }
}
