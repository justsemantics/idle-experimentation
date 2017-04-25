using UnityEngine;
using System.Collections;

public class wobble : MonoBehaviour {
    [SerializeField]
    float wobbleSpeed;

    [SerializeField]
    float wobbleAmount;

    [SerializeField]
    float wobbleAngle;

    [SerializeField]
    Vector3 wobbleDirection;

    [SerializeField]
    GameObject wobbleNormal;

    Vector3 initialPosition;
    Vector3 positionOffset;
    Vector3 eulers;

    float initAngle;
    float timeOffset;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        positionOffset = transform.position - wobbleNormal.transform.position;
        initAngle = transform.rotation.eulerAngles.z;
        timeOffset = Random.Range(0, 1 / wobbleSpeed);

        if (wobbleNormal)
        {
            wobbleDirection = wobbleNormal.transform.TransformVector(wobbleDirection);
        }
        else
        {
            eulers = transform.rotation.eulerAngles;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 translation = wobbleAmount * Mathf.Sin(wobbleSpeed * 2 * Mathf.PI * Time.fixedTime + timeOffset) * wobbleDirection;

        if (wobbleNormal)
        {
            eulers = wobbleNormal.transform.eulerAngles;
        }

        transform.rotation = Quaternion.Euler(eulers.x, initAngle + wobbleAngle * Mathf.Sin(wobbleSpeed * 2 * Mathf.PI * Time.fixedTime + timeOffset) + eulers.y, eulers.z);

        if (wobbleNormal)
            transform.position = wobbleNormal.transform.position + translation + positionOffset;
        else
            transform.position = translation + initialPosition;
	}
}
