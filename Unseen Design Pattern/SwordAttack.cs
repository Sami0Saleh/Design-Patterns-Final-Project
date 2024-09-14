using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unseen_Design_Pattern
{
    class SwordAttack : IAttackStrategy
    {
        public void Attack()
        {
            Console.WriteLine("You swing your sword at the enemy!");
        }
    }
}
