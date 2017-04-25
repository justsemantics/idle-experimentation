using UnityEngine;
using System.Collections;

public class animateKaleidStopStart : MonoBehaviour, Activateable
{

    Material mat;

    [SerializeField]
    float moveAmount, moveTime, waitTime;

    float lastStop;
    float startTime;
    float stopTime;
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
                stopTime = Time.time;
                phase += Time.time - startTime;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        startTime = 0;
        phase = 0;
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            float t = (Time.time - startTime) / moveTime;
            float offY = lastStop + moveAmount * Mathf.Pow(Mathf.SmoothStep(0, 1, t), 1);
            mat.SetFloat("_offY", offY);

            if(t >= 1)
            {
                SetPlaying(false);
                lastStop += moveAmount;
            }
        }
        else
        {
            if (Time.time - stopTime > waitTime)
            {
                SetPlaying(true);
            }
        }
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
