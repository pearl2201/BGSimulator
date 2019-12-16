﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BGSimulator.Utils.RandomUtils;

namespace BGSimulator.Model
{
    public class Battle
    {
        public int Round { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public Board PlayerABoard { get; set; }
        public Board PlayerBBoard { get; set; }

        public void Start()
        {
            var attacker = FirstAttacker();
            var defender = attacker.CurrentMatch;

            var attackerBoard = attacker.Board.Clone(); //clone because we want the state at the start of the fight.
            var defenderBoard = defender.Board.Clone();

            while (!attackerBoard.IsEmpty && !defenderBoard.IsEmpty)
            {
                IMinion attackingMinion = attackerBoard.GetNextAttacker();
                IMinion defendingMinion = defenderBoard.GetRandomDefender();

                Console.WriteLine(string.Format(@"{0} {1} Is Attacking {2} {3}", attackerBoard.Player.Name, attackingMinion.ToString(), defenderBoard.Player.Name, defendingMinion.ToString()));

                attackingMinion.DoAttack(defendingMinion);
                if (defendingMinion.IsDead)
                    defenderBoard.PlayedMinions.Remove(defendingMinion);
                if (attackingMinion.IsDead)
                    attackerBoard.PlayedMinions.Remove(attackingMinion);

                var temp = attackerBoard;
                attackerBoard = defenderBoard;
                defenderBoard = temp;
            }

        }

        private Player FirstAttacker()
        {
            if (PlayerABoard.PlayedMinions.Count > PlayerBBoard.PlayedMinions.Count)
            {
                return PlayerA;
            }
            else if (PlayerABoard.PlayedMinions.Count < PlayerBBoard.PlayedMinions.Count)
            {
                return PlayerB;
            }
            else
            {
                return FlipCoin();
            }

        }

        private Player FlipCoin()
        {
            return RandomNumber(0, 100) < 50 ? PlayerA : PlayerB;
        }
    }
}