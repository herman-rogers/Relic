using UnityEngine;
using System.Collections;

public class TurbineSceneEventListener : Observer {

	static bool controlPanelFirstInteraction = true;
	static bool turbineSwitchFirstInteraction = true;
	static GameObject previouslyActivatedTurbine;

	void Awake( ) {
		Subject.AddObserver( this );
	}
	
	public override void OnNotify (object sender, EventArguments e) {
		switch( e.EventMessage ) {
		    case "PillarEventSceneOne":
			    FirstPillarEvent( );
			    break;
		    case "TurbineRoomEventOne":
			    TurbineElectricRoomEvent( );
			    break;
		    case "SecondLabEvent":
			    SecondLabEvent( );
			    break;
			case "AltarObjectEvent":
			    ActivateTurbineEvent( sender );
			    break;
			case "NPCEvent":
			    NPCInteractionEvent( sender );
			    break;
		}
	}

	void FirstPillarEvent( ) {
		AddTextToPlayersDialogue( "Mol r Great Twins, pan symbio et aut Twins au windioch\n" +
		                         "  \\ O /          |    \n" +
		                         "   \\   /       O | O  \n" +
		                         "     |           /  \\  \n" +    
		                         "     |          /    \\ \n");
	}
	
	void TurbineElectricRoomEvent( ) {
		if( controlPanelFirstInteraction ){
		    AddTextToPlayersDialogue( "There seems to be a control panel\n...what does it do?" );
			controlPanelFirstInteraction = false;
		} else {
			StartCoroutine( StartControlPanelEvent( ) );
		}
	}

	IEnumerator StartControlPanelEvent( ){
		yield return new WaitForSeconds( 1.0f );
		AddTextToPlayersDialogue( "[Playing Camera Pan]" );
		yield return new WaitForSeconds( 4.0f );
		AddTextToPlayersDialogue( "There Seems to be no power" );
	}

	void SecondLabEvent( ) {
		AddTextToPlayersDialogue( "The door won't budge..." );
	}


	void ActivateTurbineEvent( object sender ){
		GameObject currentTurbineSwitch = (GameObject)sender;
		if( turbineSwitchFirstInteraction ){
			turbineSwitchFirstInteraction = false;
			AddTextToPlayersDialogue( "It appears to be some sort of release switch." );
		} else {
			if( previouslyActivatedTurbine != null ){
				previouslyActivatedTurbine.GetComponent< SpriteRenderer >( ).sprite.name = "stubPillarOff";
			}
			currentTurbineSwitch.GetComponent< SpriteRenderer >( ).sprite.name = "stubPillarOn";
			previouslyActivatedTurbine = currentTurbineSwitch;
		}
	}


	void NPCInteractionEvent( object sender ) {
		GameObject npcName = (GameObject)sender;
		if( npcName.name == "NPCOne" ) {
			AddTextToPlayersDialogue( "Mol r Great Twins,\n" +
				                      "for pan fydt dancio,\n" +
				                      "casom een bendio aut electro" );
		}
		if( npcName.name == "NPCTwo" ) {

		} else {
			HumitePriestInteractions( );
		}
	}

	void HumitePriestInteractions( ) { /*For Future Use*/ }

	void AddTextToPlayersDialogue( string dialogueText ) {
		GameObject playerDialogueText = GameObject.Find( "PlayerInteractiveText" );
		playerDialogueText.GetComponent< DialogueText >( ).AddTextToPlayerDialogue( dialogueText );
	}
}