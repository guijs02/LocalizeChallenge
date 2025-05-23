﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Localiza.Core.Requests
{
    public class LoginUserRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public Guid UserId { get; set; }


    }
}
