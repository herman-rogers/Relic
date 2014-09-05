using UnityEngine;
using System.Collections;

public class CharacterAnimations {
	public AnimationList runningAnimation{ get; private set; }
	SkeletonAnimation spineAnimation;
	GameObject sceneCharacter;

	public enum AnimationList {
		Walking,
		Idle,
		Running,
		Activate,
	}

	public void InitAnimations( GameObject characterObject ) {
		this.sceneCharacter = characterObject.transform.parent.gameObject;
		this.spineAnimation = sceneCharacter.GetComponent< SkeletonAnimation >( );
		this.runningAnimation = AnimationList.Idle;
        this.spineAnimation.state.Complete += AnimationComplete;
	}

	public void PlayNewAnimation( AnimationList animation ){
		PlayNewAnimation( animation, false );
	}

	public void PlayNewAnimation( AnimationList animation, bool isLooped ){
		spineAnimation.state.AddAnimation( 1, animation.ToString( ), isLooped, 0.5f );
		runningAnimation = animation;
	}

	void AnimationComplete( Spine.AnimationState animationState, int trackIndex, int loopCount ){
		switch( animationState.ToString( ) ){
		case "Idle" :
			runningAnimation = AnimationList.Idle;
			break;
		case "Walking":
			runningAnimation = AnimationList.Walking;
			break;
		case "Running":
			runningAnimation = AnimationList.Running;
			break;
		case "Activate":
			runningAnimation = AnimationList.Activate;
			break;
		}
	}
}
