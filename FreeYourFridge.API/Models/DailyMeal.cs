﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FreeYourFridge.API.Models
{
    public class DailyMeal
    {
        [Key]
        public Guid LocalId { get; set; }

        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime TimeOfLastMeal { get; set; }

        public int Id { get; set; }
        public int Grams { get; set; }
        public string? UserRemarks { get; set; }
        public int Calories { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public int Protein { get; set; }
        public int CreatedBy { get; set; }

    }
}
