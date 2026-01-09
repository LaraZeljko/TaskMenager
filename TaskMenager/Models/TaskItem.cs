using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMenager.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public int? AssignedUserId { get; set; }

        public int CreatedByUserId { get; set; }


        //[ForeignKey("User")]
        //public int? id { get; set; }
        //public User User { get; set; }

    }
}
