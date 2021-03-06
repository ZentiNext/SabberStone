﻿using System;
using SabberStoneCore.Model;
using SabberStoneCore.Enums;
using SabberStoneCore.Kettle;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Tasks;

namespace SabberStoneCore.Actions
{
	public static partial class Generic
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		public static IPlayable DrawCard(Controller c, Card card)
		{
			return DrawCardBlock.Invoke(c, card);
		}

		public static IPlayable Draw(Controller c, IPlayable cardToDraw = null)
		{
			return DrawBlock.Invoke(c, cardToDraw);
		}

		public static Func<Controller, Card, IPlayable> DrawCardBlock
			=> delegate (Controller c, Card card)
			{
				IPlayable playable = Entity.FromCard(c, card);
				//c.NumCardsDrawnThisTurn++;
				AddHandPhase.Invoke(c, playable);
				return playable;
			};

		public static Func<Controller, IPlayable, IPlayable> DrawBlock
			=> delegate (Controller c, IPlayable cardToDraw)
			{
				if (!PreDrawPhase.Invoke(c))
					return null;

				IPlayable playable = DrawPhase.Invoke(c, cardToDraw);
				//c.NumCardsToDraw--; 

				if (AddHandPhase.Invoke(c, playable))
				{
					// DrawTrigger vs TOPDECK ?? not sure which one is first

					if (cardToDraw == null)
					{
						c.Game.TaskQueue.StartEvent();
						c.Game.TriggerManager.OnDrawTrigger(playable);
						c.Game.ProcessTasks();
						c.Game.TaskQueue.EndEvent();
					}

					ISimpleTask clone = playable.Power?.TopdeckTask?.Clone();
					if (clone != null)
					{
						clone.Game = c.Game;
						clone.Controller = c;
						clone.Source = playable;

						if (c.Game.History)
						{
							// TODO: triggerkeyword: TOPDECK
							c.Game.PowerHistory.Add(
								PowerHistoryBuilder.BlockStart(BlockType.TRIGGER, playable.Id, "", 0, 0));
						}

						c.SetasideZone.Add(c.HandZone.Remove(playable));

						c.Game.Log(LogLevel.INFO, BlockType.TRIGGER, "TOPDECK",
							!c.Game.Logging ? "" : $"{playable}'s TOPDECK effect is activated.");
						clone.Process();
						if (c.Game.History)
							c.Game.PowerHistory.Add(
								PowerHistoryBuilder.BlockEnd());
					}
				}

				return playable;
			};

		private static Func<Controller, bool> PreDrawPhase
			=> delegate (Controller c)
			{
				if (c.DeckZone.IsEmpty)
				{
					int fatigueDamage = c.Hero.Fatigue == 0 ? 1 : c.Hero.Fatigue + 1;
					DamageCharFunc(c.Hero, c.Hero, fatigueDamage, false);
					return false;
				}
				return true;
			};

		private static Func<Controller, IPlayable, IPlayable> DrawPhase
			=> delegate (Controller c, IPlayable cardToDraw)
			{
				IPlayable playable = c.DeckZone.Remove(cardToDraw ?? c.DeckZone.TopCard);

				c.Game.Log(LogLevel.INFO, BlockType.ACTION, "DrawPhase", !c.Game.Logging ? "" : $"{c.Name} draws {playable}");

				c.NumCardsDrawnThisTurn++;
				c.LastCardDrawn = playable.Id;

				return playable;
			};
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
