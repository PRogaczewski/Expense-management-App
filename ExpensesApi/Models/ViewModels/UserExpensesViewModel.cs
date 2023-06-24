﻿namespace ExpensesApi.Models.ViewModels
{
    public class UserExpensesViewModel
    {
        private UserExpensesViewModel() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
