using UnityEngine;
using System.Collections;

public class TurbineRoomTouchInteraction : ObjectTouchInteraction {

	protected override void GestureStateRecognized ( ) {
		Subject.Notify( this, TurbineSceneEventListener.TurbineRoomEventOne );
	}
}
