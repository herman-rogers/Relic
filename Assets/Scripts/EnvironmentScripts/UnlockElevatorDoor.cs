using UnityEngine;
using System.Collections;

public class UnlockElevatorDoor {
	
	void OnClick( ) {
		DoorLockElevator.isDoorOpen = true;
		Debug.Log( "OPEN'D!" );
	}

    //void Start( ) {
    //    base.Start( );
    //    StateChanged += shouldChangeScene;
    //}
	
    //protected void shouldChangeScene(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
    //    switch( e.State ){
    //    case Gesture.GestureState.Recognized:
    //        OnClick( );
    //        break;
    //    }
    //}

}
