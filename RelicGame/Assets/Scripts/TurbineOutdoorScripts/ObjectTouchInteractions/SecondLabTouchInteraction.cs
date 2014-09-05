using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SecondLabTouchInteraction : ObjectTouchInteraction {

	protected override void GestureStateRecognized( ){
		Subject.NotifySendAll( this, TurbineSceneEventListener.SecondLabEvent, "No Message" );
	}
}
