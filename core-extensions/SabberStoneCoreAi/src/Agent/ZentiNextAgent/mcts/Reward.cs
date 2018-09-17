using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Model.Entities;
using SabberStoneCoreAi.POGame;

namespace SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts
{
    class Reward
    {
		public static double getReward(POGame.POGame state) {
			int enemyHealth = state.CurrentPlayer.Hero.Health;
			int myHealth = state.CurrentOpponent.Hero.Health;
			int enemyPower = 0;
			int myPower = 0;
			Minion[] enemyMnions = state.CurrentPlayer.BoardZone.GetAll();
			foreach (Minion m in enemyMnions) {
				enemyPower += m.Cost;
			}
			Minion[] myMnions = state.CurrentOpponent.BoardZone.GetAll();
			foreach (Minion m in myMnions)
			{
				myPower += m.Cost;
			}
			
			return (myHealth - enemyHealth) + 0.5 * (myPower-enemyPower);
		}
    }
}
