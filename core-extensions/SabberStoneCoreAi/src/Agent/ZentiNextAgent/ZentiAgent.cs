using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;

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
			if (poGame.CurrentPlayer.Options().Count>1) {
				return poGame.CurrentPlayer.Options()[1];
			}
			
			return poGame.CurrentPlayer.Options()[0];
		}

		public override void InitializeAgent()
		{
	
		}

		public override void InitializeGame()
		{
		}
	}
}
