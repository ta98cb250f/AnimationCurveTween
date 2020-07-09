/// <summary>
/// SharedMaterialのTween基盤
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenSharedMaterialBase.cs
/// </summary>
using UnityEngine;

/// <summary>
/// SharedMaterialのTween基盤
/// </summary>
public abstract class TweenSharedMaterialBase : TweenBase {

	//! マテリアル
	[SerializeField]
	protected Material _material = null;
	//! プロパティ名
	[SerializeField]
	string _property_name = "_Color";
	//! プロパティID
	protected int _property_id = -1;

	/// <summary>
	/// 値の更新
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected override void _UpdateValue( float v ) {

		if( !_material ) {
			enabled = false;
			return;
		}

		if( _property_id < 0 ) {
			_property_id = Shader.PropertyToID( _property_name );
			if( _property_id < 0 ) {
				enabled = false;
				return;
			}
		}

		_UpdateMaterialValue( v );
	}

	/// <summary>
	/// SharedMaterialのパラメータ更新処理
	/// </summary>
	/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
	protected abstract void _UpdateMaterialValue( float v );
}
