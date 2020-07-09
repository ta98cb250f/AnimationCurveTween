/// <summary>
/// タイムスケールのTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenTimescale.cs
/// </summary>
using UnityEngine;

/// <summary>
/// タイムスケールのTween
/// </summary>
public class TweenTimescale : TweenBase {

	//! 0のタイムスケール
	[SerializeField]
	float _from = 1.0f;
	//! 1のタイムスケール
	[SerializeField]
	float _to = 1.0f;

	/// <summary>
	/// 初期化時に初期パラメータをセット
	/// </summary>
	private void Reset() {
		_is_ignore_timescale = true;
	}

	/// <summary>
	/// 値の更新
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected override void _UpdateValue( float v ) {

		Time.timeScale = _from * ( 1.0f - v ) + _to * v;
	}

}
