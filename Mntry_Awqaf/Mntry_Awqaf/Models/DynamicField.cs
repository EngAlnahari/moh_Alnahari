using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mntry_Awqaf.Models
{
    public class DynamicField
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الحقل مطلوب")]
        [Display(Name = "اسم الحقل (مثال: مساحة الأرض)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "نوع الحقل مطلوب")]
        [Display(Name = "نوع الحقل")]
        public string Type { get; set; } // text, number, date, time, dropdown, checkbox, radio, file, email, password, textarea, multiselect

        [Display(Name = "الخيارات (مفصولة بفاصلة ،) إذا كان النوع قائمة أو خيارات")]
        public string? Options { get; set; } // For dropdowns, radios, checkboxes (comma separated)

        [Display(Name = "هل الحقل مطلوب؟")]
        public bool IsRequired { get; set; }

        public ICollection<FieldAssignment> Assignments { get; set; } = new List<FieldAssignment>();
    }
}
