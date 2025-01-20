namespace Projekt01.Models
{
    public class game
    {
        public int id { get; set; }
        public int genre_id { get; set; }
        public string game_name { get; set; }
        public genre genre { get; set; }
    }
}