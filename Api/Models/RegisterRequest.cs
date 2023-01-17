using System.ComponentModel.DataAnnotations;
using Api.Attributes;

namespace Api.Models;

public class RegisterRequest  
{
    [Required(ErrorMessage = "Не указано ФИО")]
    [StringLength(250)]
    public string FIO { get; set; }

    [Required(ErrorMessage = "Не указан номер телефона")]
    [CustomPhone(ErrorMessage = "Длина номера должна быть равной 11, начинаться с 7 и содержать только цифры")]
    public string Phone { get; set; }
    

    [Required(ErrorMessage = "Не указан электронный адрес")]
    [CustomEmail(ErrorMessage = "Некорректный электронный адрес")]
    [StringLength(150)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Поле Password не заполнено")]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "Длина поля не может быть больше 20")]
    public string Password { get; set; }
         
    [Required(ErrorMessage = "Поле PasswordConfirm не заполнено")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "Длина поля не может быть больше 20")]
    public string PasswordConfirm { get; set; }
}