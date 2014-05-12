using UnityEngine;
using System.Collections;

public class AltarObjectTouchInteraction : ObjectTouchInteraction {
	
	protected override void GestureStateRecognized ( ) {
		Subject.Notify( this.gameObject, "AltarObjectEvent" );
	}
}
