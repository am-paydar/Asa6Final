namespace Domain.Models
{
    public class DomainEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsRemove { get; set; }
        public string NormalPath { get; set; }
    }
}
