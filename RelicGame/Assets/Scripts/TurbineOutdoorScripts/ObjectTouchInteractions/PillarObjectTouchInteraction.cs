using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class PillarObjectTouchInteraction : ObjectTouchInteraction {
	
	protected override void GestureStateRecognized ( ) {
		Subject.NotifySendAll( this, TurbineSceneEventListener.PillarEventSceneOne, "No Message" );
	}
}