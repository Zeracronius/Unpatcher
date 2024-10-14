using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unpatcher.Patches
{
	internal class PatchThoughts : PatchBase
	{
		MethodBase _targetMethod1 = AccessTools.Method(typeof(ThoughtUtility), nameof(ThoughtUtility.CanGetThought));
		MethodBase _targetMethod2 = AccessTools.Method(typeof(SituationalThoughtHandler), name: "TryCreateThought");

		HarmonyMethod _patchMethod1 = AccessTools.Method("AlienRace.HarmonyPatches:CanGetThoughtPostfix");
		HarmonyMethod _patchMethod2 = AccessTools.Method("AlienRace.HarmonyPatches:TryCreateThoughtPrefix");

		public PatchThoughts(Harmony instance)
			: base(instance)
		{

			if (_targetMethod1 == null || _targetMethod2 == null || _patchMethod1 == null)
				throw new ArgumentNullException("Unpatcher can't find required methods for Rest");

			instance.Unpatch(_targetMethod1, HarmonyPatchType.Postfix, Mod.UnpatcherSettings.HumanoidAlienRacePackageId);
			instance.Unpatch(_targetMethod2, HarmonyPatchType.Prefix, Mod.UnpatcherSettings.HumanoidAlienRacePackageId);
		}

		public override string SettingsKey => "Unpatch Thoughts";
		public override string TooltipKey => "Disable patches used to allow HAR races to block thoughts. (Impact scales with number of pawns)";

		protected override void Patch(Harmony instance)
		{
			instance.Patch(_targetMethod1, postfix: _patchMethod1);
			instance.Patch(_targetMethod2, prefix: _patchMethod2);
		}

		protected override void Unpatch(Harmony instance)
		{
			instance.Unpatch(_targetMethod1, HarmonyPatchType.Postfix);
			instance.Unpatch(_targetMethod2, HarmonyPatchType.Prefix);
		}
	}
}
