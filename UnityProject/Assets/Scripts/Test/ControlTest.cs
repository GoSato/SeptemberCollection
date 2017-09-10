using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    [ContextMenu("reset")]
    public void Reset()
    {
        gameObject.transform.forward = Camera.main.transform.forward;
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }
}
