﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        //public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}