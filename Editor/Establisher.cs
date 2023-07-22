#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.PackageManager;

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

			// Subscribe to the package manager event to monitor if the define
			// manager is requested to be removed, so that appropriate cleanup
			// can be performed
			Events.registeringPackages += HandlePackageRemoval;
		}

		/// <summary>
		/// Handles the removal of Define Manager and performs cleanup
		/// </summary>
		/// <param name="registrationInfo">The arguments passed by the package manager event</param>
		static void HandlePackageRemoval(PackageRegistrationEventArgs registrationInfo)
		{
			// look through packages that are queued to be removed
			foreach(var package in registrationInfo.removed)
			{
				// when the define manager is being removed by the user,
				// perform a cleanup and remove the ENABLE_DEFINE_MANAGER
				// define that indicates other packages and scripts that the
				// define manager is installed
				if(package.name == "com.hibzz.definemanager")
				{
					Manager.RemoveDefine("ENABLE_DEFINE_MANAGER");
					return;
				}
			}	
		}
	}
}

#endif
