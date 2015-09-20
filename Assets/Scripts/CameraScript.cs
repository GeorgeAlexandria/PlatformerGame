using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target;
    private Vector3 delta;

    // Use this for initialization
    void Start()
    {
        delta = Camera.main.transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Camera.main.transform.position = target.position + delta;
        }
    }
}