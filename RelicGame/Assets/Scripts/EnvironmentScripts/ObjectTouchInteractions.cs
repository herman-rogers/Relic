using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class ObjectTouchInteractions : PressGesture {

	void Start( ) {
		base.Start( );
		StateChanged += NewObjectInteraction;
	}

	protected virtual void NewObjectInteraction(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
		switch( e.State ){
		case Gesture.GestureState.Recognized:
			ActivateNewEvent( );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}
	
	protected virtual void ActivateNewEvent( ) { }
}
