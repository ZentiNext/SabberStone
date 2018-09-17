using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Model.Entities;
using SabberStoneCoreAi.POGame;

namespace SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts
{
    class Reward
    {
		public static double getReward(POGame.POGame state, POGame.POGame initState) {

			int myHealth = initState.CurrentPlayer.Hero.Health;
			int enemyHealth = initState.CurrentOpponent.Hero.Health;
			int enemyPower = 0;
			int myPower = 0;
			Minion[] myMnions = initState.CurrentPlayer.BoardZone.GetAll();
			foreach (Minion m in myMnions)
			{
				myPower += m.Cost;
			}
			Minion[] enemyMnions = state.CurrentOpponent.BoardZone.GetAll();
			foreach (Minion m in enemyMnions)
			{
				enemyPower += m.Cost;
			}

			int enemyHealth2 = state.CurrentPlayer.Hero.Health;
			int myHealth2 = state.CurrentOpponent.Hero.Health;			
			int enemyPower2 = 0;
			int myPower2 = 0;
			Minion[] enemyMnions2 = state.CurrentPlayer.BoardZone.GetAll();
			foreach (Minion m in enemyMnions2) {
				enemyPower2 += m.Cost;
			}
			Minion[] myMnions2 = state.CurrentOpponent.BoardZone.GetAll();
			foreach (Minion m in myMnions2)
			{
				myPower2 += m.Cost;
			}
			
			return  (myHealth2-myHealth)+(8*(enemyHealth-enemyHealth2))+(3*(myPower2-myPower))+(4*(enemyPower-enemyPower2));
		}
    }
}
