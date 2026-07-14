using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CMD_CLASS_RPG
{
    public class Mage : Character
    {
        private int _HEAL;

        public Mage(int mhealth, int mmana, int damage, int heal) : base(mhealth, mmana, damage)
        {
            _HEAL = heal;
        }

        public int HEAL { get { return _HEAL; } set { _HEAL = value; } }

        public override void action(Character target)
        {
            target.HEALTH += _HEAL;
        }

        public void action(List<Character> targets)
        {
            if (this.MANA >= 2)
            {
                this.MANA -= 2;
                for (int i = 0; i < targets.Count; i++)
                {
                    action(targets[i]);
                }
                Console.WriteLine("Wszyscy towarzysze uleczeni o: " + this.HEAL);
            }
            else
            {
                Console.WriteLine("Niewystarczająca ilość many");
            }
        }

        public void attack(List<Character> targets)
        {
            Console.WriteLine("Mag uderza we wszystkie moby, zadając " + this.DAMAGE + " każdemu z nich");

            foreach (var enemy in targets)
            {
                enemy.HEALTH -= this.DAMAGE;
            }
        }
    }
}
