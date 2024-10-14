using HarmonyLib;
using RimWorld;
using System.Diagnostics.Eventing.Reader;
using Verse;

namespace Unpatcher.Mod
{

	[StaticConstructorOnStartup]
	internal static class Initializer
	{
		static Initializer()
		{
			UnpatcherMod.Current.Initialise();

			//Harmony Harmony = new Harmony("Zeracronius.Unpatcher");
			//Harmony.Unpatch(AccessTools.Method(typeof(RestUtility), nameof(RestUtility.CanUseBedEver)), HarmonyPatchType.Postfix, AlienRacesId);
			//Harmony.Unpatch(AccessTools.Method(typeof(ThoughtUtility), nameof(ThoughtUtility.CanGetThought)), HarmonyPatchType.Postfix, AlienRacesId);
			//Harmony.Unpatch(AccessTools.Method(typeof(SituationalThoughtHandler), name: "TryCreateThought"), HarmonyPatchType.Prefix, AlienRacesId);
			//Harmony.Unpatch(AccessTools.PropertySetter(typeof(Need), nameof(Need.CurLevel)), HarmonyPatchType.Postfix, AlienRacesId);
			//Harmony.Unpatch(AccessTools.PropertySetter(typeof(Need), nameof(Need.CurLevelPercentage)), HarmonyPatchType.Postfix, AlienRacesId);
		}
	}
}
