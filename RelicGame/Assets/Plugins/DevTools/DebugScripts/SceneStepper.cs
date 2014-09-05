using UnityEngine;
using System.Collections;

public enum PauseState
{
	SCENE_IS_PAUSED,
	SCENE_IS_RESUMED,
}

public class SceneStepper : UnityObserver 
{
	public static PauseState currentPauseState = PauseState.SCENE_IS_RESUMED;
	public const string STATE_IS_PAUSED = "STATE_IS_PAUSED";
	public const string STATE_IS_RESUMED = "STATE_IS_RESUMED";
	public const string PAUSE_GAME = "PAUSE_GAME";
	public const string STEP_THROUGH = "STEP_THROUGH";
	private const float sceneStepTime = 0.009f;

	public override void OnNotify (Object sender, EventArguments e)
	{
		switch( e.eventMessage )
		{
			case PAUSE_GAME:
			TogglePauseState( );
			break;
			case STEP_THROUGH:
			StartCoroutine( StepThroughScene( ) );
			break;
		}
	}
	
	private void TogglePauseState( )
	{
		if( currentPauseState == PauseState.SCENE_IS_RESUMED )
		{
			PauseFrame( );
			return;
		}
		ResumeFrame( );
		StopAllCoroutines( );
	}
	 
	IEnumerator StepThroughScene( )
	{
		ResumeFrame( );
		yield return new WaitForSeconds( sceneStepTime );
		PauseFrame( );
	}

	private void ResumeFrame( )
	{
		currentPauseState = PauseState.SCENE_IS_RESUMED;
		Subject.Notify( STATE_IS_RESUMED );
		Time.timeScale = 1.0f;
	}

	private void PauseFrame( )
	{
		currentPauseState = PauseState.SCENE_IS_PAUSED;
		Subject.Notify( STATE_IS_PAUSED );
        Time.timeScale = 0.0f;
	}
}