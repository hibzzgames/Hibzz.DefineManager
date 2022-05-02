using UnityEditor;

namespace Hibzz.DefineManager
{
    public static class Manager
    {
        /// <summary>
        /// Adds the given string as a new define, if it doesn't exist
        /// </summary>
        /// <param name="define">The define string to add</param>
        public static void AddDefine(string define)
		{
            // Get the define string from the player settings, which is a 
            // semicolon seperated list of strings, and we split it
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            string[] existingDefines = definesString.Split(';');

            // check if the define already exists, if not add it to the list
            foreach(string existingDefine in existingDefines)
			{
                if(existingDefine == define) { return; }
			}

            // Add the define to the list of defines and update the info to the PlayerSettings
            definesString += $";{define}";
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, definesString);
        }

        // Remove a define
        public static void RemoveDefine(string define)
		{
            // Get the define string from the player settings, which is a 
            // semicolon seperated list of strings, and we split it
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            string[] existingDefines = definesString.Split(';');

            // reset the output string and add every other define but the define that needs to be removed
            definesString = string.Empty;
            foreach(string existingDefine in existingDefines)
			{
                if(existingDefine == define) { continue; }
                definesString += $"{existingDefine};";
			}

            // Now push the changes back to player settings
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, definesString);
        }

        // Makes sure that a 3rd party shouldnt be able to inherit this class
        internal static void Protection() { }
    }
}
