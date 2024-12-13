namespace Game.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Scripts.Configs;
	using UnityEngine;

	public interface IQuizBrain
	{
		void GenerateGoals( LevelConfig[] levels );
		CardConfig[,] GetBoard(LevelConfig lvl);
		CardConfig GetGoal( LevelConfig levelConfig );
	}
	
	public class QuizBrain: IQuizBrain
	{
		Dictionary< string, CardConfig > _cachedGoals = new Dictionary< string, CardConfig>();

		public void GenerateGoals( LevelConfig[] levels )
		{
			_cachedGoals.Clear();
			foreach (var cfg in levels)
			{
				CardConfig goal = GetRandom(cfg.Bundles);

				var c = 0;
				
				while (_cachedGoals.Values.Contains(goal))
				{
					goal = GetRandom(cfg.Bundles);

					if (c >= 100)
					{
						Debug.Log("WARNING: Too many goals");
						break;
					}
					c++;
				}
				
				_cachedGoals.Add(cfg.Identifier, goal);
			}
		}
		
		public CardConfig[,] GetBoard(LevelConfig lvl)
		{
			CardConfig[,] cards = new CardConfig[lvl.Bundles.Length, lvl.Bundles[0].Cards.Length];
			
			for (int i = 0; i < lvl.Bundles.Length; i++)
			{
				for (int j = 0; j < lvl.Bundles[i].Cards.Length; j++)
				{
					var card = lvl.Bundles[i].Cards[j];
					
					cards[i, j] = card;
				}
			}

			return cards;
		}

		public CardConfig GetGoal( LevelConfig levelConfig )
		{
			var cfg = _cachedGoals[levelConfig.Identifier];
			return cfg;
		}
		
		CardConfig GetRandom(CardsBundleConfig[] bundles)
		{
			if (bundles == null || bundles.Length == 0)
			{
				throw new ArgumentException("Bundles array is null or empty.");
			}

			var cards = bundles
				.SelectMany(b => b.Cards)
				.ToList();
			
			int randIndex = UnityEngine.Random.Range(0, cards.Count());

			return cards[randIndex];
		}
	}
}