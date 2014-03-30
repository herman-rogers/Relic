using UnityEngine;
using System.Collections;

public class FlickerTween : SpriteTween {

	public SpriteRenderer flickerSprite;

	public AnimationCurve frequency = AnimationCurve.Linear( 0.0f, 0.0f, 1.0f, 1.0f );
	public AnimationCurve addativeNoise = AnimationCurve.Linear( 0.0f, 0.0f, 1.0f, 1.0f );
	[Range( 1.0f, 10.0f )]
	public float rateOfFlicker = 2.0f;


	public void Update( ) {
//		from.color = new Color( 1.0f, 1.0f, 1.0f, 1.0f - frequency.Evaluate( Time.timeSinceLevelLoad ) );
		flickerSprite.color = new Color( 1.0f, 1.0f, 1.0f, Flicker( ) );
	}

	private float Flicker( ) {
		return frequency.Evaluate( Time.timeSinceLevelLoad / rateOfFlicker ) + addativeNoise.Evaluate( Time.timeSinceLevelLoad );
	}
}
