using System;
using System.Collections.Generic;
using Assets.Level.Editor_;
using UnityEngine;

namespace Assets.Features.Level.Editor_
{
	[Serializable]
	public class FlatLayout
	{
		[Serializable]
		public class PuzzleObject
		{
			[SerializeField]
			public string Type;
			[SerializeField]
			public TilePos Position;

			[SerializeField]
			public List<Property> Properties = new List<Property>();

			[Serializable]
			public class Property
			{
				public string Key;
				public string Value;
				public string Type;
			}
		}

		[SerializeField]
		public List<PuzzleObject> Objects = new List<PuzzleObject>();

		[SerializeField]
		public List<NodeConnection> Connections = new List<NodeConnection>();
	}
}