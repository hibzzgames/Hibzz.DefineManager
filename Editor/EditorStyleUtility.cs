using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Hibzz.DefineManager
{
    internal static class EditorStyleUtility
    {
        #region ReadOnly Colors

        private static readonly Color leftTabBgColor = new Color(0.2f, 0.2f, 0.2f);
        private static readonly Color rightTabBgColor = new Color(0.2196f, 0.2196f, 0.2196f);
        private static readonly Color selectedBgColor = new Color(0.1725f, 0.3647f, 0.5294f);


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
	}
}
