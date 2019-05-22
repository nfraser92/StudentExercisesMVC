using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Cohort
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(55)]
        [Display(Name = "Cohort Name")]
        public string Designation { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
