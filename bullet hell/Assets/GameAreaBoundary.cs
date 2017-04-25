using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaBoundary : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
