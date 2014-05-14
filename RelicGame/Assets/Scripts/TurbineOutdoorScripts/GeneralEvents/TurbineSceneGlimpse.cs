using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurbineSceneGlimpse : UnityObserver {

	Transform[] turbineSceneItems = new Transform[]{ };

	void Start( ) {
		turbineSceneItems = GetComponentsInChildren< Transform >( );
	}

	public override void OnNotify ( object sender, EventArguments e ) {
		if( e.EventMessage == "AltarObjectEvent" ) {
			StartCoroutine( ShowTurbineScene( ) );
		}
	}

	IEnumerator ShowTurbineScene( ) {
		yield return new WaitForSeconds( 1.0f );
		foreach( Transform turbineItem in turbineSceneItems ){
			ChangeTurbineObjectState( true );
		}
		yield return new WaitForSeconds( 5.0f );
		ChangeTurbineObjectState( false );
	}

	void ChangeTurbineObjectState( bool state ){
		foreach( Transform turbineItem in turbineSceneItems ) {
			turbineItem.gameObject.SetActive( state );
		}
	}
}
