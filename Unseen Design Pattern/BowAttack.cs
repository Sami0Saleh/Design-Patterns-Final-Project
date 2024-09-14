using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unseen_Design_Pattern
{
    class BowAttack : IAttackStrategy
    {
        public void Attack()
        {
            Console.WriteLine("You shoot an arrow at the enemy!");
        }
    }
}
