﻿namespace SShezzy.Authentication.Models
{
    public class ApiResponse
    {
        public ApiResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
