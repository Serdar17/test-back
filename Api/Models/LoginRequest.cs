using System.ComponentModel.DataAnnotations;
using Api.Attributes;

namespace Api.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Поле phone не может быть пустым")]
    [CustomPhone(ErrorMessage = "Длина номера должна быть равной 11, начинаться с 7 и содержать только цифры")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Поле password не может быть пустым")]
    [StringLength(20, ErrorMessage = "Длина пароля не может быть больше 20")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}