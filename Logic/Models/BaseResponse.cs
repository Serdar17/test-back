namespace Logic.Models;

public class BaseResponse<T>
{
    public string Description { get; set; }
    
    public string PhoneDescription { get; set; }

    public int StatusCode { get; set; }
    
    public string Token { get; set; }
        
    public T Claims { get; set; }
}