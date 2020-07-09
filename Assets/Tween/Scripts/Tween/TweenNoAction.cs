/// <summary>
/// 何もしないTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenNoAction.cs
/// </summary>

/// <summary>
/// 何もしないTween
/// </summary>
public class TweenNoAction : TweenBase  {

	/// <summary>
	/// カーブサンプリング前の時間の値を取得する段階
	/// 何もしない
	/// </summary>
	/// <param name="t">0-1に正規化された時間</param>
	protected override void _EvaluateValue( float t ) {
	}

	/// <summary>
	/// 値の更新
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected override void _UpdateValue( float v ) {
	}
}
