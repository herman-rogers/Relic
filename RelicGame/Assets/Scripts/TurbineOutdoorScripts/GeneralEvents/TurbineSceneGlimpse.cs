using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurbineSceneGlimpse : UnityObserver {

	public Camera sceneCamera;
	static bool firstInteraction = true;

	public override void OnNotify ( Object sender, EventArguments e ) {
		if( e.eventMessage == TurbineSceneEventListener.AltarObjectEvent && !firstInteraction ) {
			StartCoroutine( ShowTurbineScene( ) );
		}
		firstInteraction = false;
	}

	IEnumerator ShowTurbineScene( ) {
		yield return new WaitForSeconds( 0.3f );
		sceneCamera.gameObject.SetActive( true );
		yield return new WaitForSeconds( 4.0f );
		sceneCamera.gameObject.SetActive( false );
	}
}
