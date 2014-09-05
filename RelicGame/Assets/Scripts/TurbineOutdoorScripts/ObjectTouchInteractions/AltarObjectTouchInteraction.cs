using UnityEngine;
using System.Collections;

public class AltarObjectTouchInteraction : ObjectTouchInteraction {
	
	protected override void GestureStateRecognized ( ) {
		Subject.NotifySendAll( this.gameObject, TurbineSceneEventListener.AltarObjectEvent, "No Message" );
	}
}
