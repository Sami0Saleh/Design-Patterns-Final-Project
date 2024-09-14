using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unseen_Design_Pattern
{
    class MagicAttack : IAttackStrategy
    {
        public void Attack()
        {
            Console.WriteLine("You cast a fireball at the enemy!");
        }
    }
}
