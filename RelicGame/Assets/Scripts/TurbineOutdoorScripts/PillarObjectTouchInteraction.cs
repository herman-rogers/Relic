using UnityEngine;
using System.Collections;

public class PillarObjectTouchInteraction : ObjectTouchInteractions {

	protected override void ActivateNewEvent ( ) {
		Subject.Notify( this, "TestEvent" );
	}
}