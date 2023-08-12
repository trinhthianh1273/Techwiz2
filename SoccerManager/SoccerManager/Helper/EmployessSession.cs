using Microsoft.AspNetCore.Http;
public class EmployessSession 
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployessSession(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public bool IsEmployessSession()
    {
        if(_httpContextAccessor.HttpContext.Session.GetString("CustomerId") is null)
        {
            return false;
        }
        return true;
    }
}
