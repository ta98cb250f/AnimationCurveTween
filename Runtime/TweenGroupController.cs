/// <summary>
/// Tweenのグループ処理
///
/// @author t-yoshino
/// @date 2020/06/28
/// @file TweenGroupController.cs
/// </summary>
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UGUITween {

	/// <summary>
	/// 指定したグループ名のTweenをまとめて処理する
	/// </summary>
	public class TweenGroupController : MonoBehaviour {

		//! 自身のTransform
		[SerializeField]
		Transform _transform = null;
		//! 有効化されたときに処理するイベント
		[SerializeField]
		UnityEvent _on_enabled_event = null;

		/// <summary>
		/// 有効化されたときに処理するイベント
		/// </summary>
		public UnityEvent oEnabledEvent => _on_enabled_event;

		//! グループとして管理するTween配列
		TweenBase[] _tweens = null;

		/// <summary>
		/// 自身のTransformを事前に取得しておく
		/// </summary>
		private void Reset() {
			_transform = transform;

#if UNITY_EDITOR
			// Tweenより上に配置する
			Component[] components = GetComponents<Component>();
			for( int current_index = components.Length - 1; current_index > 0; --current_index ) {
				Component current_component = components[current_index];
				if( current_component != this ) {
					continue;
				}

				for( int up_index = current_index - 1; up_index >= 0; --up_index ) {
					Component up_component = components[up_index];
					TweenBase tween = up_component as TweenBase;
					if( tween ) {
						for( ; up_index < current_index; --current_index ) {
							UnityEditorInternal.ComponentUtility.MoveComponentUp( this );
						}
					}
				}
				break;
			}
#endif
		}

		/// <summary>
		/// 有効化された時に処理するイベントがあれば実行
		/// </summary>
		private void OnEnable() {
			_on_enabled_event?.Invoke();
		}

		/// <summary>
		/// Tween配列の初期化
		/// </summary>
		/// <param name="force_reset">強制的に実施するならtrue</param>
		public void ResetTweensArray( bool force_reset = false ) {
			if( _tweens == null || force_reset ) {
				_tweens = _transform.GetTweens( include_inactive: true );
			}
		}

		/// <summary>
		/// 指定したグループのTweenを再生する
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void Play( string group = "" ) {
			ResetTweensArray();
			_tweens.Play( group: group );
		}
		/// <summary>
		/// 指定したグループのTweenを逆再生する
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void PlayReverse( string group = "" ) {
			ResetTweensArray();
			_tweens.Play( group: group, reverse: true );
		}

		/// <summary>
		/// 指定したグループのTweenをリセットする
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void Reset( string group = "" ) {
			ResetTweensArray();
			_tweens.Reset( group: group, pause: true );
		}
		/// <summary>
		/// 指定したグループのTweenを逆再生基準でリセットする
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void ResetReverse( string group = "" ) {
			ResetTweensArray();
			_tweens.Reset( group: group, reverse: true, pause: true );
		}

		/// <summary>
		/// 指定したグループのTweenを停止する
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void Pause( string group = "" ) {
			ResetTweensArray();
			_tweens.Pause( group: group );
		}

		/// <summary>
		/// 指定したグループのTweenを途中から再生し直す
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void Resume( string group = "" ) {
			ResetTweensArray();
			_tweens.Resume( group: group );
		}
		/// <summary>
		/// 指定したグループのTweenを途中から逆再生し直す
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		public void ResumeReverse( string group = "" ) {
			ResetTweensArray();
			_tweens.Resume( group: group, reverse: true );
		}

		/// <summary>
		/// 指定したグループのTweenが再生されているか確認する
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		/// <returns>再生中ならtrueを返す</returns>
		public bool IsPlaying( string group = "" ) {
			ResetTweensArray();
			return _tweens.IsPlaying();
		}

		/// <summary>
		/// 指定したグループのTweenを再生する
		/// </summary>
		/// <param name="group">グループ名の指定</param>
		/// <param name="reverse">逆生成するならtrue</param>
		public IEnumerator PlayWhile( string group, bool reverse = false ) {
			ResetTweensArray();
			yield return _tweens.PlayWhile( group: group, reverse: reverse );
		}
	}
}