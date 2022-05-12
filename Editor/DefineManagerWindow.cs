using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Hibzz.DefineManager
{
	internal class DefineManagerWindow : EditorWindow
	{
		[MenuItem("Window/Define Manager")]
		private static void ShowEditor()
		{
			// start by refreshing the data
			Manager.RefreshList();

			// create the window and show it
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

		/// <summary>
		/// A reference to the define data that's currently selected
		/// </summary>
		DefineRegistrationData selectedData = null;

		/// <summary>
		/// Force request a repaint
		/// </summary>
		bool requestRepaint = false;

		void DrawDefineManager()
		{
			var settings = DefineManagerSettings.GetOrCreateSettings();

			GUILayout.BeginHorizontal();

			DrawLeftTab(settings);
			DrawRightTab();

			GUILayout.EndHorizontal();

			HandleRepaint();
		}

		
		private Vector2 scrollpos;
		private float DefineListPaneWidth = 300.0f;
		private void DrawLeftTab(DefineManagerSettings settings)
		{
			scrollpos = GUILayout.BeginScrollView(scrollpos, EditorStyleUtility.LeftTabStyle,
							GUILayout.Height(position.height), GUILayout.Width(DefineListPaneWidth));

			foreach (var defineData in settings.DefineRegistery)
			{
				// Style based GUILayout.BeginHorizontal()
				if (defineData == selectedData)
				{
					GUILayout.BeginHorizontal(EditorStyleUtility.LeftTabSelectedStyle);
				}
				else
                {
					GUILayout.BeginHorizontal();
				}
				
				GUILayout.Label($"{defineData.DisplayName}");
				if(defineData.IsInstalled)
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label(EditorGUIUtility.IconContent("FilterSelectedOnly"));
				}
				GUILayout.EndHorizontal();

				// don't need to check for click if already selected
				if(selectedData == defineData) { continue; }

				if(Event.current.type == EventType.MouseDown && 
					GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
				{
					selectedData = defineData;
					requestRepaint = true;
				}
			}

			AddRefreshButton();
			GUILayout.EndScrollView();
		}

		private void DrawRightTab()
		{
			// don't do anything if none of the elements are selected
			if(selectedData == null) { return; }

			GUILayout.BeginVertical(EditorStyleUtility.RightTabStyle);
			GUILayout.Label($"{selectedData.DisplayName}");
			GUILayout.Label($"Preprocess Directive: {selectedData.Define}");
			AddInstallButton(selectedData);
			GUILayout.EndVertical();
		}

		void AddRefreshButton()
		{
			// Configure the button that can be clicked to refresh the define list
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Refresh", GUILayout.Width(DefineListPaneWidth - 24), GUILayout.Height(30)))
			{
				// after the refresh, the most valid data exists in the DefineRegistery
				Manager.RefreshList();
			}
		}

		private float installButtonSize = 75.0f;
		void AddInstallButton(DefineRegistrationData registrationData)
		{
			// Configure the button that can be clicked to refresh the define list
			GUILayout.FlexibleSpace();

			// Aligns button to the right
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			// NOTE: In the future can be improved so that this check is only
			// done once when we switch defines in the left tab
			string buttonText = registrationData.IsInstalled ? "Remove" : "Install";

			if (GUILayout.Button(buttonText, GUILayout.Width(installButtonSize)))
			{
				// Add/Remove the requested define
				if (registrationData.IsInstalled) 
				{ 
					Manager.RemoveDefine(registrationData.Define);
					registrationData.IsInstalled = false;
				}
				else 
				{ 
					Manager.AddDefine(registrationData.Define);
					registrationData.IsInstalled = true;
				}
			}

			GUILayout.EndHorizontal();
		}

		void HandleRepaint()
        {
			if(requestRepaint)
            {
				Repaint();
            }
        }
		#endif
	}
}
