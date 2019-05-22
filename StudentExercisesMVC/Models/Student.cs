using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(55)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        

        [Required]
        [StringLength(55)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(55)]
        [Display(Name = "Slack Handle")]
        public string SlackHandle { get; set; }

        [Required]
        public int CohortId { get; set; }

        public Cohort cohort { get; set; }

        public List<Exercise> exercises { get; set; } = new List<Exercise>();
    }
}
