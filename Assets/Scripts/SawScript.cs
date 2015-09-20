using UnityEngine;
using System.Collections;

public class SawScript : MonoBehaviour
{
    public float SpeedRotation = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, SpeedRotation));
    }
}
