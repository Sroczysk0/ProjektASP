namespace Projekt01.Models
{
    public class game_platform
    {
        public int id { get; set; }
        public int game_publisher_id { get; set; } 
        public int platform_id { get; set; }      
        public int release_year { get; set; }     

        public platform platform { get; set; }
    }

}