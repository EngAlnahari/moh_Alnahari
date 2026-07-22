using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Invitation//الادعاء
    {
        public int Id { get; set; }
        [Display(Name = " الادعاء ")]
        public string? Invit { get; set; }
        [Display(Name = "المشتكي نفسة او من ينوبة")]
        public string? Self { get; set; }
        [Display(Name = "رد المشكوبة")]
        public string? Reply { get; set; }

    }
}
