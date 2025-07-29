namespace TheRealEstate.Models.DTOs
{
    public class CreatePropertyDto
    {
        public string Location { get; set; }            
        public string PropertyType { get; set; }        
        public int SizeSqm { get; set; }                
        public string Features { get; set; }            
        public int YearBuilt { get; set; }              
        public decimal? ActualPrice { get; set; }       
        public int AgentId { get; set; }
    }
}
