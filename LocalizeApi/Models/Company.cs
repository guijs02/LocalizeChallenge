namespace Domain.Entites
{
    public class Company
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;             
        public string Cnpj { get; set; } = string.Empty;                 
        public string Status { get; set; } = string.Empty;
        public string OpeningDate { get; set; } = string.Empty;   
        public string Type { get; set; } = string.Empty;                 
        public string LegalNature { get; set; } = string.Empty;          
        public string PrimaryActivity { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string AddressComplement { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
