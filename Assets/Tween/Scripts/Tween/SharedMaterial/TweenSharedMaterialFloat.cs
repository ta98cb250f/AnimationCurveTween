/// <summary>
/// SharedMaterialのFloatプロパティTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenSharedMaterialFloat.cs
/// </summary>
using UnityEngine;

/// <summary>
/// SharedMaterialのFloatプロパティTween
/// </summary>
public class TweenSharedMaterialFloat : TweenSharedMaterialBase {

	//! 0に相当するVector
	[SerializeField]
	float _from = 0.0f;
	//! 1に相当するVector
	[SerializeField]
	float _to = 1.0f;

	/// <summary>
	/// SharedMaterialのパラメータ更新処理
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected override void _UpdateMaterialValue( float v ) {

		float value = _from * ( 1.0f - v ) + _to * v;

		_material.SetFloat( _property_id, value );
	}
}
