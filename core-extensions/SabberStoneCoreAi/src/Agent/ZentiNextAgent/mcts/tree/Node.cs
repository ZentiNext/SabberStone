using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCoreAi.POGame;
using System.Linq;
using SabberStoneCore.Tasks;

namespace SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts.tree
{
	class Node
	{
		POGame.POGame state;
		Node parent;
		List<Node> childArray;
		int visitCount = 0;
		double winScore = 0;
		PlayerTask nodeTask;

		public Node(POGame.POGame state) {
			this.state = state;
			this.childArray = new List<Node>();
		}

		public Node(POGame.POGame state, Node parent, PlayerTask nodeTask) {
			this.state = state;
			this.parent = parent;
			this.childArray = new List<Node>();
			this.nodeTask = nodeTask;
		}


		public POGame.POGame getState()
		{
			return state;
		}

		public void setState(POGame.POGame state)
		{
			this.state = state;
		}

		public Node getParent()
		{
			return parent;
		}

		internal Node getRandomChild()
		{
			return childArray[new Random().Next(childArray.Count)];
		}

		public List<Node> getChildArray()
		{
			return childArray;
		}

		public PlayerTask getNodeTask()
		{
			return nodeTask;
		}

		internal Node getMaxChild()
		{
			int maxCount = childArray[0].visitCount;
			Node child = childArray[0];
			foreach(Node n in childArray)
			{
				if (maxCount < n.visitCount) {
					maxCount = n.visitCount;
					child = n;
				}
			}
			return child;
		}

		public int getVisitCount() {
			return visitCount;
		}

		public double getWinScore() {
			return winScore;
		}

		public void addChild(Node child) {
			childArray.Add(child);
		}

		public void incrementVisit()
		{
			visitCount++;
		}

		public void addScore(double score) {
			winScore += score;
		}
		

		
	}
}
