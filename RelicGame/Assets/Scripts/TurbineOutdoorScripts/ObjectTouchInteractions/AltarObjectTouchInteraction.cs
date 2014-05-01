using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class AltarObjectTouchInteraction : PressGesture {

	void Start( ) {
		base.Start( );
		StateChanged += AltarObjectInteraction;
	}
	
	protected virtual void AltarObjectInteraction(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			Subject.Notify( this, "AltarObjectEvent" );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}
}
