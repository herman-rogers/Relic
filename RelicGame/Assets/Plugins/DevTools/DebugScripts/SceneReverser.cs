using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneReverser : Observer {
	public const string RESET_REVERSER_STATE = "RESET_REVERSER_STATE";
	public const string REVERSER_STEP_FORWARD = "REVERSER_STEP_FORWARD";
	public const string REVERSER_STEP_BACK = "REVERSER_STEP_BACK";
    private int currentPositionInList;
	private float reverserResetTimer = 0.0f;
    private List< Vector3 > transformSnapShot;
	private List< Quaternion > quaternionSnapShot;
	private List< Vector3 > cameraSnapShot;
	private const int stepForward = 1;
	private const int stepBack = -1;
    
	public SceneReverser( )
	{
		transformSnapShot = new List< Vector3 >( );
		quaternionSnapShot = new List<Quaternion>( );
		cameraSnapShot = new List< Vector3 >( );
	}

	public override void UnityUpdate( )
	{
		RecordTransform( );
	}

	public override void OnNotify (Object sender, EventArguments e)
	{
		switch( e.eventMessage )
		{
			case SceneStepper.STATE_IS_PAUSED:
			    currentPositionInList = transformSnapShot.Count;
			    break;
		    case SceneStepper.STATE_IS_RESUMED:
			    ResetState( ); 
			    break;
			case RESET_REVERSER_STATE:
			    ResetState( );
			    break;
			case REVERSER_STEP_BACK:
			    Reverser( stepBack );
			    break;
			case REVERSER_STEP_FORWARD:
			    Reverser( stepForward );
			    break;
		}
	}

	private void RecordTransform( )
	{
		if( SceneStepper.currentPauseState == PauseState.SCENE_IS_PAUSED 
		    || SceneTransformTracker.currentlyTrackedObject == null )
		{
            return;
		}
		cameraSnapShot.Add( Camera.main.transform.position );
		transformSnapShot.Add( SceneTransformTracker.currentlyTrackedObject.transform.position );
		quaternionSnapShot.Add( SceneTransformTracker.currentlyTrackedObject.transform.rotation );
		reverserResetTimer += Time.deltaTime;
		if( reverserResetTimer >= 30.0f )
		{
			ResetState( );
		}
	}

	private void Reverser( int stepDirection )
	{
		if( SceneStepper.currentPauseState != PauseState.SCENE_IS_PAUSED )
		{
			Subject.Notify( SceneStepper.PAUSE_GAME );
		}
		currentPositionInList = ( currentPositionInList - ( stepDirection ) );
		if( transformSnapShot.Count <= currentPositionInList
		    || currentPositionInList <= 0 )
		{
			currentPositionInList = transformSnapShot.Count;
			return;
		}
		if( SceneTransformTracker.currentlyTrackedObject == null )
		{
			return;
		}
		SceneTransformTracker.currentlyTrackedObject.transform.position = transformSnapShot[ currentPositionInList ];
		SceneTransformTracker.currentlyTrackedObject.transform.rotation = quaternionSnapShot[ currentPositionInList ];
		Camera.main.transform.position = cameraSnapShot[ currentPositionInList ];
    }

	private void ResetState( )
	{
		reverserResetTimer = 0.0f;
		transformSnapShot.Clear( );
		quaternionSnapShot.Clear( );
		cameraSnapShot.Clear( );
    }
}