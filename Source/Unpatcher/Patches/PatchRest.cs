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
	internal class PatchRest : PatchBase
	{
		MethodBase _targetMethod = AccessTools.Method(typeof(RestUtility), nameof(RestUtility.CanUseBedEver));
		HarmonyMethod _patchMethod = AccessTools.Method("AlienRace.HarmonyPatches:CanUseBedEverPostfix");

		public PatchRest(Harmony instance)
			: base(instance)
		{
			if (_targetMethod == null || _patchMethod == null)
				throw new ArgumentNullException("Unpatcher can't find required methods for Rest");

			instance.Unpatch(_targetMethod, HarmonyPatchType.Postfix, Mod.UnpatcherSettings.HumanoidAlienRacePackageId);
		}

		public override string SettingsKey => "Unpatch Rest";
		public override string TooltipKey => "Disable patches used to allow HAR races to require special beds. (Impact scales with number of sleeping pawns.)";

		protected override void Patch(Harmony instance)
		{
			instance.Patch(_targetMethod, postfix: _patchMethod);
		}

		protected override void Unpatch(Harmony instance)
		{
			instance.Unpatch(_targetMethod, HarmonyPatchType.Postfix);
		}
	}
}
