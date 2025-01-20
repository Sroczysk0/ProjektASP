using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projekt01.Models
{
    public class CreateGameViewModel
    {
        [Required(ErrorMessage = "Nazwa gry jest wymagana.")]
        public string GameName { get; set; }

        [Required(ErrorMessage = "Wybór gatunku jest wymagany.")]
        public int SelectedGenreId { get; set; }

        [Required(ErrorMessage = "Wybór co najmniej jednego wydawcy jest wymagany.")]
        public List<int> SelectedPublisherIds { get; set; } = new List<int>();

        public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
    }
}