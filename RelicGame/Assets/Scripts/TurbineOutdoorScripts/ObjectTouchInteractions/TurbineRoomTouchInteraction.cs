using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class TurbineRoomTouchInteraction : PressGesture {

	void Start( ) {
		base.Start( );
		StateChanged += TurbineRoomObjectInteration;
	}
	
	protected virtual void TurbineRoomObjectInteration(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			Subject.Notify( this, "TurbineRoomEventOne" );
			break;
		case Gesture.GestureState.Began:
			break;
		}
	}
}
