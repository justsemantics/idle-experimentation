using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

    [SerializeField]
    Vector3 rotation;

    Quaternion qRotation;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    float driftAmount;

    [SerializeField]
    float maxDrift;

    Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        qRotation = Quaternion.AngleAxis(rotateSpeed * Time.fixedDeltaTime, rotation);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.rotation *= qRotation;
        //float x = Mathf.Clamp(transform.position.x + Random.Range(-driftAmount, driftAmount), initialPosition.x - maxDrift, initialPosition.x + maxDrift);
        //float y = Mathf.Clamp(transform.position.y + Random.Range(-driftAmount, driftAmount), initialPosition.y - maxDrift, initialPosition.y + maxDrift);
        //float z = Mathf.Clamp(transform.position.z + Random.Range(-driftAmount, driftAmount), initialPosition.z - maxDrift, initialPosition.z + maxDrift);

        //transform.position = new Vector3(x, y, z);
	}
}
