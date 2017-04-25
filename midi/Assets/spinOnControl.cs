using UnityEngine;
using System.Collections;

public class spinOnControl : MonoBehaviour {

    float theta = 0;

    float speed = 0;

    [SerializeField]
    float topSpeed = 1;

    Quaternion initialRotation;

	// Use this for initialization
	void Start () {
        initialRotation = transform.rotation;
	}

	
	// Update is called once per frame
	void Update () {
        speed = MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch1, 7) * topSpeed;

        theta += speed * Time.deltaTime;

        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(0, theta, 0);
        newRotation = newRotation * initialRotation;

        transform.rotation = newRotation;
	}
}
