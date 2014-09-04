using UnityEngine;
using System.Collections;

public class LevelUIStart : UnityObserver
{
	public UITexture fadeBackground;
	public UILabel test;
	private const float startDelay = 5.0f;
	private const float fadeTime = 0.009f;

	void Awake( ) {
//		CharacterController.togglePlayerInput = PlayerInput.STOP_PLAYER_INPUT;
		SetBackgroundToScreenSize( );
		fadeBackground.alpha = 1.0f;
		test.alpha = 0.0f;
		StartCoroutine( FadeBackground( ) );
	}

	private void SetBackgroundToScreenSize( ) {
		fadeBackground.GetComponent< UITexture >( ).width = Screen.width;
		fadeBackground.GetComponent< UITexture > ().height = Screen.height;
	}

	private IEnumerator FadeBackground( ) {
		yield return new WaitForSeconds( startDelay );
		while( fadeBackground.alpha > 0.0f ) {
			fadeBackground.alpha -= fadeTime;
			yield return new WaitForSeconds( fadeTime );
		}
//		CharacterController.togglePlayerInput = PlayerInput.START_PLAYER_INPUT;
	}
}