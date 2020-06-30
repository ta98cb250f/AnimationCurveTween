/// <summary>
/// アニメーションカーブでのアクション処理
///
/// @author t-yoshino
/// @date 2020/06/28
/// @file TweenBase.cs
/// </summary>
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// アニメーションカーブでのアクション処理
/// </summary>
public abstract class TweenBase : MonoBehaviour {

	/// <summary>
	/// 再生タイプ
	/// </summary>
	public enum eType {
		PlayOnce,
		Loop,
		PingPong,
	}

	/// <summary>
	/// グループ名
	/// </summary>
	[SerializeField]
	string _group_name = "";
	/// <summary>
	/// グループ名
	/// </summary>
	public string groupName => _group_name;

	/// <summary>
	/// 再生タイプ
	/// </summary>
	[SerializeField]
	eType type = eType.PlayOnce;
	/// <summary>
	/// 逆再生フラグ
	/// </summary>
	[SerializeField]
	bool _is_reverse = false;
	/// <summary>
	/// カーブ
	/// </summary>
	[SerializeField]
	AnimationCurve _curve = new AnimationCurve( new Keyframe( 0f, 0f ), new Keyframe( 1f, 1f ) );
	/// <summary>
	/// 再生を待つ時間
	/// </summary>
	[SerializeField]
	float _delay = 0.0f;
	/// <summary>
	/// 再生にかける時間
	/// </summary>
	[SerializeField]
	float _duration = 1.0f;
	/// <summary>
	/// 終了時のイベント
	/// </summary>
	[SerializeField]
	UnityEvent _on_finished = new UnityEvent();

	/// <summary>
	/// 逆再生フラグ
	/// </summary>
	public bool isReverse => _is_reverse;
	/// <summary>
	/// 終了時のイベント
	/// </summary>
	public UnityEvent onFinished => _on_finished;

	/// <summary>
	/// 現在のディレイ時間
	/// </summary>
	float _delay_time = 0.0f;
	/// <summary>
	/// 現在の再生時間
	/// </summary>
	float _time = 0.0f;

	/// <summary>
	/// Updateで時間を更新、値を更新する
	/// 必要であれば停止する（PlayOnceで時間経過
	/// </summary>
	private void Update() {

		if( _duration <= 0.0f ) {
#if DEBUG
			Debug.LogError( $"Tween:Update() duraion is under zero ( = {_duration:0.00} )", this );
#endif
			_duration = 1.0f;
		}

		// ディレイ中ならディレイ時間計算する
		if( _delay_time < _delay ) {
			_delay_time += Time.deltaTime;
			if( _is_reverse ) {
				_time = _duration;
			} else {
				_time = 0.0f;
			}
		}
		// 逆再生の場合
		else if( _is_reverse ) {
			switch( type ) {
			case eType.Loop:
				_time -= Time.deltaTime;
				if( _time < 0.0f ) {
					_time += _duration;
					if( _time < 0.0f ) {
						_time = _duration;
					}
				}
				break;
			case eType.PingPong:
				_time -= Time.deltaTime;
				if( _time < 0.0f ) {
					_time += 2.0f * _duration;
					if( _time < 0.0f ) {
						_time = _duration;
					}
				}
				break;
			default:
				_time -= Time.deltaTime;
				break;
			}
		// 通常再生の場合
		} else {
			switch( type ) {
			case eType.Loop:
				_time += Time.deltaTime;
				if( _time > _duration ) {
					_time -= _duration;
					if( _time > _duration ) {
						_time = 0.0f;
					}
				}
				break;
			case eType.PingPong:
				_time += Time.deltaTime;
				if( _time > 2.0f * _duration ) {
					_time -= 2.0f * _duration;
					if( _time > 2.0f * _duration ) {
						_time = 0.0f;
					}
				}
				break;
			default:
				_time += Time.deltaTime;
				break;
			}
		}

		// 再生時間からカーブからサンプリングする
		switch( type ) {
		case eType.Loop:
			_EvaluateValue( _time / _duration );
			break;
		case eType.PingPong:
			float t = _time / _duration;
			if( _time > _duration ) {
				t = ( 2.0f * _duration - _time ) / _duration;
			}
			_EvaluateValue( t );
			break;
		default:
			_EvaluateValue( Mathf.Clamp01( _time / _duration ) );
			break;
		}

		// 停止処理
		if( type == eType.PlayOnce ) {
			if( _is_reverse && _time <= 0.0f ) {
				enabled = false;
				_on_finished?.Invoke();
			} else if( !_is_reverse && _time > _duration ) {
				enabled = false;
				_on_finished?.Invoke();
			}
		}

	}

	/// <summary>
	/// 基本的にはカーブからのサンプリングを行い、値を更新する
	/// 別のサンプリングが必要ならここからオーバーライドする
	/// </summary>
	/// <param name="t">0-1に正規化した</param>
	protected virtual void _EvaluateValue( float t ) {
		float v = _curve.Evaluate( t );
		_UpdateValue( v );
	}

	/// <summary>
	/// 値の更新（適用）を行う
	/// AnimationCurveをしようする場合はここをオーバーライドする
	/// </summary>
	/// <param name="v">AnimationCurveからサンプリングした値</param>
	protected abstract void _UpdateValue( float v );

	/// <summary>
	/// 再生開始処理
	/// </summary>
	/// <param name="reverse">逆再生にするならtrue</param>
	public void Play( bool reverse = false ) {
		Reset( reverse );
		Resume( reverse );
	}

	/// <summary>
	/// 逆再生開始
	/// </summary>
	public void PlayReverse() {
		Play( reverse: true );
	}

	/// <summary>
	/// リセット処理
	/// </summary>
	/// <param name="reverse">逆再生にするならtrue</param>
	/// <param name="pause">同時にポーズするならtrue</param>
	public void Reset( bool reverse = false, bool pause = false ) {

		if( _delay < 0.0f ) {
#if DEBUG
			Debug.LogError( $"Tween:Reset() delay is less than zero ( = {_delay:0.00} )", this );
#endif
			_delay = 0.0f;
		}
		_delay_time = 0.0f;

		if( _duration <= 0.0f ) {
#if DEBUG
			Debug.LogError( $"Tween:Reset() duraion is under zero ( = {_duration:0.00} )", this );
#endif
			_duration = 1.0f;
		}

		_is_reverse = reverse;
		if( _is_reverse ) {
			_time = _duration;
		} else {
			_time = 0.0f;
		}

		_EvaluateValue( _time / _duration );

		if( pause ) {
			enabled = false;
		}
	}
	/// <summary>
	/// ポーズする
	/// </summary>
	public void Pause() {
		enabled = false;
	}
	/// <summary>
	/// 再生しなおす
	/// </summary>
	/// <param name="reverse">逆再生にするならtrue</param>
	public void Resume( bool reverse = false ) {
		_is_reverse = reverse;
		enabled = true;
	}
}
