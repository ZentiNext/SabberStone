using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCoreAi.POGame;
using System.Linq;

namespace SabberStoneCoreAi.src.Agent.ZentiNextAgent.mcts.tree
{
    class Node
    {
		POGame.POGame state;
		Node parent;
		List<Node> childArray;

		public Node()
		{
			
			childArray = new List<Node>();
		}

		public Node(POGame.POGame state)
		{
			this.state = state;
			childArray = new List<Node>();
		}

		public Node(POGame.POGame state, Node parent, List<Node> childArray)
		{
			this.state = state;
			this.parent = parent;
			this.childArray = childArray;
		}

		public Node(Node node)
		{
			this.childArray = new List<Node>();
			this.state = new POGame.POGame(node.getState());
			if (node.getParent() != null)
				this.parent = node.getParent();
			List<Node> childArray = node.getChildArray();
			foreach (Node child in childArray)
			{
				this.childArray.Add(new Node(child));
			}
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

		public void setParent(Node parent)
		{
			this.parent = parent;
		}

		public List<Node> getChildArray()
		{
			return childArray;
		}

		public void setChildArray(List<Node> childArray)
		{
			this.childArray = childArray;
		}

		public Node getRandomChildNode()
		{
			int noOfPossibleMoves = this.childArray.Count;
			int selectRandom = (int)(new Random().Next(noOfPossibleMoves) );
			return this.childArray[selectRandom];
		}

		public Node getChildWithMaxScore()
		{
			return this.childArray.Max();
			//Not yet fully implemented
		}
	}
}
