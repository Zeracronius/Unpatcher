using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Unpatcher.Patches
{
	internal abstract class PatchBase
	{
		Harmony _instance;
		private bool? _currentState;
		public bool Enabled;

		public string Key;

		protected PatchBase(Harmony instance)
        {
			_instance = instance;
			Key = GetType().Name;
		}

        public void Apply()
		{
			if (_currentState == Enabled)
				return;

			if (Enabled)
			{
				Unpatch(_instance);
				Log.Message("Unpatched " + GetType().Name);
				_currentState = true;
			}
			else
			{
				Patch(_instance);
				Log.Message("Patched " + GetType().Name);
				_currentState = false;
			}
		}

		public abstract string SettingsKey { get; }
		public abstract string TooltipKey { get; }

		protected abstract void Patch(Harmony instance);
		protected abstract void Unpatch(Harmony instance);
	}
}
