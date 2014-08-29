using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class ObjectTouchInteraction : PressGesture {

	public float interactionDistance = 3.5f;
	public GameObject mainCharacter;

	protected override void Start( ) {
		base.Start( );
		StateChanged += ObjectInteraction;
	}

	protected virtual void GestureStateRecognized( ){ }
	
	protected virtual void GestureStateBegan( ){ }

	void ObjectInteraction( object sender, TouchScript.Events.GestureStateChangeEventArgs e ){
		switch( e.State ) {
		case Gesture.GestureState.Recognized:
			if( IsPlayerCloseEnough( ) ){ GestureStateRecognized( ); }
			break;
		case Gesture.GestureState.Began:
			if( IsPlayerCloseEnough( ) ) { GestureStateBegan( ); }
			break;
		}
	}

	bool IsPlayerCloseEnough( ){
		float playersDistanceFromObject = Vector3.Distance( mainCharacter.transform.position, this.transform.position );
		return( interactionDistance > playersDistanceFromObject );
	}
}
