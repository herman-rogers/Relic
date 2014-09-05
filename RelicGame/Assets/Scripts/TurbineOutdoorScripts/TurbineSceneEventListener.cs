using UnityEngine;
using System.Collections;

public class TurbineSceneEventListener : Observer {
	public const string PillarEventSceneOne = "PillarEventSceneOne";
	public const string TurbineRoomEventOne = "TurbineRoomEventOne";
	public const string SecondLabEvent = "SecondLabEvent";
	public const string AltarObjectEvent = "AltarObjectEvent";
	public const string NPCEvent = "NPCEvent";
//	TurbineEvents sceneEvents;

//	protected override void GetSceneEvents( ) {
//		sceneEvents = eventListener.GetComponent< TurbineEvents >( );
//	}

//	public override void OnNotify ( object sender, EventArguments e ) {
//		switch( e.EventMessage ) {
//		    case PillarEventSceneOne:
//			    sceneEvents.FirstPillarEvent( );
//			    break;
//		    case TurbineRoomEventOne:
//			    sceneEvents.TurbineElectricRoomEvent( );
//			    break;
//			case SecondLabEvent:
//			    sceneEvents.SecondLabEvent( );
//			    break;
//			case AltarObjectEvent:
//			    sceneEvents.ActivateAltarEvent( sender );
//			    break;
//			case NPCEvent:
//			    sceneEvents.NPCInteractionEvent( sender );
//			    break;
//		}
//	}
}