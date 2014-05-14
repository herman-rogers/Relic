using UnityEngine;
using System.Collections;

public class TurbineSceneEventListener : Observer {
	TurbineEvents sceneEvents;

	protected override void GetSceneEvents( ) {
		sceneEvents = eventListener.GetComponent< TurbineEvents >( );
	}

	public override void OnNotify ( object sender, EventArguments e ) {
		switch( e.EventMessage ) {
		    case "PillarEventSceneOne":
			    sceneEvents.FirstPillarEvent( );
			    break;
		    case "TurbineRoomEventOne":
			    sceneEvents.TurbineElectricRoomEvent( );
			    break;
		    case "SecondLabEvent":
			    sceneEvents.SecondLabEvent( );
			    break;
			case "AltarObjectEvent":
			    sceneEvents.ActivateAltarEvent( sender );
			    break;
			case "NPCEvent":
			    sceneEvents.NPCInteractionEvent( sender );
			    break;
		}
	}
}