﻿namespace FreeYourFridge.API.Models
{
    public class Instruction
    {
        public string name { get; set; }
        public Step[] steps { get; set; }
    }
}