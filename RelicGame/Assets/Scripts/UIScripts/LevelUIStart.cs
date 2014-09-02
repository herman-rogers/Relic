using UnityEngine;
using System.Collections;

public class LevelUIStart : UnityObserver
{
	public UITexture fadeObjects;
	public UILabel test;
	private const float startDelay = 5.0f;
	private const float fadeTime = 0.009f;

	void Awake( )
	{
		CharacterController.togglePlayerInput = PlayerInput.STOP_PLAYER_INPUT;
		fadeObjects.alpha = 1.0f;
		test.alpha = 0.0f;
		StartCoroutine( FadeBackground( ) );
	}

	private IEnumerator FadeBackground( )
	{
		yield return new WaitForSeconds( startDelay );
		while( fadeObjects.alpha > 0.0f )
		{
			fadeObjects.alpha -= fadeTime;
			yield return new WaitForSeconds( fadeTime );
		}
		CharacterController.togglePlayerInput = PlayerInput.START_PLAYER_INPUT;
	}
}