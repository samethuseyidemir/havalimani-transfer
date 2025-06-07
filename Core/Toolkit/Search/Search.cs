using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Toolkit.Search
{
    public class SearchDto 
    {
        public int StartIndex { get; set; }
        public int MaxCount { get; set; }
        public OrderByDto? OrderBy { get; set; }
    }
    public class OrderByDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
