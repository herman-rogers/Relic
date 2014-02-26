using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public class MonsterPressGesture : MonoBehaviour {
	private MonsterAnimations moveMonster;
	private PressGesture pressGesture;

	void Start( ){
		pressGesture = this.GetComponent< PressGesture >( );
		moveMonster = this.GetComponent< MonsterAnimations >( );
		if( pressGesture != null ) pressGesture.StateChanged += StateChangeHandler;
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ){
		switch( e.State ){
		case Gesture.GestureState.Recognized:
			moveMonster.StartWalkingAnimation(  );
			break;
		case Gesture.GestureState.Began:
			Debug.Log( e.State.ToString( ) + " has began!" );
			break;
		}
	}
}
