using UnityEngine;
using System.Collections;

public interface Activateable
{
    void Activate();
}

public class animateKaleidTexture : MonoBehaviour, Activateable {

    Material mat;

    [SerializeField]
    float xSpeed, ySpeed, xScale, yScale, colorSpeed;

    [SerializeField]
    Color Color1, Color2, Color3, Color4;

    float startTime;
    float phase;
    bool playing;

    public bool Playing
    {
        get
        {
            return playing;
        }
        private set
        {
            playing = value;

            if (playing)
            {
                startTime = Time.time;
            }
            else if (!playing)
            {
                phase += Mathf.PI * 2 * (Time.time - startTime);
            }
        }
    }

	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        startTime = 0;
        phase = 0;
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Playing)
        {
            float t = Mathf.PI * 2 * (Time.time - startTime) + phase;
            mat.SetFloat("_offX", xScale * Mathf.Sin(xSpeed * t));
            mat.SetFloat("_offY", yScale * Mathf.Sin(ySpeed * t));
        }

        //colorSpeed = Mathf.SmoothStep(0.02f, 10, Mathf.PingPong(Time.time * 0.1f, 1)) + 0.5f;
        //Color lerpedColor1 = Color.Lerp(Color1, Color2, Mathf.SmoothStep(0, 1, Time.time * colorSpeed % 1));
        //Color lerpedColor2 = Color.Lerp(Color3, Color4, Mathf.SmoothStep(0, 1, Time.time * colorSpeed % 1));

        //mat.SetColor("_Color1", lerpedColor1);
        //mat.SetColor("_Color2", lerpedColor2);
    }

    void SetPlaying(bool setTo)
    {
        Playing = setTo;
    }

    public void Activate()
    {
        SetPlaying(!Playing);
    }
}
