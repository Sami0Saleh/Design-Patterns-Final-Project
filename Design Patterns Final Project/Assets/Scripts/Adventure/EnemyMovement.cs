using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float _distance;

    public float moveSpeed = 2f;

    void Update()
    {
        _distance = Vector3.Distance(transform.position, player.position);
        if (_distance < 70)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        
    }
}