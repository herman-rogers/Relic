using UnityEngine;
using System.Collections;

public class DialogueText : MonoBehaviour {

	UILabel playerDialogueText;
	bool coroutineRunning;

	void Awake( ){
		playerDialogueText = GetComponent< UILabel >( );
	}

	public void AddTextToPlayerDialogue( string displayNewText ) {
		if( !coroutineRunning ){
		    StartCoroutine( ScrollTextToScreen( displayNewText ) );
		}
	}

	IEnumerator ScrollTextToScreen( string newText ){
		coroutineRunning = true;
		foreach( char text in newText ){
			playerDialogueText.text += text;
			yield return new WaitForSeconds( 0.05f );
		}
		yield return new WaitForSeconds( 3.0f );
		playerDialogueText.text = "";
		coroutineRunning = false;
	}
}
