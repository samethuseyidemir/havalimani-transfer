using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Toolkit.ToolBox
{
    public static  class RandomFunc
    {
        public static int Run(int minValue,int maxValue)
        {
            Random rnd = new Random();
            return rnd.Next(minValue, maxValue);
        }
    }
}
