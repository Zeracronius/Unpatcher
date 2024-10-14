using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unpatcher.Patches;
using Unpatcher.UserInterface;
using Unpatcher.UserInterface.TreeBox;
using Verse;

namespace Unpatcher.Mod
{
	internal class UnpatcherSettings : ModSettings
	{
		private FilterTreeBox? _nodes;
		private Dictionary<string, bool> _optionsDictionary;
		private List<PatchBase>? _patches;

		public const string HumanoidAlienRacePackageId = "rimworld.erdelf.alien_race.main";

        public UnpatcherSettings()
        {
			_optionsDictionary = new Dictionary<string, bool>();
		}


		public void GenerateSettings(List<PatchBase> patches)
		{
			List<TreeNode_FilterBox> nodes = new List<TreeNode_FilterBox>();

			foreach (PatchBase patch in patches)
			{
				patch.Enabled = _optionsDictionary.TryGetValue(patch.Key);
				patch.Apply();
				nodes.Add(new TreeNode_FilterBox(patch.SettingsKey, callback: (in Rect x) =>
					Widgets.Checkbox(x.position, ref patch.Enabled, x.height)));
			}
			_patches = patches;
			_nodes = new FilterTreeBox(nodes);
		}

		public void DrawSettings(Rect rect)
		{
			_nodes?.Draw(rect);

			if (_patches != null)
			{
				foreach (var patch in _patches)
				{
					patch.Apply();
					_optionsDictionary[patch.Key] = patch.Enabled;
				}
			}
		}

		public override void ExposeData()
		{
			base.ExposeData();

			Scribe_Collections.Look(ref _optionsDictionary, "PatchOptions");
			if (_optionsDictionary == null)
				_optionsDictionary = new Dictionary<string, bool>();
		}
	}
}
