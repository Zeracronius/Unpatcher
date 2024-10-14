using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Unpatcher.Patches;
using Verse;

namespace Unpatcher.Mod
{
	public class UnpatcherMod : Verse.Mod
	{
		internal static Harmony Harmony = new Harmony("DynamicTradeInterfaceMod");

#pragma warning disable CS8618 // Will always be initialized by constructor by rimworld.
		internal static UnpatcherMod Current;
		internal static UnpatcherSettings Settings;
#pragma warning restore CS8618

		public Harmony HarmonyInstance;
		internal List<PatchBase>? Patches;

		public UnpatcherMod(ModContentPack content) : base(content)
		{
			Current = this;
			Settings = GetSettings<UnpatcherSettings>();
			HarmonyInstance = new Harmony("Zeracronius.Unpatcher");
		}

		public void Initialise()
		{
			Patches = new List<PatchBase>
			{
				new PatchNeeds(HarmonyInstance),
				new PatchRest(HarmonyInstance),
				new PatchThoughts(HarmonyInstance),
			};

			Settings.GenerateSettings(Patches);
		}

		public override string SettingsCategory()
		{
			return "Unpatcher";
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			Settings.DrawSettings(inRect);
		}
	}
}