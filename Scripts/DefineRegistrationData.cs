using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.DefineManager
{
	[System.Serializable]
	public class DefineRegistrationData : IComparable<DefineRegistrationData>
	{
		#region public settable values

		public string Define;
		public string DisplayName;
		public bool   EnableByDefault;
		public string Category;

		#endregion

		private bool? _isInstalled = null;
		/// <summary>
		/// Is the registered data installed?
		/// </summary>
		internal bool IsInstalled
		{
			get
			{
				// if the value is null, perform a check to see if the define is 
				_isInstalled ??= Manager.ContainDefine(Define);
				return _isInstalled.Value;
			}

			set { _isInstalled = value; }
		}

		/// <summary>
		/// Function used to compare two registration data
		/// </summary>
		public int CompareTo(DefineRegistrationData other)
		{
			if (Category == other.Category)
			{
				return DisplayName.CompareTo(other.DisplayName);
			}

			return Category.CompareTo(other.Category);
		}
	}
}
