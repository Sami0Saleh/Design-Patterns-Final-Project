using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _spawnPointTransform;

    [SerializeField] private Transform _camTransform;
    [SerializeField] private float horizontalSpeed = 2.0f;
    [SerializeField] private float verticalSpeed = 2.0f;

    private bool _gamePaused = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        MovePlayer();

        RotatePlayerAndCamera();

        HandlePause();
    }

    private void MovePlayer()
    {
        if (_camTransform != null)
        {
            Vector3 forward = _camTransform.forward;
            Vector3 right = _camTransform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            float moveX = Input.GetAxis("Horizontal") * _moveSpeed;
            float moveZ = Input.GetAxis("Vertical") * _moveSpeed;

            Vector3 moveDirection = (forward * moveZ) + (right * moveX);

            _rb.velocity = new Vector3(moveDirection.x, _rb.velocity.y, moveDirection.z);
        }
    }

    private void RotatePlayerAndCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        transform.Rotate(0, mouseX, 0);

        if (_camTransform != null)
        {
            _camTransform.position = transform.position - transform.forward * 10f + Vector3.up * 5f;
            _camTransform.LookAt(transform.position + Vector3.up * 2f); 
        }
    }

    private void HandlePause()
    {
        if (!_gamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.Pause();
            _gamePaused = true;
        }
        else if (_gamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.Continue();
            _gamePaused = false;
        }
    }

    private void Respawn()
    {
        transform.position = _spawnPointTransform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Respawn();
        }
    }
}