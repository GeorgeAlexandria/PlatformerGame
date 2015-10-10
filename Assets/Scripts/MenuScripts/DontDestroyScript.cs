using UnityEngine;
using System.Collections;

public class DontDestroyScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
