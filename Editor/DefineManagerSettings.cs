using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Hibzz.DefineManager
{
	[System.Serializable]
	internal class DefineManagerSettings
	{
		private const string defineSettingsPath = "ProjectSettings/DefineManagerSettings.asset";

		[SerializeField] internal List<DefineRegistrationData> DefineRegistery;
		[SerializeField] internal List<string> IgnoreAssemblyList;

		/// <summary>
		/// Constructor
		/// </summary>
		private DefineManagerSettings()
        {
			DefineRegistery = new List<DefineRegistrationData>();
			IgnoreAssemblyList = new List<string>();
        }

		/// <summary>
		/// A singleton instance
		/// </summary>
		private static DefineManagerSettings Instance;

		/// <summary>
		/// Get the define manager settings. If it doesn't exist create a new one
		/// </summary>
		internal static DefineManagerSettings GetOrCreateSettings()
        {
			// If we have singleton, then we return it
			if(Instance != null) { return Instance; }

			// if the file exists, populate the singleton and return it
			if(File.Exists(defineSettingsPath))
            {
				string json_string = File.ReadAllText(defineSettingsPath);
				Instance = JsonUtility.FromJson<DefineManagerSettings>(json_string);
				return Instance;
			}

			// file doesn't exist, so we initialize the singleton and save it to a file
			Instance = new DefineManagerSettings();
			SaveSettings();
			return Instance;
        }

		/// <summary>
		/// Save the Define Manager Settings
		/// </summary>
		internal static void SaveSettings()
        {
			string json = JsonUtility.ToJson(Instance);
			File.WriteAllText(defineSettingsPath, json);
		}
	}
}
