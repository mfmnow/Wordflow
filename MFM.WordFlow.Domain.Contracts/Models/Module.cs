namespace MFM.WordFlow.Domain.Contracts.Models
{
    public class Module
    {
        public int Id { get; set; }
        public int[] DependenciesIds { get; set; }
        public bool Loaded { get; set; }
    }
}
