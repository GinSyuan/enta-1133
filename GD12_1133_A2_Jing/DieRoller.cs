using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jing_Lab2
{
    public class DieRoller
    {

        public int Roll(int numberOfSides) 
        {
        
        Random random = new Random();
        return random.Next(numberOfSides);
        
        }




    }
}
