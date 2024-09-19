using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public float moveSpeed = .5f;
    public float verticalMoveSpeed = .5f;
    public float horizontalMoveSpeed = .5f;

    void Update()
    {
        if (OVRInput.GetActiveController() == OVRInput.Controller.LTouch ||
            OVRInput.GetActiveController() == OVRInput.Controller.RTouch)
        {
            Vector2 rightThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 movement = new Vector3(rightThumbstick.x, 0, rightThumbstick.y) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            if (OVRInput.Get(OVRInput.Button.One))
            {
                Debug.Log("Button One");
                transform.position += Vector3.up * verticalMoveSpeed * Time.deltaTime;
            }

            if (OVRInput.Get(OVRInput.Button.Two))
            {
                Debug.Log("Button Two");
                transform.position += Vector3.down * verticalMoveSpeed * Time.deltaTime;
            }
        }
    }
}
