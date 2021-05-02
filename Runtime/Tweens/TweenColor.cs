/// <summary>
/// カラーのTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenColor.cs
/// </summary>
using UnityEngine;
using UnityEngine.UI;

namespace UGUITween {

	/// <summary>
	/// カラーのTween
	/// </summary>
	public class TweenColor : TweenBase {

		//! ターゲットにするImage
		Image _image = null;
		//! ターゲットにするText
		Text _text = null;
		//! ターゲットにするTMP
		TMPro.TextMeshProUGUI _tmp = null;
		//! ターゲットにするSpriteRenderer
		SpriteRenderer _sprite_renderer = null;
		//! 上記以外のRenderer
		Renderer _renderer = null;

		//! 最初の色
		[SerializeField]
		Color _from = new Color();
		//! 最後の色
		[SerializeField]
		Color _to = new Color();
		//! 条件フラグ
		[SerializeField]
		eColorConstraints _option = 0;

		/// <summary>
		/// 初期化時に初期パラメータをセット
		/// </summary>
		private void Reset() {

			Image image = GetComponent<Image>();
			if( image ) {
				_from = image.color;
				_to = _from;
				return;
			}

			Text text = GetComponent<Text>();
			if( text ) {
				_from = text.color;
				_to = _from;
				return;
			}

			TMPro.TextMeshProUGUI tmp = GetComponent<TMPro.TextMeshProUGUI>();
			if( tmp ) {
				_from = tmp.color;
				_to = _from;
				return;
			}

			SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
			if( sprite_renderer ) {
				_from = sprite_renderer.color;
				_to = _from;
				return;
			}

			Renderer renderer = GetComponent<Renderer>();
			if( _renderer ) {
				_from = _renderer.material.color;
				_to = _from;
			}
		}

		/// <summary>
		/// 値の更新
		/// </summary>
		/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
		protected override void _UpdateValue( float v ) {

			if( !_image && !_text && !_tmp && !_sprite_renderer && !_renderer ) {
				_image = GetComponent<Image>();
				_text = GetComponent<Text>();
				_tmp = GetComponent<TMPro.TextMeshProUGUI>();
				_sprite_renderer = GetComponent<SpriteRenderer>();
				_renderer = GetComponent<Renderer>();
			}

			Color value;
			if( _image ) {
				value = _image.color;
			} else if( _text ) {
				value = _text.color;
			} else if( _tmp ) {
				value = _tmp.color;
			} else if( _sprite_renderer ) {
				value = _sprite_renderer.color;
			} else if( _renderer ) {
				value = _renderer.material.color;
			} else {
				enabled = false;
				return;
			}

			if( !_option.HasFlag( eColorConstraints.Ignore_R ) ) {
				value.r = _from.r * ( 1.0f - v ) + _to.r * v;
			}
			if( !_option.HasFlag( eColorConstraints.Ignore_G ) ) {
				value.g = _from.g * ( 1.0f - v ) + _to.g * v;
			}
			if( !_option.HasFlag( eColorConstraints.Ignore_B ) ) {
				value.b = _from.b * ( 1.0f - v ) + _to.b * v;
			}
			if( !_option.HasFlag( eColorConstraints.Ignore_A ) ) {
				value.a = _from.a * ( 1.0f - v ) + _to.a * v;
			}

			if( _image ) {
				_image.color = value;
			} else if( _text ) {
				_text.color = value;
			} else if( _tmp ) {
				_tmp.color = value;
			} else if( _sprite_renderer ) {
				_sprite_renderer.color = value;
			} else if( _renderer ) {
				_renderer.material.color = value;
			}
		}

	}
}
