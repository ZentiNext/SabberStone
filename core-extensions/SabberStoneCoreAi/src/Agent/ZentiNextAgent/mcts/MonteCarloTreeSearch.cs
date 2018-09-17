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
		static tree.Tree tree;
		public static PlayerTask findNextMove(POGame.POGame poGame) {
			tree = new tree.Tree(poGame);
			long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			long end = start + 150;
			long time = start;
			while (time < end)
			{
				//Selection
				Node optimalNode = findOptimalNode(tree.getRoot());

				//Expantion
				if (optimalNode.getNodeTask() == null || optimalNode.getNodeTask().PlayerTaskType != PlayerTaskType.END_TURN) {
					expandNode(optimalNode);
				}

				//Simulation
				Node simulateNode = optimalNode;
				if (simulateNode.getChildArray().Count >0) {
					simulateNode = simulateNode.getRandomChild();
				}
				double simulationResult = simulatePlay(simulateNode);

				//Back Propagation
				backPropagation(simulateNode, simulationResult);

				time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			}
			return tree.getRoot().getMaxChild().getNodeTask();
		}

		private static void backPropagation(Node node, double result)
		{
			Node tempNode = node;
			while (tempNode != null) {
				tempNode.incrementVisit();
				tempNode.addScore(result);
				tempNode = tempNode.getParent();
			}
		}

		private static double simulatePlay(Node simulateNode)
		{
			Node tempNode = simulateNode;
			if (tempNode.getNodeTask().PlayerTaskType == PlayerTaskType.END_TURN)
			{
				return Reward.getReward(tempNode.getState(),tree.getRoot().getState());
			}

			List<PlayerTask> options = tempNode.getState().CurrentPlayer.Options();
			Dictionary<PlayerTask, POGame.POGame> states = tempNode.getState().Simulate(options);
			PlayerTask playerTask = options[new Random().Next(options.Count)];
			POGame.POGame state = states.GetValueOrDefault(playerTask);

			while (playerTask.PlayerTaskType != PlayerTaskType.END_TURN) {
				playerTask = state.CurrentPlayer.Options()[new Random().Next(state.CurrentPlayer.Options().Count)];
				state.Process(playerTask);				
			}
			return Reward.getReward(state, tree.getRoot().getState());
		}

		private static void expandNode(Node node)
		{
			List<PlayerTask> options = node.getState().CurrentPlayer.Options();
			Dictionary<PlayerTask, POGame.POGame> stateSpace = node.getState().Simulate(options);
			foreach(PlayerTask playerTask in options)
			{
				node.addChild(new Node(stateSpace.GetValueOrDefault(playerTask), node, playerTask));
			}
		}

		private static Node findOptimalNode(Node node)
		{
			Node optimalNode = node;
			while (optimalNode.getChildArray().Count != 0) {
				optimalNode = UCT.findOptimalUctNode(optimalNode);
			}
			return optimalNode;
		}
	}
}
