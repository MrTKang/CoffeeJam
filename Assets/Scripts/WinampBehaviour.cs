using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinampBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource BackgroundMusic;
    public AudioSource CelebrationLoop;
    private float loopTimeCollapsed;
    private const float LOOP_TIME = 0.8571428571428576f;
    private const float LOOP_OFFSET = -0.2f;

    private PlayState currentState = PlayState.Start;
    private float lastLoopPlaybackTime = 0f;

    public enum PlayState
    {
        Playing,
        Queued,
        Muted,
        Start
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var loopPlaybackTime = (CelebrationLoop.time + LOOP_OFFSET) % LOOP_TIME;
        if (loopPlaybackTime < lastLoopPlaybackTime)
        {
            switch (currentState)
            {
                case PlayState.Muted:
                    break;
                case PlayState.Queued:
                    CelebrationLoop.mute = false;
                    currentState = PlayState.Playing;
                    break;
                case PlayState.Playing:
                    CelebrationLoop.mute = true;
                    currentState = PlayState.Muted;
                    break;
            }
        }
        lastLoopPlaybackTime = loopPlaybackTime;
    }

    public void PlayCelebration()
    {
        currentState = PlayState.Queued;
    }
}
