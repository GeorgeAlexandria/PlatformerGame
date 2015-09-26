using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform Target;
    private const float maxDistance = 3.0f;
    private Vector3 delta;
    private const int positionZ = -10;

    // Use this for initialization
    void Start()
    {
        delta = Target.position - Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Vector3.Lerp(transform.position, Target.position - delta, Time.deltaTime * maxDistance);
        temp.z = positionZ;
        Camera.main.transform.position = temp;
    }
}
