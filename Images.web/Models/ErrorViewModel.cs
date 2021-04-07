using Images.data;
using System;

namespace Images.web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class SingleImageViewModel
    {
        public Image Image { get; set; }
        public string HasBeenHere { get; set; }
    }
}
