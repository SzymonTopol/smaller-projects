using CMD_CLASS_RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CMD_CLASS_RPG
{
    public class Warrior : Character
    {
        private int _DAMAGE_INCREASE;

        public Warrior(int mhealth, int mmana, int damage, int damage_increase) : base(mhealth, mmana, damage) {
            _DAMAGE_INCREASE = damage_increase;
        }

        public int DAMAGE_INCREASE { get { return  _DAMAGE_INCREASE; } set { _DAMAGE_INCREASE=value; }}

        public override void action(Character target)
        {
            if (this.MANA >= 2)
            {
                this.MANA -= 2;
                target.DAMAGE += _DAMAGE_INCREASE;
                Console.WriteLine(target.ToString() + " DAMAGE ZWIĘKSZONY DO " + target.DAMAGE);
            }
            else
            {
                Console.WriteLine("Niewystarczająca ilość many");
            }
            
        }

    }
}
