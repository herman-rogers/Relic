using UnityEngine;
using System.Collections;

public class HumiteTouchInteraction : ObjectTouchInteraction {

	protected override void GestureStateRecognized( ) {
		Subject.Notify( this.gameObject, "NPCEvent" );
	}
}
