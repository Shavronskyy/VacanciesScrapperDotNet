using System.ComponentModel.DataAnnotations;

namespace VacanciesScrapper_DAL.Entities
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
    
        public string? ShortDescription { get; set; }
        [Required]
        public Company Company { get; set; } = new();

        public string? Salary { get; set; }

        public string? Location { get; set; }

        public string? CreationDate { get; set; }
        [Required]
        public string? Link { get; set; }
    }
}