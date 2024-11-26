using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Rigidbody rb;
    private bool isPlayerInRange = false;
    private GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    private void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            Vector3 targetDirection = player.transform.position - transform.position;
            rb.AddForce(targetDirection * speed * Time.deltaTime, ForceMode.VelocityChange);
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = 0f;
            rb.velocity = newVelocity;
        }
    }
}
