using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD_CLASS_RPG
{
    public class Character
    {
        private int _HEALTH;
        private int _MAX_HEALTH;
        private int _MANA;
        private int _MAX_MANA;

        private int _DAMAGE;

        public Character(int mhealth, int mmana,int damage)
        {
            _MAX_HEALTH = mhealth;
            _MAX_MANA = mmana;
            _DAMAGE = damage;

            _HEALTH = mhealth;
            _MANA = mmana;
        }

        public virtual void action(Character target) { } //specjalne akcje postaci

        public int HEALTH { get { return _HEALTH; } set { _HEALTH = value; if (_HEALTH > _MAX_HEALTH)  _HEALTH = MAX_HEALTH;  } }
        public int MAX_HEALTH { get { return _MAX_HEALTH; } set { _MAX_HEALTH = value; } }
        public int MANA { get { return _MANA; } set { _MANA = value; } }
        public int MAX_MANA { get { return _MAX_MANA; } set { _MAX_MANA = value; } }
        public int DAMAGE { get { return _DAMAGE; } set { _DAMAGE = value; } }


        public void restore_max_stats()
        {
            _HEALTH = _MAX_HEALTH;
            _MANA = _MAX_MANA;
        }

        public virtual void attack(Character target) { 
        
            if (target == null) return;
            target.HEALTH -= this.DAMAGE;
            Console.Clear();
            Console.WriteLine(this.ToString() + " zaatakował, zadając przeciwnikowi " + this.DAMAGE + ". Mobowi pozostało " + target.HEALTH + " HP");
        }


    }
}
