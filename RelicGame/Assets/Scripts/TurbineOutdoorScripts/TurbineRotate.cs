using UnityEngine;
using System.Collections;

public class TurbineRotate : UnityObserver {

	public GameObject windTurbineOne;
	public GameObject windTurbineTwo;

	public override void OnNotify( object sender, EventArguments e ) {
		if( e.EventMessage == TurbineSceneEventListener.AltarObjectEvent ){
			ActivateTurbine( );
		}
	}

	void ActivateTurbine( ){
		StopAllCoroutines( );
		if( TurbineEvents.currentTurbineState != TurbineEvents.TurbineState.turbinesInactive ) {
			StartCoroutine( RotateSprite( ) );
		}
		if ( TurbineEvents.currentTurbineState == TurbineEvents.TurbineState.bothTurbinesActive ) {
			StartCoroutine( RotateBothSprites( ) );
		}
	}
	
	IEnumerator RotateSprite( ) {
		while( true ){
			if( TurbineEvents.currentTurbineState == TurbineEvents.TurbineState.firstTurbineActive ) {
			    windTurbineOne.transform.Rotate( Vector3.forward );
			}
			if( TurbineEvents.currentTurbineState == TurbineEvents.TurbineState.secondTurbineActive ){
			    windTurbineTwo.transform.Rotate( Vector3.forward );
			}
			yield return new WaitForSeconds( 0.009f );
		}
	}

	IEnumerator RotateBothSprites( ){
		while( true ){
			windTurbineOne.transform.Rotate( Vector3.forward );
			windTurbineTwo.transform.Rotate( Vector3.forward );
		}
	}
}
