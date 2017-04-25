using UnityEngine;
using System.Collections;

public class sonarCamera : MonoBehaviour {

    Camera cam;

    public float range;
    public float width;
    public float speed;

    public Material sonarEffectMaterial;

    float minDistance;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        minDistance = cam.nearClipPlane;

        StartCoroutine(scan());
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator scan()
    {
        bool scanComplete = false;
        float t = minDistance;

        while(!scanComplete)
        {
            cam.nearClipPlane = t;
            cam.farClipPlane = t + width;
            t += Time.deltaTime * speed;

            if(t >= range - width)
            {
                scanComplete = true;
            }
            yield return null;
        }

        StartCoroutine(scan());
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, sonarEffectMaterial);
    }
}
