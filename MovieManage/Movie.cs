using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManage
{
    class Movie
    {
        public string MovNum { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string RunTime { get; set; }
        public bool IsBooked { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        
    }
}
