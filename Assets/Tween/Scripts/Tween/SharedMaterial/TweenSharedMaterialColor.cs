/// <summary>
/// SharedMaterialのカラーTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenSharedMaterialColor.cs
/// </summary>
using UnityEngine;

/// <summary>
/// SharedMaterialのカラーTween
/// </summary>
public class TweenSharedMaterialColor : TweenSharedMaterialBase {

	//! 最初の色
	[SerializeField]
	Color _from = Color.gray;
	//! 最後の色
	[SerializeField]
	Color _to = Color.white;
	//! 条件フラグ
	[SerializeField]
	eColorConstraints _option = 0;


	/// <summary>
	/// SharedMaterialのパラメータ更新処理
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected override void _UpdateMaterialValue( float v ) {

		Color value = _material.GetColor( _property_id );

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

		_material.SetColor( _property_id, value );
	}
}
