using UnityEngine;

public class SceneFPS : MonoBehaviour
{
	private static float f_mTimeLeftCurrentInterval;
	private static float cachedFPS;
	private static float f_mAccumulation;
	private static int i_mNumberOfFrames;
	private const float f_mUpdateInterval = 0.5f;
	private const string fpsLabel = "FPS: ";

	public static string GetFramesPerSecond( )
	{
		UpdateFPS( );
		float framesPerSecond = ( f_mAccumulation / i_mNumberOfFrames );
		if( framesPerSecond == Mathf.Infinity )
		{
			return fpsLabel + cachedFPS;
		}
		cachedFPS = framesPerSecond;
		return fpsLabel + cachedFPS;
	}

	private static void UpdateFPS( )
	{
		if( SceneStepper.currentPauseState == PauseState.SCENE_IS_PAUSED )
		{
			return;
		}
		f_mTimeLeftCurrentInterval = f_mTimeLeftCurrentInterval - Time.deltaTime;
		f_mAccumulation = f_mAccumulation + ( Time.timeScale / Time.deltaTime );
		i_mNumberOfFrames = i_mNumberOfFrames + 1;
		if( f_mTimeLeftCurrentInterval > 0.0f )
		{
			return;
		}
		InitializeFpsLabel( );
	}

	private static void InitializeFpsLabel( )
	{
		f_mTimeLeftCurrentInterval = f_mUpdateInterval;
		f_mAccumulation = 0.0f;
		i_mNumberOfFrames = 0;
	}
}