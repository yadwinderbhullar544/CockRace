using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockRace
{
    static class Factory
    {
        public static Punter CreatePunter(string name)
        {
            if(name == "Yadwinder")
            {
                return new Yadwinder();
            }
            if(name == "Love")
            {
                return new Love();
            }
            if (name == "Rajinder")
            {
                return new Rajinder();
            }

            return null;
            

        }

    }
}
