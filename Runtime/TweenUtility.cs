/// <summary>
/// Tweenのユーティリティ関数
///
/// @author t-yoshino
/// @date 2020/06/28
/// @file TweenUtility.cs
/// </summary>
using System.Collections;
using UnityEngine;
#if UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace UGUITween {

	/// <summary>
	/// Tweenのユーティリティ関数
	/// </summary>
	public static class TweenUtility {

		/// <summary>
		/// Tweenを取得する
		/// </summary>
		/// <param name="transform">Tweenを探索する親Transform</param>
		/// <param name="group">グループ名</param>
		/// <param name="include_inactive">非アクティブObjectを探索対象から外すならfalse</param>
		/// <returns></returns>
		public static TweenBase[] GetTweens( this Transform transform, string group = "", bool include_inactive = true ) {

			TweenBase[] tweens = transform.GetComponentsInChildren<TweenBase>( include_inactive );

			if( !string.IsNullOrEmpty( group ) ) {
				tweens = System.Array.FindAll( tweens, t => t.groupName == group );
			}
			return tweens;
		}

		/// <summary>
		/// 再生
		/// グループ名の指定、逆再生を指定する
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		/// <param name="reverse">逆再生するか</param>
		public static void Play( this TweenBase[] tweens, string group = "", bool reverse = false ) {

			if( string.IsNullOrEmpty( group ) ) {
				foreach( var t in tweens ) {
					t.Play( reverse );
				}
			} else {
				foreach( var t in tweens ) {
					if( t.groupName == group ) {
						t.Play( reverse );
					}
				}
			}
		}

		/// <summary>
		/// リセット
		/// グループ名の指定、逆再生を指定する
		/// 同時にポーズできる
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		/// <param name="reverse">逆再生するか</param>
		/// <param name="pause">ポーズするか</param>
		public static void Reset( this TweenBase[] tweens, string group = "", bool reverse = false, bool pause = false ) {

			if( string.IsNullOrEmpty( group ) ) {
				foreach( var t in tweens ) {
					t.Reset( reverse, pause );
				}
			} else {
				foreach( var t in tweens ) {
					if( t.groupName == group ) {
						t.Reset( reverse, pause );
					}
				}
			}
		}
		/// <summary>
		/// リセット
		/// グループ名を指定できる
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		public static void Pause( this TweenBase[] tweens, string group = "" ) {

			if( string.IsNullOrEmpty( group ) ) {
				foreach( var t in tweens ) {
					t.Pause();
				}
			} else {
				foreach( var t in tweens ) {
					if( t.groupName == group ) {
						t.Pause();
					}
				}
			}
		}

		/// <summary>
		/// 再生し直し
		/// グループ名の指定、逆再生を指定する
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		/// <param name="reverse">逆再生するか</param>
		public static void Resume( this TweenBase[] tweens, string group = "", bool reverse = false ) {

			if( string.IsNullOrEmpty( group ) ) {
				foreach( var t in tweens ) {
					t.Resume( reverse );
				}
			} else {
				foreach( var t in tweens ) {
					if( t.groupName == group ) {
						t.Resume( reverse );
					}
				}
			}
		}

		/// <summary>
		/// 再生中かどうかを判定する
		/// グループ名の指定、逆再生を指定する
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		/// <returns>指定のうち、どれか一つが再生中ならtrue</returns>
		public static bool IsPlaying( this TweenBase[] tweens, string group = "" ) {

			if( string.IsNullOrEmpty( group ) ) {
				foreach( var t in tweens ) {
					if( t.enabled ) {
						return true;
					}
				}
			} else {
				foreach( var t in tweens ) {
					if( t.groupName == group ) {
						if( t.enabled ) {
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 再生して再生中を待つ
		/// グループ名の指定、逆再生を指定する
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		/// <param name="reverse">逆再生するか</param>
		public static IEnumerator PlayWhile( this TweenBase[] tweens, string group = "", bool reverse = false ) {

			TweenBase[] targets = tweens;
			if( !string.IsNullOrEmpty( group ) ) {
				targets = System.Array.FindAll( targets, t => t.groupName == group );
			}

			foreach( var t in targets ) {
				t.Play( reverse );
			}
			while( true ) {
				bool hit = false;
				foreach( var t in targets ) {
					if( t.enabled ) {
						hit = true;
						break;
					}
				}
				if( !hit ) {
					break;
				}
				yield return null;
			}
		}

#if UNITASK
		/// <summary>
		/// 再生して再生中を待つ
		/// グループ名の指定、逆再生を指定する
		/// UniTaskで使用する場合、asmdefにUniTaskを追加して使用する
		/// </summary>
		/// <param name="tweens">対象のTween配列</param>
		/// <param name="group">グループ名</param>
		/// <param name="reverse">逆再生するか</param>
		public static async UniTask PlayWhileAsync( this TweenBase[] tweens, string group = "", bool reverse = false ) {

			TweenBase[] targets = tweens;
			if( !string.IsNullOrEmpty( group ) ) {
				targets = System.Array.FindAll( targets, t => t.groupName == group );
			}

			foreach( var t in targets ) {
				t.Play( reverse );
			}
			while( true ) {
				bool hit = false;
				foreach( var t in targets ) {
					if( t.enabled ) {
						hit = true;
						break;
					}
				}
				if( !hit ) {
					break;
				}
				await UniTask.Yield();
			}
		}
#endif
	}
}