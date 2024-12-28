using System.Net;
using com.mastama.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace com.mastama.Helper;

public class ResponseHelper
{
    public static BaseResponse BuildResponse(HttpStatusCode httpStatus, string serviceId, string constantCode, string responseDesc, object data)
    {
        var responseCode = $"{(int)httpStatus}{serviceId}{constantCode}";
        return new BaseResponse(responseCode, responseDesc, data);
    }
}
