/// <summary>
/// SharedMaterialのTweenのインスペクター
/// 
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenSharedMaterial_Inspector.cs
/// </summary>
using UnityEngine;
using UnityEditor;

namespace UGUITween.Editor {

	/// <summary>
	/// SharedMaterialのTweenのインスペクター
	/// </summary>
	[CustomEditor( typeof( TweenSharedMaterialBase ), true ), CanEditMultipleObjects]
	public class TweenSharedMaterial_Inspector : Tween_Inspector {

		//! マテリアル
		SerializedProperty _material;
		//! プロパティ名
		SerializedProperty _property_name;

		/// <summary>
		/// インスペクタ有効化時にプロパティを取得
		/// </summary>
		protected override void OnEnable() {

			_material = serializedObject.FindProperty( "_material" );
			_property_name = serializedObject.FindProperty( "_property_name" );

			base.OnEnable();
		}

		/// <summary>
		/// インスペクター表示
		/// </summary>
		public override void OnInspectorGUI() {

			serializedObject.Update();
			{
				EditorGUILayout.PropertyField( _material );
				EditorGUILayout.PropertyField( _property_name );
			}
			serializedObject.ApplyModifiedProperties();

			GUILayout.Box( "", GUILayout.ExpandWidth( true ), GUILayout.Height( 2 ) );

			base.OnInspectorGUI();
		}
	}
}
