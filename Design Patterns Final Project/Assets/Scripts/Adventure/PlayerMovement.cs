using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    public Vector3 CamPosition;
    [SerializeField] private Transform _camTransform;

    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);
        if (_camTransform != null)
        {
            _camTransform.position = transform.position + CamPosition;
        }
    }
}
