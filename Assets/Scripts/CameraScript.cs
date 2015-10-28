using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private GameObject target;
    private const float maxDistance = 3.0f;
    private Vector3 delta;
    private const int positionZ = -10;

    void Awake()
    {
        target = ApplicationManager.hero.gameObject;
    }

    // Use this for initialization
    void Start()
    {
        delta = target.GetComponent<Rigidbody2D>().transform.position - Camera.main.transform.position;
    }

    void OnLevelWasLoaded(int level)
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Vector3.Lerp(transform.position, target.transform.position - delta, Time.deltaTime * maxDistance);
        temp.z = positionZ;
        Camera.main.transform.position = temp;
    }
}
