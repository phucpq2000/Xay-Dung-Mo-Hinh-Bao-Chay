using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTO
{
    
    public class RealtimeData
    {
        public float Humidity { get; set; }
        public float Tempurature { get; set; }
        public DateTime Time { get; set; }
    }
}