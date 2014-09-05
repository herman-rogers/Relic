using UnityEngine;
using System.Collections;

public class TurbineRoomTouchInteraction : ObjectTouchInteraction {

	protected override void GestureStateRecognized ( ) {
		Subject.NotifySendAll( this, TurbineSceneEventListener.TurbineRoomEventOne, "No Message" );
	}
}
