#if UNITY_EDITOR

using UnityEditor;

namespace Hibzz.DefineManager
{
	internal class Establisher
	{
		/// <summary>
		/// If not added before, adds the define #ENABLE_DEFINE_MANAGER to the define list when the editor reloads
		/// </summary>
		[InitializeOnLoadMethod]
		internal static void Establish()
		{
			// Check and add the script
			Manager.AddDefine("ENABLE_DEFINE_MANAGER");
		}
	}
}

#endif
