using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Core.EasyInput
{
	[CreateAssetMenu(fileName = "InputAction_", menuName = "Bitwise/Input/Input Action")]
	public class InputAction : ScriptableObject, IEquatable<InputAction>
	{
		[SerializeField] private string _displayName = "Input Action";
		[SerializeField] public int _hashCode;

		// ----------------------------------------------------------------------------

		public string DisplayName => _displayName;
		public int HashCode => _hashCode;

		// ----------------------------------------------------------------------------

		private void OnValidate()
		{
			_hashCode = GetHashCode();
		}

		// ----------------------------------------------------------------------------

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		// ----------------------------------------------------------------------------

		public bool Equals(InputAction other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return HashCode == other.HashCode;
		}

		// ----------------------------------------------------------------------------

#if UNITY_EDITOR

		[CustomEditor(typeof(InputAction))]
		public class InputActionEditor : Editor
		{
			private SerializedProperty _displayName;
			private SerializedProperty _hashCode;

			// ------------------------------------------------------------------------

			private void OnEnable()
			{
				_displayName = serializedObject.FindProperty("_displayName");
				_hashCode = serializedObject.FindProperty("_hashCode");
			}

			public override void OnInspectorGUI()
			{
				serializedObject.Update();

				EditorGUILayout.PropertyField(_displayName);

				EditorGUI.BeginDisabledGroup(true);
				{
					EditorGUILayout.PropertyField(_hashCode);
					EditorGUI.EndDisabledGroup();
				}

				serializedObject.ApplyModifiedProperties();
			}
		}

#endif // UNITY_EDITOR
	}
}