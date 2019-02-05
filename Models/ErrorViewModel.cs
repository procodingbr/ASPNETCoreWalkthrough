using System;

namespace ProCoding.Demos.ASPNETCore.Walkthrough.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}