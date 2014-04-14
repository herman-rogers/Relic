using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public class CharacterPressGesture : PressGesture {
	private CharacterController moveMonster;

	void Start( ){
		base.Start( );
		moveMonster = this.GetComponent< CharacterController >( );
		StateChanged += StateChangeHandler;
	}

	void StateChangeHandler( object sender, TouchScript.Events.GestureStateChangeEventArgs e ){
		switch( e.State ){
		case Gesture.GestureState.Recognized:
			moveMonster.MoveMonsterOnXAxis( ScreenPosition.x, 
			                                CharacterAnimations.AnimationList.Walking, true );
			break;
		}
	}
}
