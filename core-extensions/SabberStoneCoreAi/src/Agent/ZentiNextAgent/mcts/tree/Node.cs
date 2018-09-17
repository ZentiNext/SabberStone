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
		int visits = 0;
		int wins = 0;
		double probability = 0.0;
		PlayerTask nodeTask;

		

		public Node(POGame.POGame state)
		{
			this.state = state;
			this.childArray = new List<Node>();
			List<PlayerTask> options = state.CurrentPlayer.Options();
			Dictionary<PlayerTask, POGame.POGame> simulation = state.Simulate(options);
			this.state = null;
			System.GC.Collect();
			foreach (PlayerTask playerTask in options) {
				childArray.Add(new Node(simulation.GetValueOrDefault(playerTask),this,playerTask));
			}
		}

		public Node(POGame.POGame state, Node parent, PlayerTask playerTask) {
			this.state = state;
			this.parent = parent;
			this.nodeTask = playerTask;

			if (playerTask.PlayerTaskType == PlayerTaskType.END_TURN){
				double reward = Reward.getReward(this.state);
				this.state = null;
				System.GC.Collect();
				//this.wins += reward;
				this.visits++;
				this.probability = reward;
				if(this.parent != null)
				{
					updateParents(this.parent , reward);
				}
			}else{
				
				this.childArray = new List<Node>();
				List<PlayerTask> options = this.state.CurrentPlayer.Options();
				Dictionary<PlayerTask, POGame.POGame> simulation = state.Simulate(options);
				foreach (PlayerTask pTask in options)
				{
					childArray.Add(new Node(simulation.GetValueOrDefault(pTask), this, pTask));
				}
				this.state = null;
				System.GC.Collect();

			}
		}

		private void updateParents(Node parent ,double reward)
		{
			//parent.wins += reward;
			parent.visits++;
			if (parent.probability < reward) {
				parent.probability = reward;
			}
			//parent.probability = parent.wins / parent.visits;

			if (parent.parent != null)
			{
				updateParents(parent.parent, reward);
			}
		}

		public Node(POGame.POGame state, Node parent, List<Node> childArray)
		{
			this.state = state;
			this.parent = parent;
			this.childArray = childArray;
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


		public List<Node> getChildArray()
		{
			return childArray;
		}

		public double getProbability() {
			return probability;
		}

		public PlayerTask getNodeTask()
		{
			return nodeTask;
		}

		

		
	}
}
