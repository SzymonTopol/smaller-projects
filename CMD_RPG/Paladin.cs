using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CMD_CLASS_RPG
{
    public class Paladin : Character
    {

        public Paladin(int mhealth, int mmana, int damage) : base(mhealth, mmana, damage)
        {}
        public override void action(Character target)
        {
            if (this.MANA >= 2)
            {
                this.MANA -= 2;
                target.MANA = target.MAX_MANA;
                Console.WriteLine(target.ToString() + " MANA ODNOWIONA");

            }
            else
            {
                Console.WriteLine("Niewystarczająca ilość many");
            }
        }

    }
}
