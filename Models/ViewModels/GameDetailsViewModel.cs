namespace Projekt01.Models
{
    public class GameDetailsViewModel
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public List<PlatformDetailsViewModel> PlatformDetails { get; set; } = new();
    }

    public class PlatformDetailsViewModel
    {
        public string PlatformName { get; set; }
        public int ReleaseYear { get; set; }
        public List<RegionSalesViewModel> Regions { get; set; } = new();
    }

    public class RegionSalesViewModel
    {
        public string RegionName { get; set; }
        public double NumSales { get; set; }
    }
}