#if UNITY_EDITOR

using UnityEngine;

namespace Hibzz.DefineManager
{
	internal static class EditorStyleUtility
	{
		#region ReadOnly Colors

		private static readonly Color leftTabBgColor = new Color(0.2f, 0.2f, 0.2f);
		private static readonly Color rightTabBgColor = new Color(0.2196f, 0.2196f, 0.2196f);
		private static readonly Color selectedBgColor = new Color(0.1725f, 0.3647f, 0.5294f);
		private static readonly Color CategoryBgColor = new Color(0.1568f, 0.1568f, 0.1568f);


		#endregion

		private static GUIStyle _leftTabStyle;
		internal static GUIStyle LeftTabStyle
		{
			get
			{
				if(_leftTabStyle == null)
				{
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, leftTabBgColor);
					tex.Apply();

					_leftTabStyle = new GUIStyle();
					_leftTabStyle.normal.background = tex;

					_leftTabStyle.padding = new RectOffset(5, 5, 10, 10);
				}

				return _leftTabStyle;
			}
		}

		private static GUIStyle _rightTabStyle;
		internal static GUIStyle RightTabStyle
		{
			get
			{
				if(_rightTabStyle == null)
				{
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, rightTabBgColor);
					tex.Apply();

					_rightTabStyle = new GUIStyle();
					_rightTabStyle.normal.background = tex;

					_rightTabStyle.padding = new RectOffset(10, 10, 10, 10);
				}

				return _rightTabStyle;
			}
		}

		private static GUIStyle _leftTabSelectedStyle;
		internal static GUIStyle LeftTabSelectedStyle
		{
			get
			{
				if(_leftTabSelectedStyle == null)
				{
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, selectedBgColor);
					tex.Apply();

					_leftTabSelectedStyle = new GUIStyle();
					_leftTabSelectedStyle.normal.background = tex;
				}

				return _leftTabSelectedStyle;
			}
		}

		private static GUIStyle _CategoryElementStyle;
		internal static GUIStyle CategoryElementStyle
		{
			get 
			{
				if(_CategoryElementStyle == null)
				{
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, CategoryBgColor);
					tex.Apply();

					// set background color
					_CategoryElementStyle = new GUIStyle();
					_CategoryElementStyle.normal.background = tex;
				}

				return _CategoryElementStyle;
			}
		}

		private static GUIStyle _CategoryFontStyle;
		internal static GUIStyle CategoryFontStyle
		{
			get
			{
				if(_CategoryFontStyle == null)
				{
					_CategoryFontStyle = new GUIStyle(GUI.skin.label); ;
					_CategoryFontStyle.fontSize = 14;
				}

				return _CategoryFontStyle;
			}
		}

		private static GUIStyle _TitleStyle;
		internal static GUIStyle TitleStyle
		{
			get
			{
				if(_TitleStyle == null)
				{
					_TitleStyle = new GUIStyle(GUI.skin.label);
					_TitleStyle.fontSize = 24;
					_TitleStyle.fontStyle = FontStyle.Bold;
				}

				return _TitleStyle;
			}
		}
	}
}

#endif
