﻿using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;
public class RegistrationModel
{
    [Required(ErrorMessage = "Event Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Event Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Event ID is required")]
    public int EventId { get; set; }
}
