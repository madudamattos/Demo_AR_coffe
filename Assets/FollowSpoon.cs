using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpoon : MonoBehaviour
{
    public Transform reference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = reference.position;
    }
}
