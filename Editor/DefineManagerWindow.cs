using UnityEngine;
using UnityEditor;

namespace Hibzz.DefineManager
{
    internal class DefineManagerWindow : EditorWindow
    {
        [MenuItem("Window/Define Manager")]
        private static void ShowEditor()
		{
            var window = GetWindow<DefineManagerWindow>();
            window.titleContent = new GUIContent("Define Manager");
		}

		private void OnGUI()
		{
			#if ENABLE_DEFINE_MANAGER
			DrawDefineManager();
			#else
			DrawUnitialized();
			#endif
		}

		#if !ENABLE_DEFINE_MANAGER
		private readonly Vector2 InitializeButtonSize = new Vector2(200.0f, 50.0f);
		
		private void DrawUnitialized()
		{
			// Configuring the rect for the button
			Rect rect = new Rect(
				(position.width - InitializeButtonSize.x) / 2.0f,
				(position.height - InitializeButtonSize.y) / 2.0f,
				InitializeButtonSize.x, 
				InitializeButtonSize.y);

			// Create the button
			if(GUI.Button(rect, "Initialize Define Manager"))
			{
				Establisher.Establish();
			}
		}
		#endif

		#if ENABLE_DEFINE_MANAGER
		void DrawDefineManager()
		{
			GUILayout.Label("Define Manager has been initialized");
		}
		#endif
	}
}
