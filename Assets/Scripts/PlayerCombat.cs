using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCombat : NetworkBehaviour
{
    public GameObject projectilePrefab;
    public float bulletVelocity = 50f;
    public AudioClip sfxFire;
    public Transform firePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire1()
    {
        Debug.Log("FIRINGSSS!");
        local_Shoot();
    }

    public void local_Shoot()
    {
        GameObject newBullet = Instantiate(projectilePrefab, firePos.position, firePos.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = firePos.TransformDirection(Vector3.forward * bulletVelocity);
        AudioSource.PlayClipAtPoint(sfxFire, firePos.position);
        ReqShootServerRPC(firePos.position, firePos.TransformDirection(Vector3.forward * bulletVelocity));
    }

    [ServerRpc]
    void ReqShootServerRPC(Vector3 shootPos,Vector3 bulletVel)
    {
        ImShootClientRPC(shootPos,bulletVel);
    }

    [ClientRpc]
    void ImShootClientRPC(Vector3 shootPos, Vector3 bulletVel)
    {
        if (!IsOwner)
        {
            GameObject newBullet = Instantiate(projectilePrefab, shootPos, Quaternion.Euler(bulletVel));
            newBullet.GetComponent<Rigidbody>().velocity = bulletVel;
            AudioSource.PlayClipAtPoint(sfxFire, firePos.position);
        }
    }
}
