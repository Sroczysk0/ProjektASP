namespace Projekt01.Models
{
    public class PaginatedGamesViewModel
    {
        public List<GameViewModel> Games { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalGames { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalGames / PageSize);
    }
}