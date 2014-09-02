using UnityEngine;
using System.Collections;

public class HumiteTouchInteraction : ObjectTouchInteraction {

	protected override void GestureStateRecognized( ) {
		Subject.NotifySendAll( this.gameObject, "NPCEvent", "No Message" );
	}
}
