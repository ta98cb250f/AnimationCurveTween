/// <summary>
/// SharedMaterialのVectorプロパティTween
///
/// @author t-yoshino
/// @date 2020/07/09
/// @file TweenSharedMaterialVecotr.cs
/// </summary>
using UnityEngine;

namespace UGUITween {

	/// <summary>
	/// SharedMaterialのVectorプロパティTween
	/// </summary>
	public class TweenSharedMaterialVecotr : TweenSharedMaterialBase {

		//! 0に相当するVector
		[SerializeField]
		Vector4 _from = new Vector4( 0.0f, 0.0f, 0.0f, 0.0f );
		//! 1に相当するVector
		[SerializeField]
		Vector4 _to = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
		//! 条件フラグ
		[SerializeField]
		eVectorConstraints _option = 0;


		/// <summary>
		/// SharedMaterialのパラメータ更新処理
		/// </summary>
		/// <param name="v">カーブからサンプリングした0−1で正規化された値</param>
		protected override void _UpdateMaterialValue( float v ) {

			Vector4 value = _material.GetVector( _property_id );

			if( !_option.HasFlag( eVectorConstraints.Ignore_X ) ) {
				value.x = _from.x * ( 1.0f - v ) + _to.x * v;
			}
			if( !_option.HasFlag( eVectorConstraints.Ignore_Y ) ) {
				value.y = _from.y * ( 1.0f - v ) + _to.y * v;
			}
			if( !_option.HasFlag( eVectorConstraints.Ignore_Z ) ) {
				value.z = _from.z * ( 1.0f - v ) + _to.z * v;
			}
			if( !_option.HasFlag( eVectorConstraints.Ignore_W ) ) {
				value.w = _from.w * ( 1.0f - v ) + _to.w * v;
			}

			_material.SetVector( _property_id, value );
		}
	}
}
