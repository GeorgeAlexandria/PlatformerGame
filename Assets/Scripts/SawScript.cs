using UnityEngine;
using System.Collections;

public class SawScript : MonoBehaviour
{
    public float SpeedRotation = 3;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, SpeedRotation));
    }
}
