using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unseen_Design_Pattern
{
    class Character
    {
        private IAttackStrategy attackStrategy;

        public Character(IAttackStrategy attackStrategy)
        {
            this.attackStrategy = attackStrategy;
        }

        public void SetAttackStrategy(IAttackStrategy newAttackStrategy)
        {
            this.attackStrategy = newAttackStrategy;
        }

        public void Attack()
        {
            attackStrategy.Attack();
        }
    }
}
