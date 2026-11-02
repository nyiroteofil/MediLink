using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Data.DTOs;

public class PatientUpdateDTO
{
    public string Username { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}