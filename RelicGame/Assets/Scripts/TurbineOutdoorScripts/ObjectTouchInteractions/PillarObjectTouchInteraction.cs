using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class PillarObjectTouchInteraction : PressGesture {

	void Start( ) {
		base.Start( );
		StateChanged += PillarObjectInteraction;
	}
	
	protected virtual void PillarObjectInteraction(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			Subject.Notify( this, "PillarEventSceneOne" );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}
}