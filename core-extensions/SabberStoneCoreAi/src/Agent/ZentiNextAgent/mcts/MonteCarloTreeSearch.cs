using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts.tree;

namespace SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts
{
    class MonteCarloTreeSearch
    {
		public static PlayerTask findNextMove(POGame.POGame poGame) {
			tree.Tree tree = new tree.Tree(poGame);
			return findBestChild(tree);
		}

		private static PlayerTask findBestChild(Tree tree)
		{
			List<Node> options = tree.getRoot().getChildArray();
			double max = options[0].getProbability();
			Node maxNode = options[0];
			foreach (Node node in options) {
				if (max < node.getProbability()) {
					max = node.getProbability();
					maxNode = node;
				}
			}

			return maxNode.getNodeTask();

			
		}
	}
}
