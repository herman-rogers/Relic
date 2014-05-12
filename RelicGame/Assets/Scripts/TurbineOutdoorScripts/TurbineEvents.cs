using UnityEngine;
using System.Collections;

public class TurbineEvents : MonoBehaviour {
	public Sprite onSprite;
	public Sprite offSprite;
	static bool controlPanelFirstInteraction = true;
	static bool turbineSwitchFirstInteraction = true;
	static GameObject previouslyActivatedTurbine;

	void Awake( ){
	    TurbineSceneEventListener startListening = new TurbineSceneEventListener( );
		DontDestroyOnLoad( this );
	}

	public void FirstPillarEvent( ) {
		AddTextToPlayersDialogue( "Mol r Great Twins, pan symbio et aut Twins au windioch\n" +
		                         "  \\ O /          |    \n" +
		                         "   \\   /       O | O  \n" +
		                         "     |           /  \\  \n" +    
		                         "     |          /    \\ \n");
	}
	
	public void TurbineElectricRoomEvent( ) {
		if( controlPanelFirstInteraction ){
			AddTextToPlayersDialogue( "There seems to be a control panel\n...what does it do?" );
			controlPanelFirstInteraction = false;
		} else {
			StartCoroutine( StartControlPanelEvent( ) );
		}
	}
	
	public IEnumerator StartControlPanelEvent( ){
		yield return new WaitForSeconds( 1.0f );
		AddTextToPlayersDialogue( "[Playing Camera Pan]" );
		yield return new WaitForSeconds( 4.0f );
		AddTextToPlayersDialogue( "There Seems to be no power" );
	}
	
	public void SecondLabEvent( ) {
		AddTextToPlayersDialogue( "The door won't budge..." );
	}
	
	
	public void ActivateAltarEvent( object sender ){
		GameObject currentTurbineSwitch = ( GameObject )sender;
		if( turbineSwitchFirstInteraction ) {
			turbineSwitchFirstInteraction = false;
			AddTextToPlayersDialogue( "It appears to be some sort of release switch." );
		} else {
			if( previouslyActivatedTurbine != null ) {
				previouslyActivatedTurbine.GetComponent< SpriteRenderer >( ).sprite = offSprite;
			}
			currentTurbineSwitch.GetComponent< SpriteRenderer >( ).sprite = onSprite;
			previouslyActivatedTurbine = currentTurbineSwitch;
		}
	}
	
	
	public void NPCInteractionEvent( object sender ) {
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
