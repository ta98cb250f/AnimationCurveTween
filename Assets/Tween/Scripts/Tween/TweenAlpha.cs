/// <summary>
/// カラーアルファのTween
///
/// @author t-yoshino
/// @date 2020/06/28
/// @file TweenAlpha.cs
/// </summary>
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カラーアルファのTween
/// </summary>
public class TweenAlpha : TweenBase {

	//! ターゲットにするCanvasGroup
	CanvasGroup _canvas_group = null;
	//! ターゲットにするImage
	Image _image = null;
	//! ターゲットにするText
	Text _text = null;
	//! ターゲットにするTMP
	TMPro.TextMeshProUGUI _tmp = null;
	//! ターゲットにするSpriteRenderer
	SpriteRenderer _sprite_renderer = null;

	//! 最初の位置
	[SerializeField]
	float _from = 0.0f;
	//! 最後の位置
	[SerializeField]
	float _to = 1.0f;

	/// <summary>
	/// 初期化時に初期パラメータをセット
	/// </summary>
	private void Reset() {
		CanvasGroup canvs_group = GetComponent<CanvasGroup>();
		if( canvs_group ) {
			_from = canvs_group.alpha;
			_to = _from;
			return;
		}

		Image image = GetComponent<Image>();
		if( image ) {
			_from = image.color.a;
			_to = _from;
			return;
		}

		Text text = GetComponent<Text>();
		if( text ) {
			_from = text.color.a;
			_to = _from;
			return;
		}

		TMPro.TextMeshProUGUI tmp = GetComponent<TMPro.TextMeshProUGUI>();
		if( tmp ) {
			_from = tmp.color.a;
			_to = _from;
			return;
		}
		SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
		if( sprite_renderer ) {
			_from = sprite_renderer.color.a;
			_to = _from;
			return;
		}
	}

	/// <summary>
	/// 値の更新
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected override void _UpdateValue( float v ) {

		if( !_canvas_group && !_image && !_text && !_tmp && !_sprite_renderer ) {
			_canvas_group = GetComponent<CanvasGroup>();
			_image = GetComponent<Image>();
			_text = GetComponent<Text>();
			_tmp = GetComponent<TMPro.TextMeshProUGUI>();
			_sprite_renderer = GetComponent<SpriteRenderer>();
		}

		float value = _from * ( 1.0f - v ) + _to * v;
		if( _canvas_group ) {
			_canvas_group.alpha = value;
		} else if( _image ) {
			Color dest_color = _image.color;
			dest_color.a = value;
			_image.color = dest_color;
		} else if( _text ) {
			Color dest_color = _text.color;
			dest_color.a = value;
			_text.color = dest_color;
		} else if( _tmp ) {
			Color dest_color = _tmp.color;
			dest_color.a = value;
			_tmp.color = dest_color;
		} else if( _sprite_renderer ) {
			Color dest_color = _sprite_renderer.color;
			dest_color.a = value;
			_sprite_renderer.color = dest_color;
		} else {
			enabled = false;
		}
	}

}
