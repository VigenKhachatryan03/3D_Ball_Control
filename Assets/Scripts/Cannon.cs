using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform cannonHead;
    [SerializeField] private Transform cannonTip;
    [SerializeField] private float shootingCoolDown = 3f;
    [SerializeField] private float laserPower = 100f;

    private bool isPlayerInRange = false;
    private GameObject player;
    private float timeLeftShoot = 0;
    private LineRenderer cannonLaser;

    void Start()
    {
        cannonLaser = GetComponent<LineRenderer>();
        cannonLaser.sharedMaterial.color = Color.yellow;
        cannonLaser.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        timeLeftShoot = shootingCoolDown;
    }

    void Update()
    {
        if(isPlayerInRange)
        {
            cannonHead.transform.LookAt(player.transform);
            cannonLaser.SetPosition(0, cannonTip.transform.position);
            cannonLaser.SetPosition(1, player.transform.position);
            timeLeftShoot -= Time.deltaTime;
        }

        if (timeLeftShoot <= shootingCoolDown * 0.5)
        {
            cannonLaser.material.color = Color.red;
        }

        if (timeLeftShoot <= 0)
        {
            Vector3 directionToPushBack = player.transform.position - cannonTip.transform.position;

            player.GetComponent<Rigidbody>().AddForce(directionToPushBack * laserPower, ForceMode.Impulse);
            timeLeftShoot = shootingCoolDown;
            cannonLaser.sharedMaterial.color = Color.green;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            cannonLaser.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            cannonLaser.enabled = false;

            timeLeftShoot = shootingCoolDown;
            cannonLaser.sharedMaterial.color = Color.yellow;
        }
    }
}
