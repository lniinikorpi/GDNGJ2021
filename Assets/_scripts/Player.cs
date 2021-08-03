using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public Transform mainObject;

    Vector2 movement = new Vector2();
    int movementDelta = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movement != new Vector2())
        {
            mainObject.position += new Vector3(movement.x / movementDelta, movement.y / movementDelta, 0);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnMouseMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        Vector3 pos = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 dir = new Vector3(v.x, v.y, 0) - new Vector3(pos.x, pos.y, 0);
        Vector3 newDir = Vector3.RotateTowards(Vector3.forward, dir, 1, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
