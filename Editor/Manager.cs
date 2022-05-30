using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Hibzz.DefineManager
{
	internal static class Manager
	{
		/// <summary>
		/// Adds the given string as a new define, if it doesn't exist
		/// </summary>
		/// <param name="define">The define string to add</param>
		internal static void AddDefine(string define)
		{
			// Get the define string from the player settings, which is a 
			// semicolon seperated list of strings, and we split it
			string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
			string[] existingDefines = definesString.Split(';');

			// check if the define already exists, if not add it to the list
			foreach (string existingDefine in existingDefines)
			{
				if (existingDefine == define) { return; }
			}

			// Add the define to the list of defines and update the info to the PlayerSettings
			definesString += $";{define}";
			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, definesString);
		}

		/// <summary>
		/// Remove the given define from the player define list
		/// </summary>
		/// <param name="define">The define to remove</param>
		internal static void RemoveDefine(string define)
		{
			// Get the define string from the player settings, which is a 
			// semicolon seperated list of strings, and we split it
			string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
			string[] existingDefines = definesString.Split(';');

			// reset the output string and add every other define but the define that needs to be removed
			definesString = string.Empty;
			foreach (string existingDefine in existingDefines)
			{
				if (existingDefine == define) { continue; }
				definesString += $"{existingDefine};";
			}

			// Now push the changes back to player settings
			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, definesString);
		}

		/// <summary>
		/// Does it contain the requested define?
		/// </summary>
		internal static bool ContainDefine(string define)
        {
			// Get the define string from the player settings, which is a 
			// semicolon seperated list of strings, and we split it
			string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
			string[] existingDefines = definesString.Split(';');

			// check if the define already exists, if yes return true
			foreach (string existingDefine in existingDefines)
			{
				if (existingDefine == define) { return true; }
			}

			// define not found
			return false;
		}

		/// <summary>
		/// Refreshes the define list
		/// </summary>
		internal static void RefreshList()
		{
			var settings = DefineManagerSettings.GetOrCreateSettings();
			settings.DefineRegistery = new List<DefineRegistrationData>();

			var methods = TypeCache.GetMethodsWithAttribute<RegisterDefineAttribute>();
			foreach(var method in methods)
			{
				// The method can't be validated, so skip this iteration
				if(!ValidateMethod(method)) { continue; }

				// the method is guaranteed to not accept any parameters and
				// to return DefineRegistrationData
				var data = method.Invoke(null, null) as DefineRegistrationData;

				// check for duplicate defines, else add it to the dictionary
				if (settings.DefineRegistery.Find((item) => item.Define.Equals(data.Define)) != null)
				{
					Debug.LogError($"Duplicate DEFINE key '{data.Define}'. Please use another DEFINE key to resolve conflict");
					continue;
				}

				// check if the data is initialized, if not initialize it
				if(!data.Initialized) { data.Initialize(); }

				// add to dictionary
				settings.DefineRegistery.Add(data);
			}

			// Sort by assembly name then by the display name
			settings.DefineRegistery.Sort();

			// Perform collapse data refresh
			RefreshCollapsedCategoryInfo();

			// finally save the refreshed content
			DefineManagerSettings.SaveSettings();
		}

		/// <summary>
		/// Refresh the collapsed category info
		/// </summary>
		private static void RefreshCollapsedCategoryInfo()
        {
			var settings = DefineManagerSettings.GetOrCreateSettings();
			
			var cacheData = settings.CollapseInfo.collapseData;
			settings.CollapseInfo.collapseData = new Dictionary<string, bool>();

			string lastCategory = string.Empty;
			foreach(var defineData in settings.DefineRegistery)
            {
				// skip if we've already processed the categorry
				if(defineData.Category == lastCategory) { continue; }

				// if we already have info on whether the category was
				// collapsed or not, we use it during the refresh
				bool collapsed = false;
				if(cacheData.ContainsKey(defineData.Category))
                {
					collapsed = cacheData[defineData.Category];
                }

				// store the info to the new category info inside settings
				settings.CollapseInfo.collapseData[defineData.Category] = collapsed;

				// update the last category flag
				lastCategory = defineData.Category;
			}
        }

		/// <summary>
		/// Validate the method to return DefineRegistrationData and take no parameters
		/// </summary>
		/// <param name="method">The method to check</param>
		/// <returns>Is the given method valid?</returns>
		private static bool ValidateMethod(MethodInfo method)
		{
			// Should return DefineRegistrationData
			if (method.ReturnType != typeof(DefineRegistrationData))
			{
				Debug.LogError($"Method {method.Name} in {method.DeclaringType} doesn't return DefineRegistrationData");
				return false;
			}

			// Shouldn't accept any parameters
			if (method.GetParameters().Length != 0)
			{
				Debug.LogError($"Method {method.Name} in {method.DeclaringType} shouldn't accept any parameters");
				return false;
			}

			return true;
		}
	}
}
#endif
