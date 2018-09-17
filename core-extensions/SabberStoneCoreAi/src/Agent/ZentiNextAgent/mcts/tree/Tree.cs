using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCoreAi.POGame;

namespace SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts.tree
{
    class Tree
    {
		Node root;

		public Tree(POGame.POGame poGame) {
			this.root = new Node(poGame);
		}

		public Node getRoot() {
			return root;
		}
		
	}
}
