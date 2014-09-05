using UnityEngine;
using System.Collections;

public enum ToggleDebugViewable
{
	DEBUG_VIEWABLE,
	DEBUG_HIDDEN
}

public class SceneDebugger : UnityObserver
{
	public static ToggleDebugViewable toggleView = ToggleDebugViewable.DEBUG_HIDDEN;
	public const string versionNumber = "v0.01.1";
	public const string SHOW_GUI = "SHOW_GUI";
	#pragma warning disable 0414
	private SceneReverser sceneReverser;
	private SceneTransformTracker transformTracker;
	private bool coroutineRunning;
	private Rect dragWindow = new Rect( Screen.width - 220, 0, 250, 450 );

	void Start( )
	{
		DontDestroyOnLoad( this );
		transformTracker = new SceneTransformTracker( );
		#pragma warning disable 0168
		sceneReverser = new SceneReverser( );
		#pragma warning disable 0168
		var sceneDebuggerObserver = new SceneDebuggerObserver( );
	}

	void Update( )
	{
		sceneReverser.UnityUpdate( );
	}

	public override void OnNotify (Object sender, EventArguments e)
	{
		if( e.eventMessage == SHOW_GUI )
		{
			ToggleDebugGUI( );
		}
	}

	void OnGUI( )
	{
		if( toggleView == ToggleDebugViewable.DEBUG_VIEWABLE  )
		{
			ShowMainGUI( );
		}
		transformTracker.TrackTransformsInScene( );
	}

	private void ToggleDebugGUI( )
	{
		Subject.NotifySendAll( gameObject, SceneDebuggerObserver.TOGGLE_VIEWABLE, "NoMessage" );
    }
    
    private void ShowMainGUI( )
	{
		dragWindow = GUI.Window( 0, dragWindow, GUIDragWindow, "Unity Debug Tools " + versionNumber );
	}

	private void GUIDragWindow( int windowID )
	{
		string currentReverserTarget = "No Target";
		if( SceneTransformTracker.currentlyTrackedObject != null )
		{
			currentReverserTarget = SceneTransformTracker.currentlyTrackedObject.name;
		}
		GUI.Label( new Rect( 10, 20, 200, 100 ), SceneFPS.GetFramesPerSecond( ) );
		GUI.Label( new Rect( 10, 40, 200, 100 ), "Obs: " + Subject.ObserverCount( ) );
		GUI.Label( new Rect( 10, 60, 200, 100 ), "UnityObs: " + Subject.UnityObserverCount( ) );
		GUI.Label( new Rect( 10, 80, 200, 100 ), "Reverser Target: " + currentReverserTarget );
		if ( GUI.Button( new Rect( 10, 140, 200, 20 ), "Clear Tracked" ) )
		{
			transformTracker.ResetTrackedObjects( );
		}
		GUI.Label( new Rect( 10, 160, 200, 20 ), "Tracked Objects: " );
		transformTracker.ListTrackedObjects( );
		GUI.DragWindow( );
	}
}