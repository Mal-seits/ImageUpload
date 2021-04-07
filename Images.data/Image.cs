using System;
using System.Collections.Generic;
using System.Text;

namespace Images.data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
        public string FileName { get; set; }
   
    }
}
