using UnityEngine;
using System.Collections;

public class TurbineSceneEventListener : Observer {

	void Awake( ) {
		Subject.AddObserver( this );
	}
	
	public override void OnNotify (object sender, EventArguments e) {
		switch( e.EventMessage ){
		    case "PillarEventSceneOne":
			    AddTextToPlayersDialogue( "Pillar In Scene One" );
			    break;
		    case "TurbineRoomEventOne":
			    AddTextToPlayersDialogue( "Broken Turbine Room" );
			    break;
		    case "SecondLabEvent":
			    AddTextToPlayersDialogue( "You've Found the Second Lab" );
			    break;
			case "AltarObjectEvent":
			    AddTextToPlayersDialogue( "You've reached the first Altar" );
			    break;
		}
	}

	void AddTextToPlayersDialogue( string dialogueText ) {
		GameObject playerDialogueText = GameObject.Find( "PlayerInteractiveText" );
		playerDialogueText.GetComponent< DialogueText >( ).AddTextToPlayerDialogue( dialogueText );
	}
}