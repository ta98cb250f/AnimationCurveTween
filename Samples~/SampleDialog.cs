/// <summary>
/// Tweenサンプルダイアログ
///
/// @author t-yoshino
/// @date 2020/07/15
/// @file SampleDialog.cs
/// </summary>
using System.Collections;
using UnityEngine;
using UGUITween;

/// <summary>
/// Tweenサンプルダイアログ
/// </summary>
public class SampleDialog : MonoBehaviour
{
	//! 自身のTransform
	[SerializeField]
	Transform _transform = null;

	//! 入力禁止マスクオブジェクト
	[SerializeField]
	GameObject _input_block_object = null;

	//! Tween配列（事前取得しておく）
	TweenBase[] _tweens = null;

	/// <summary>
	/// サンプル動作のための開始処理
	/// </summary>
	private void Start() {

		Initialize();

		Open();
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize() {
		_tweens = _transform.GetTweens();
		_tweens.Reset( pause: true );
		gameObject.SetActive( false );
	}

	/// <summary>
	/// 開く
	/// </summary>
	public void Open() {
		gameObject.SetActive( true );
		StartCoroutine( _Open() );
	}
	/// <summary>
	/// 開く
	/// </summary>
	IEnumerator _Open() {
		_input_block_object.SetActive( true );

		_tweens.Reset( "after_open", pause: true );
		yield return _tweens.PlayWhile( "open" );
		_tweens.Play( "after_open" );

		_input_block_object.SetActive( false );
	}

	/// <summary>
	/// 閉じる
	/// </summary>
	public void Close() {
		StartCoroutine( _Close() );
	}
	/// <summary>
	/// 閉じる（サンプルとしてその後開く）
	/// </summary>
	/// <returns></returns>
	IEnumerator _Close() {
		_input_block_object.SetActive( true );
		yield return _tweens.PlayWhile( "close" );
		_input_block_object.SetActive( true );

		Open();
	}
}
