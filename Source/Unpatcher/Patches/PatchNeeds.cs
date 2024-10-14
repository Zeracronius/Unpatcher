using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unpatcher.Patches
{
	internal class PatchNeeds : PatchBase
	{
		MethodBase _targetMethod1 = AccessTools.PropertySetter(typeof(Need), nameof(Need.CurLevel));
		MethodBase _targetMethod2 = AccessTools.PropertySetter(typeof(Need), nameof(Need.CurLevelPercentage));

		HarmonyMethod _patchMethod1 = AccessTools.Method("AlienRace.HarmonyPatches:NeedLevelPostfix");

		public PatchNeeds(Harmony instance)
			: base(instance)
		{
			if (_targetMethod1 == null || _targetMethod2 == null || _patchMethod1 == null)
				throw new ArgumentNullException("Unpatcher can't find required methods for Needs");

			instance.Unpatch(_targetMethod1, HarmonyPatchType.Postfix, Mod.UnpatcherSettings.HumanoidAlienRacePackageId);
			instance.Unpatch(_targetMethod2, HarmonyPatchType.Postfix, Mod.UnpatcherSettings.HumanoidAlienRacePackageId);
		}

		public override string SettingsKey => "Unpatch Needs";
		public override string TooltipKey => "Disable patches used to allow body addons to respond to Needs.";

		protected override void Patch(Harmony instance)
		{
			instance.Patch(_targetMethod1, postfix: _patchMethod1);
			instance.Patch(_targetMethod2, postfix: _patchMethod1);
		}

		protected override void Unpatch(Harmony instance)
		{
			instance.Unpatch(_targetMethod1, HarmonyPatchType.Postfix);
			instance.Unpatch(_targetMethod2, HarmonyPatchType.Postfix);
		}
	}
}
