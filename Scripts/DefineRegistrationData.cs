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
		public string Category;
		public string Description;
		public bool   EnableByDefault;

		#endregion

		[SerializeField] internal bool Initialized = false;

		#if UNITY_EDITOR
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
		#endif

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

		#if UNITY_EDITOR
		/// <summary>
		/// Initialize the data
		/// </summary>
		internal void Initialize()
        {
			// if already initialize, skip the process
			if(Initialized) { return; }

			// If asked to enable by default, the define gets automatically added
			if(EnableByDefault) 
			{
				Manager.AddDefine(Define);
			}

			// mark the data as initialized
			Initialized = true;
        }
		#endif
	}
}
