using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {

    [SerializeField]
    GameObject NodePrefab;

    [SerializeField]
    int size;

    [SerializeField]
    float spacing;

    List<Wave> waves;

    List<Node> nodes;

	// Use this for initialization
	void Start () {
        Init();
        LookupCurve myCurve = new LookupCurve(100);
        waves = new List<Wave>();
        waves.Add(new Wave(new Vector3(-5, 0, -8), 1f, myCurve));
        //waves.Add(new Wave(new Vector3(10, 0, 10), 2f));

        foreach(Wave wave in waves)
        {
            wave.PrecalculateDistances(nodes);
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach(Wave wave in waves)
        {
            doWave(wave);
        }
	}

    void doWave(Wave wave)
    {
        wave.Update(Time.deltaTime);
        //float radiusSq = wave.Radius * wave.Radius;
        //foreach(Node currentNode in nodes)
        //{
        //    if (!currentNode.Activated)
        //    {
        //        float distSq = (currentNode.transform.position - wave.Origin).sqrMagnitude;
        //        if(distSq < radiusSq)
        //        {
        //            currentNode.Activate();
        //        }
        //    }
        //}
    }

    [ContextMenu("Init")]
    public void Init()
    {
        foreach(Node node in GetComponentsInChildren<Node>())
        {
            GameObject.Destroy(node.gameObject);
        }
        nodes = new List<Node>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject nodeGO = Instantiate(NodePrefab);
                nodeGO.transform.SetParent(this.transform, false);
                nodeGO.transform.localPosition = new Vector3(spacing * (i - size / 2), 0, spacing * (j - size / 2));
                nodeGO.transform.localRotation = Quaternion.identity;

                Node node = nodeGO.GetComponent<Node>();

                nodes.Add(node);
            }
        }
    }
}

public class LookupCurve
{
    int resolution;
    float[] Values;
    float inc;
    public LookupCurve(int _resolution)
    {
        resolution = _resolution;
        Values = new float[resolution];

        inc = (float)1 / (float)resolution;
        Debug.Log(inc);

        for(int i = 0; i < resolution; i++)
        {
            Values[i] = Mathf.Sin(i * inc * Mathf.PI * 2);
        }
    }

    public float GetValueAt(float input)
    {
        input = (input % 1) * resolution;
        return Values[Mathf.FloorToInt(input)];
    }
}

public class Wave
{
    Vector3 origin;
    public Vector3 Origin
    {
        get { return origin; }
        private set { origin = value; }
    }

    Node[] nodes;
    float[] distances;
    LookupCurve curve;

    float radius;
    public float Radius
    {
        get { return radius; }
        private set
        {
            radius = value;
        }
    }
    float speed;

    public Wave(Vector3 _Origin, float _speed, LookupCurve _curve)
    {
        Origin = _Origin;
        speed = _speed;
        Radius = 0;
        curve = _curve;
    }

    public void Update(float deltaTime)
    {
        Radius += deltaTime * speed;

        for(int i = 0; i < nodes.Length; i++)
        { 
            if (distances[i] < Radius)
            {
                //Vector3 pos = nodes[i].transform.localPosition;
                float z = curve.GetValueAt((Radius - distances[i])*.1f);
                //nodes[i].transform.localPosition = new Vector3(pos.x, pos.y, z);
                nodes[i].SetPosition(z);
            }
        }
    }

    public void PrecalculateDistances(List<Node> Nodes)
    {

        nodes = new Node[Nodes.Count];
        distances = new float[Nodes.Count];

        int i = 0;
        foreach(Node node in Nodes)
        {
            float d = (node.transform.position - Origin).magnitude;
            nodes[i] = node;
            distances[i] = d;
            i++;
        }
    }
}
