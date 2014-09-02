using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SceneDebuggerObserver : Observer
{
	public const string TOGGLE_VIEWABLE = "TOGGLE_VIEWABLE";

	public override void OnNotify( Object sender, EventArguments e )
	{
		switch ( e.eventMessage )
		{
			case TOGGLE_VIEWABLE:
			    ToggleCanSeeDebug( );
			    break;
		}
	}

	private void ToggleCanSeeDebug( )
	{
		if( SceneDebugger.toggleView == ToggleDebugViewable.DEBUG_VIEWABLE )
		{
			SceneDebugger.toggleView = ToggleDebugViewable.DEBUG_HIDDEN;
			return;
		}
		SceneDebugger.toggleView = ToggleDebugViewable.DEBUG_VIEWABLE;
	}
}