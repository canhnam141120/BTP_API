﻿namespace BTP_API.Helpers
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int NumberOfRecords { get; set; }
    }
}
