﻿using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public class dyCharacterPressGesture : MonoBehaviour {
	private CharacterController moveMonster;
	private PressGesture pressGesture;

	void Start( ){
		pressGesture = this.GetComponent< PressGesture >( );
		moveMonster = this.GetComponent< CharacterController >( );
		if( pressGesture != null ) pressGesture.StateChanged += StateChangeHandler;
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ){
		switch( e.State ){
		case Gesture.GestureState.Recognized:
			moveMonster.MoveMonsterOnXAxis( pressGesture.ScreenPosition.x, 
			                                CharacterController.AnimationList.Walking, true );
			break;
		}
	}
}