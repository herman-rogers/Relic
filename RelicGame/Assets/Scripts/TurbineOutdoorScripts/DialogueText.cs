using UnityEngine;
using System.Collections;

public class DialogueText : MonoBehaviour {

	public void AddTextToPlayerDialogue( string displayNewText ) {
		GetComponent< UILabel >( ).text = displayNewText;
	}
}
