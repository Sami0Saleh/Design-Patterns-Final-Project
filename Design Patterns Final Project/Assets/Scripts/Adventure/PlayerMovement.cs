using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _spawnPointTransform;

    public Vector3 CamPosition;
    [SerializeField] private Transform _camTransform;

    private bool _gamePaused = false;
    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * _moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * _moveSpeed;
        _rb.velocity = new Vector3(moveX, _rb.velocity.y, moveZ);
        if (_camTransform != null)
        {
            _camTransform.position = transform.position + CamPosition;
        }
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
        if (collision.gameObject.tag == "Enemy")
        {
            Respawn();
        }
    }
}
