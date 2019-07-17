using System;
using UnityEngine;
using UnityModManagerNet;

namespace XLNoclip {
	class Main {
		public static bool enabled;
		public static String modId;
		public static UnityModManager.ModEntry modEntry;

		static void Load(UnityModManager.ModEntry modEntry) {
			Main.modEntry = modEntry;
			Main.modId = modEntry.Info.Id;

			modEntry.OnToggle = OnToggle;
		}

		static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) {
			if (value == enabled) return true;
			enabled = value;

			if (enabled) {
				XLShredLib.ModMenu.Instance.gameObject.AddComponent<NoclipController>();
			} else {
				UnityEngine.Object.Destroy(XLShredLib.ModMenu.Instance.gameObject.GetComponent<NoclipController>());
			}

			return true;
		}
	}
}
