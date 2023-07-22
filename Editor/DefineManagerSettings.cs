#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hibzz.DefineManager
{
	[System.Serializable]
	internal class DefineManagerSettings
	{
		private const string defineSettingsPath = "ProjectSettings/DefineManagerSettings.asset";

		[SerializeField] internal List<DefineRegistrationData> DefineRegistery;

		internal CategoryCollapseInfo CollapseInfo;

		/// <summary>
		/// Constructor
		/// </summary>
		private DefineManagerSettings()
		{
			DefineRegistery = new List<DefineRegistrationData>();
			CollapseInfo = new CategoryCollapseInfo();
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

	internal class CategoryCollapseInfo
	{
		// data structure to store the category collapsed info
		internal Dictionary<string, bool> collapseData;

		// constructor
		internal CategoryCollapseInfo()
		{
			collapseData = new Dictionary<string, bool>();
		}

		/// <summary>
		/// Is the requested category collapsed or not?
		/// </summary>
		internal bool IsCollapsed(string category)
		{
			// return the collapse data if it's available
			if(collapseData.ContainsKey(category))
			{
				return collapseData[category];
			}

			// else by default, it's false
			return false;
		}

		/// <summary>
		/// Toggle the collapse status of the requested category
		/// </summary>
		internal void Toggle(string category)
		{
			if(collapseData.ContainsKey(category))
			{
				collapseData[category] = !collapseData[category];
			}
		}
	}
}

#endif
