/// <summary>
/// 回転のTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenRotation.cs
/// </summary>
using UnityEngine;

namespace UGUITween {

	/// <summary>
	/// 回転のTween
	/// </summary>
	public class TweenRotation : TweenBase {

		//! ターゲットにするRectTransform
		RectTransform _rect_transform = null;
		//! ターゲットにするTransform
		Transform _transform = null;

		//! 最初の位置
		[SerializeField]
		Vector3 _from = new Vector3();
		//! 最後の位置
		[SerializeField]
		Vector3 _to = new Vector3();
		//! 条件フラグ
		[SerializeField]
		eVectorConstraints _option = 0;

		/// <summary>
		/// 初期化時に初期パラメータをセット
		/// </summary>
		private void Reset() {
			RectTransform rect = GetComponent<RectTransform>();
			if( rect ) {
				_from = rect.localEulerAngles;
				_to = _from;
				return;
			}

			_from = transform.localEulerAngles;
			_to = _from;
		}

		/// <summary>
		/// 値の更新
		/// </summary>
		/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
		protected override void _UpdateValue( float v ) {

			if( !_rect_transform && !_transform ) {
				_rect_transform = GetComponent<RectTransform>();
				_transform = GetComponent<Transform>();
			}

			Vector3 value;
			if( _rect_transform ) {
				value = _rect_transform.localEulerAngles;
			} else if( _transform ) {
				value = _transform.localEulerAngles;
			} else {
				enabled = false;
				return;
			}

			if( !_option.HasFlag( eVectorConstraints.Ignore_X ) ) {
				value.x = _from.x * ( 1.0f - v ) + _to.x * v;
			}
			if( !_option.HasFlag( eVectorConstraints.Ignore_Y ) ) {
				value.y = _from.y * ( 1.0f - v ) + _to.y * v;
			}
			if( !_option.HasFlag( eVectorConstraints.Ignore_Z ) ) {
				value.z = _from.z * ( 1.0f - v ) + _to.z * v;
			}

			if( _rect_transform ) {
				_rect_transform.localEulerAngles = value;
			} else if( _transform ) {
				_transform.localEulerAngles = value;
			}
		}
	}
}
