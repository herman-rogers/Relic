using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneTransformTracker : Observer
{
	[HideInInspector]
	public static GameObject currentlyTrackedObject;
	public const string LEFT_MOUSE_BUTTON = "LEFT_MOUSE_BUTTON";
	public const string RIGHT_MOUSE_BUTTON = "RIGHT_MOUSE_BUTTON";
	private List<GameObject> trackedGameObjects = new List<GameObject>( );

	public override void OnNotify (Object sender, EventArguments e)
	{
		if( e.eventMessage == LEFT_MOUSE_BUTTON || e.eventMessage == RIGHT_MOUSE_BUTTON )
		{
			AddRemoveTrackObjects( e.eventMessage );
		}
	}

	public void TrackTransformsInScene( )
	{
		DisplayAboveTrackedObjects( );
	}

	public void ResetTrackedObjects( )
	{
		currentlyTrackedObject = null;
		trackedGameObjects.Clear( );
	}

	public void ListTrackedObjects( )
	{
		int boxCounter = 200;
		trackedGameObjects.RemoveAll( item => item == null );
		foreach( GameObject gameObject in trackedGameObjects )
		{
			if( gameObject == null )
		    {
			    continue;
		    }
		    if( SceneDebugger.toggleView == ToggleDebugViewable.DEBUG_VIEWABLE )
		    {
			    GUI.Label(  new Rect( 10, boxCounter, 200, 100 ), gameObject.name + 
				                                                  "\n" + gameObject.transform.position );
			    boxCounter += 40;
		    }
		}
	}

	private void AddRemoveTrackObjects( string eventMessage )
	{
		RaycastHit hit;
		Ray testRay = Camera.main.ScreenPointToRay( Input.mousePosition );
		if( !Physics.Raycast( testRay, out hit ) )
		{
			return;
		}
		if( eventMessage == LEFT_MOUSE_BUTTON )
		{
			RemoveTrackedObject( hit.transform.gameObject );
		} 
		if( eventMessage == RIGHT_MOUSE_BUTTON )
		{
			AddTrackedObject( hit.transform.gameObject );
		}
	}

	private void DisplayAboveTrackedObjects( )
	{
		trackedGameObjects.RemoveAll( item => item == null );
		foreach( GameObject gameObject in trackedGameObjects )
		{
			if( gameObject == null )
			{
				continue;
			}
			Vector3 cachedPosition = Camera.main.WorldToScreenPoint( gameObject.transform.position );
			GUI.Label( new Rect( cachedPosition.x, cachedPosition.y, 120, 50 ), 
			                     gameObject.name + "\n" + gameObject.transform.position );
		}
	}

	private void AddTrackedObject( GameObject trackedObject )
	{
		if( trackedGameObjects.Contains( trackedObject ) )
		{
			return;
		}
		Subject.Notify( SceneReverser.RESET_REVERSER_STATE );
		trackedGameObjects.Add( trackedObject );
		currentlyTrackedObject = trackedObject;
	}
	
	private void RemoveTrackedObject( GameObject trackedObject )
	{
		if( !trackedGameObjects.Contains( trackedObject ) )
		{
			return;
		}
		trackedGameObjects.Remove( trackedObject );
		currentlyTrackedObject = null;
	}
}