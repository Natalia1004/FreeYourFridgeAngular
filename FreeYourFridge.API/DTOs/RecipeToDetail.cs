﻿namespace FreeYourFridge.API.DTOs
{
    public class RecipeToDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int ReadyInMinutes { get; set; }
        public double SpoonacularScore { get; set; }
    }
}