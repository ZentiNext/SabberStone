using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts;

namespace SabberStoneCoreAi.Agent
{
	class ZentiAgent : AbstractAgent
	{
		
		public override void FinalizeAgent()
		{
		}

		public override void FinalizeGame()
		{
		}

		public override PlayerTask GetMove(SabberStoneCoreAi.POGame.POGame poGame)
		{
			return MonteCarloTreeSearch.findNextMove(poGame);
		}

		public override void InitializeAgent()
		{
	
		}

		public override void InitializeGame()
		{
		}

		
	}
}
