namespace Pokedex_v2_api.Models
{
    public class Pokemon
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }
        public long CoachId { get; set; }
    }
}
