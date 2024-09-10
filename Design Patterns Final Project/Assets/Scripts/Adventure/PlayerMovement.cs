using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform spawnPointTransform;

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
    private void Respawn()
    {
        transform.position = spawnPointTransform.position;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Respawn();
        }
    }
}
