/// <summary>
/// Tweenのインスペクター
/// 再生テストの機能
/// 
/// @author t-yoshino
/// @date 2020/06/28
/// @file Tween_Inspector.cs
/// </summary>
using UnityEngine;
using UnityEditor;

namespace UGUITween.Editor {

	/// <summary>
	/// Tweenのインスペクター
	/// 再生テストの機能
	/// </summary>
	[CustomEditor( typeof( TweenBase ), true ), CanEditMultipleObjects]
	public class Tween_Inspector : UnityEditor.Editor {

		//! 子と一緒に再生するか
		static bool _is_test_with_child = false;
		// 再生テストのグループ名フィルター
		static string _test_groupname = "";

		//! グループ名
		SerializedProperty _group_name;
		//! 再生タイプ
		SerializedProperty _type;
		//! 逆再生するか
		SerializedProperty _is_reverse;
		//! 動作カーブ
		SerializedProperty _curve;
		//! 再生待ちディレイ
		SerializedProperty _delay;
		//! 再生にかける時間
		SerializedProperty _duration;
		//! 終了イベント
		SerializedProperty _on_finished;
		//! タイムスケールを無視するか
		SerializedProperty _is_ignore_timescale;
		//! 物理フレームで動作するか
		SerializedProperty _is_fixedtime;

		//! 0にあたる値
		SerializedProperty _from;
		//! 1にあたる値
		SerializedProperty _to;
		//! 追加オプション
		SerializedProperty _option;

		/// <summary>
		/// インスペクタ有効化時にプロパティを取得
		/// </summary>
		protected virtual void OnEnable() {

			_group_name = serializedObject.FindProperty( "_group_name" );
			_type = serializedObject.FindProperty( "_type" );
			_is_reverse = serializedObject.FindProperty( "_is_reverse" );
			_curve = serializedObject.FindProperty( "_curve" );
			_delay = serializedObject.FindProperty( "_delay" );
			_duration = serializedObject.FindProperty( "_duration" );
			_on_finished = serializedObject.FindProperty( "_on_finished" );
			_is_ignore_timescale = serializedObject.FindProperty( "_is_ignore_timescale" );
			_is_fixedtime = serializedObject.FindProperty( "_is_fixedtime" );

			_from = serializedObject.FindProperty( "_from" );
			_to = serializedObject.FindProperty( "_to" );
			_option = serializedObject.FindProperty( "_option" );
		}

		/// <summary>
		/// インスペクター表示
		/// </summary>
		public override void OnInspectorGUI() {

			EditorGUI.BeginDisabledGroup( true );
			EditorGUILayout.PropertyField( serializedObject.FindProperty( "m_Script" ) );
			EditorGUI.EndDisabledGroup();

			serializedObject.Update();
			EditorGUILayout.BeginVertical( GUI.skin.box );
			{
				EditorGUIUtility.labelWidth = 80;

				EditorGUILayout.PropertyField( _group_name, new GUIContent( _group_name.displayName.Replace( "_", " " ) ) );

				DrawLine( Color.black );

				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField( _type );
					ToggleLeftPropertyField( _is_reverse, "逆再生するならtrue", GUILayout.MaxWidth( 100 ) );
					EditorGUILayout.EndHorizontal();
				}

				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField( "", GUILayout.Width( 80 ) );
					ToggleLeftPropertyField( _is_ignore_timescale, "タイムスケールを無視するならtrue" );
					ToggleLeftPropertyField( _is_fixedtime, "物理フレームで動作するならtrue" );
					EditorGUILayout.EndHorizontal();
				}

				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField( _delay );
					EditorGUILayout.PropertyField( _duration );
					EditorGUILayout.EndHorizontal();
				}

				DrawLine( Color.black );

				{
					if( _from != null ) {
						EditorGUILayout.PropertyField( _from );
					}
					if( _to != null ) {
						EditorGUILayout.PropertyField( _to );
					}
					if( _option != null ) {
						EditorGUILayout.PropertyField( _option );
					}
				}

				EditorGUILayout.PropertyField( _curve, GUILayout.Height( 50 ) );
				EditorGUILayout.PropertyField( _on_finished, new GUIContent( _on_finished.displayName.Replace( "_", " " ) ) );
			}
			EditorGUILayout.EndVertical();
			serializedObject.ApplyModifiedProperties();

			// 実行中のみテスト機能を有効化
			if( Application.isPlaying ) {
				DrawLine( Color.black );
				{
					GUILayout.BeginHorizontal();
					if( GUILayout.Button( "Replay", GUILayout.Width( 80 ) ) ) {
						TweenBase tween = target as TweenBase;
						if( _is_test_with_child ) {
							TweenBase[] tweens = tween.transform.GetTweens( _test_groupname );
							tweens.Play();
						} else {
							tween.Play( tween.isReverse );
						}
					}
					_is_test_with_child = GUILayout.Toggle( _is_test_with_child, "WithChild", GUILayout.Width( 100 ) );
					GUI.enabled = _is_test_with_child;
					_test_groupname = GUILayout.TextField( _test_groupname );
					GUI.enabled = true;
					GUILayout.EndHorizontal();
				}
			}
		}

		/// <summary>
		/// インスペクタ上に横ラインを引く
		/// </summary>
		/// <param name="lineColor">色の指定があれば設置する</param>
		void DrawLine( Color? lineColor = null ) {

			var originalColor = GUI.color;
			if( lineColor != null ) {
				GUI.color = (Color)lineColor;
			}
			GUILayout.Box( "", GUILayout.ExpandWidth( true ), GUILayout.Height( 2 ) );
			GUI.color = originalColor;
		}

		/// <summary>
		/// 左側にトグルがあるプロパティフィールド表示
		/// </summary>
		/// <param name="property">対象のプロパティ</param>
		/// <param name="tooltip">ツールチップ表示（privateプロパティはtooltipが取得できない）</param>
		/// <param name="options">表示オプション</param>
		void ToggleLeftPropertyField( SerializedProperty property, string tooltip, params GUILayoutOption[] options ) {

			property.boolValue = EditorGUILayout.ToggleLeft(
				new GUIContent( property.displayName.Replace( "_", " " ), tooltip ),
				property.boolValue,
				options
			);
		}
	}
}
