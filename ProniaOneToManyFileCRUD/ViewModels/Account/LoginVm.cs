﻿using System.ComponentModel.DataAnnotations;

namespace ProniaOneToManyFileCRUD.ViewModels.Account
{
    public class LoginVm
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
