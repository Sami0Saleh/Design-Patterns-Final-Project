using System;

namespace Unseen_Design_Pattern
{
    class Program
    {
        static void Main()
        {
            Character hero = new Character(new SwordAttack());
            hero.Attack();

            // Changing attack strategy dynamically
            hero.SetAttackStrategy(new BowAttack());
            hero.Attack();

            hero.SetAttackStrategy(new MagicAttack());
            hero.Attack();
        }
    }
}
