using UnityEngine;
using System.Collections;

public enum cutsceneState
{
    lerpIn,
    lerpOut,
    playing,
    stopped
}

public class cutsceneCamera : MonoBehaviour {

    cutsceneZone currentScene;
    public cutsceneZone CurrentScene
    {
        get
        {
            return currentScene;
        }
        set
        {
            currentScene = value;
        }
    }

    cutsceneState state;
    cutsceneState State
    {
        get
        {
            return state;
        }
        set
        {
            if(value == cutsceneState.lerpIn || value == cutsceneState.lerpOut)
            {
                lerpBeginTimestamp = Time.time;
                lerpStartPosition = transform.localPosition;
                lerpStartRotation = transform.localRotation;
            }
            if (value == cutsceneState.playing)
            {
            }
            state = value;
        }
    }

    [SerializeField]
    float lerpTime = 1;
    float lerpBeginTimestamp;

    Vector3 lerpStartPosition;
    Quaternion lerpStartRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentScene) {
            switch (State)
            {
                case cutsceneState.lerpIn:
                    float t = (Time.time - lerpBeginTimestamp) / lerpTime;
                    transform.localPosition = Vector3.Lerp(lerpStartPosition, Vector3.zero, t);
                    transform.localRotation = Quaternion.Lerp(lerpStartRotation, Quaternion.identity, t);
                    if (t >= lerpTime)
                    {
                        State = cutsceneState.playing;
                    }
                    break;
                case cutsceneState.lerpOut:
                    break;
                case cutsceneState.playing:
                    break;
                case cutsceneState.stopped:
                    break;
            }
        }
	}

    public void playScene(cutsceneZone scene)
    {
        CurrentScene = scene;
        transform.parent = CurrentScene.CameraDock;
        State = cutsceneState.lerpIn;
    }
}
