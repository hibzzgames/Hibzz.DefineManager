using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
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
            {
				// variable used to track the last drawn category...
				// This works because the system passes in define data sorted
				// by category
				string lastCategory = string.Empty;
				foreach (var defineData in settings.DefineRegistery)
				{
					// Get is the data collapsed
					bool isCollapsed = settings.CollapseInfo.IsCollapsed(defineData.Category);

					if (defineData.Category != lastCategory)
					{
						// Draw Category
						DrawCategoryElement(defineData, isCollapsed);
						lastCategory = defineData.Category;
					}

					// don't draw the define elements if the category is collapsed
					if(isCollapsed) { continue; }

					DrawDefineElement(defineData);
				}

				AddRefreshButton();
			}
			GUILayout.EndScrollView();
		}

		// draw category element on the left
		private void DrawCategoryElement(DefineRegistrationData data, bool isCollapsed)
		{
			GUILayout.BeginHorizontal(EditorStyleUtility.CategoryElementStyle);
			{
				GUIContent foldoutIcon = isCollapsed ?
					EditorGUIUtility.IconContent("IN foldout") :
					EditorGUIUtility.IconContent("IN foldout on");

				GUILayout.Label(foldoutIcon, GUILayout.Width(18), GUILayout.Height(18));
				GUILayout.Label(data.Category, EditorStyleUtility.CategoryFontStyle);
			}
			GUILayout.EndHorizontal();

			// Check for a mouse click
			if (Event.current.type == EventType.MouseDown &&
				GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
			{
				// toggle the collapsed data info for that category
				var settings = DefineManagerSettings.GetOrCreateSettings();
				settings.CollapseInfo.Toggle(data.Category);

				requestRepaint = true;	
			}
		}

		// draw define element
		private void DrawDefineElement(DefineRegistrationData defineData)
		{
			GUIStyle style = CheckSelectedAndGetStyle(defineData);
			GUILayout.BeginHorizontal(style);
			{
				GUILayout.Label($"{defineData.DisplayName}");
				if (defineData.IsInstalled)
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label(EditorGUIUtility.IconContent("FilterSelectedOnly"));
				}
			}
			GUILayout.EndHorizontal();

			// don't need to check for click if already selected
			if (selectedData == defineData) { return; }

			// Check for a mouse click
			if (Event.current.type == EventType.MouseDown &&
				GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
			{
				selectedData = defineData;
				requestRepaint = true;
			}
		}

		// Check if the passed in data is the selected one, and pass the appropriate data
		private GUIStyle CheckSelectedAndGetStyle(DefineRegistrationData defineData)
		{
			if (defineData == selectedData) { return EditorStyleUtility.LeftTabSelectedStyle; }
			return GUIStyle.none;
		}

		// Draw the right pane
		private void DrawRightTab()
		{
			// don't do anything if none of the elements are selected
			if(selectedData == null) { return; }

			GUILayout.BeginVertical(EditorStyleUtility.RightTabStyle);
            {
				// Title + Define data
				GUILayout.Label($"{selectedData.DisplayName}", EditorStyleUtility.TitleStyle);
				GUILayout.Label($"Scripting Define: {selectedData.Define}");
				
				// Seperator line
				GUILayout.Space(10);
				GUILayout.Box("", GUILayout.Width(position.width - DefineListPaneWidth), GUILayout.Height(2));
				GUILayout.Space(10);
				
				// Description
				GUILayout.Label($"{selectedData.Description}", EditorStyles.wordWrappedLabel, 
					GUILayout.Width(position.width - DefineListPaneWidth - 20));
				
				// Install button
				AddInstallButton(selectedData);
			}
			GUILayout.EndVertical();
		}

		// Add a refresh button
		void AddRefreshButton()
		{
			// Configure the button that can be clicked to refresh the define list
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Refresh", GUILayout.Width(DefineListPaneWidth - 16), GUILayout.Height(30)))
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

            {
				// pick the text based on if the data is installed or not
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

				// padding space
				GUILayout.Space(30);
			}

			GUILayout.EndHorizontal();
		}

		// Check and handle repaint request
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
#endif
