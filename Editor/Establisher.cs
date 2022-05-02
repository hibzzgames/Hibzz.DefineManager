using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEditor;

namespace Hibzz.DefineManager
{
    internal class Establisher
    {
        /// <summary>
        /// If not added before, adds the define #ENABLE_DEFINE_MANAGER to the define list when the editor reloads
        /// </summary>
        internal static void Establish()
		{
            // Future improvements: look into calling it only once during reload?
            // Or another option is to add UI button to the new editor window which would add it

            // Check and add the script
            Manager.AddDefine("ENABLE_DEFINE_MANAGER");
		}
    }
}
