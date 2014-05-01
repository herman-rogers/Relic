using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SecondLabTouchInteraction : PressGesture {

	void Start( ) {
		base.Start( );
		StateChanged += SecondLabObjectInteraction;
	}
	
	protected virtual void SecondLabObjectInteraction(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			Subject.Notify( this, "SecondLabEvent" );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}
}
