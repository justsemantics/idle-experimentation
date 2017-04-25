using UnityEngine;
using System.Collections;

public class animateTextureOffset : MonoBehaviour {

    [SerializeField]
    string textureToAnimate;
    [SerializeField]
    float moveSpeedX, moveSpeedY;
    [SerializeField]
    bool sine = false;
    [SerializeField]
    bool stutter = false;
    [SerializeField]
    float gateTime = 1;
    float lastUpdate = 0;


    Vector2 offset = new Vector2(0, 0);

    Material mat;

	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        offset.x += moveSpeedX * Time.deltaTime;
        offset.y += moveSpeedY * Time.deltaTime;

        if (stutter)
        {
            if (Time.fixedTime - lastUpdate >= gateTime)
            {
                lastUpdate = Time.fixedTime;
                mat.SetTextureOffset(textureToAnimate, offset);
            }
        }
        else if (sine)
        {
            if(Time.fixedTime - lastUpdate >= gateTime)
            {
                lastUpdate = Time.fixedTime;
            }
            float sinT = Mathf.Pow( Mathf.Sin((Time.fixedTime - lastUpdate) / gateTime * 2 * Mathf.PI), 3);
            offset.x = moveSpeedX * sinT;
            offset.y = moveSpeedY * sinT;

            mat.SetTextureOffset(textureToAnimate, offset);
        }
        else
        {
            //Debug.Log(offset.x + ", " + offset.y);
            mat.SetTextureOffset(textureToAnimate, offset);
        }

	}
}
