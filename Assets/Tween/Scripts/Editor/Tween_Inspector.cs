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

/// <summary>
/// Tweenのインスペクター
/// 再生テストの機能
/// </summary>
[CustomEditor( typeof( TweenBase ), true )]
public class Tween_Inspector : Editor {

	//! 子と一緒に再生するか
	static bool _with_child = false;
	// 再生テストのグループ名フィルター
	static string _group = "";

	/// <summary>
	/// インスペクター表示
	/// </summary>
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		// 実行中のみ
		if( Application.isPlaying ) {
			GUILayout.BeginHorizontal();
				if( GUILayout.Button( "Replay", GUILayout.Width( 50 ) ) ) {
					TweenBase tween = target as TweenBase;
					if( _with_child ) {
						TweenBase[] tweens = tween.transform.GetTweens( _group );
						tweens.Play();
					} else {
						tween.Play( tween.isReverse );
					}
				}
				_with_child = GUILayout.Toggle( _with_child, "WithChild", GUILayout.Width( 100 ) );
				GUI.enabled = _with_child;
				_group = GUILayout.TextField( _group );
				GUI.enabled = true;
			GUILayout.EndHorizontal();
		}
	}
}
