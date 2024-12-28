namespace com.mastama.DTOs;

public class BaseResponse
{
    public string ResponseCode { get; set; }
    public string ResponseDesc { get; set; }
    public object Data { get; set; }
    
    
    // constructor tanpa parameter
    public BaseResponse() {}

    // constructor dengan parameter
    public BaseResponse(string responseCode, string responseDesc, object data)
    {
        ResponseCode = responseCode;
        ResponseDesc = responseDesc;
        Data = data;
    }
}