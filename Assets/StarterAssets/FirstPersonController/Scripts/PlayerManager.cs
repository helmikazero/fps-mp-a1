using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerManager : NetworkBehaviour
{
    public Transform thisCameraRoot;

    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        

        if (IsOwner)
        {
            Transform camFollowTarget = GameObject.FindGameObjectWithTag("CinemachineTarget").transform;
            camFollowTarget.parent = thisCameraRoot;
            camFollowTarget.localPosition = Vector3.zero;
            camFollowTarget.localRotation = Quaternion.identity;

            GameObject newSpawnPoints = GameObject.Find("SpawnPoints");
            Transform[] newSpawnPointIndiv = new Transform[newSpawnPoints.transform.childCount];
            for (int i = 0; i < newSpawnPointIndiv.Length; i++)
            {

                newSpawnPointIndiv[i] = newSpawnPoints.transform.GetChild(i);
                Debug.Log(newSpawnPointIndiv[i].gameObject.name);
            }

            spawnPoints = newSpawnPointIndiv;

            transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }
        else
        {
            GetComponent<CharacterController>().enabled = false;
            GetComponent<StarterAssets.FirstPersonController>().enabled = false;
            GetComponent<BasicRigidBodyPush>().enabled = false;
            GetComponent<StarterAssets.StarterAssetsInputs>().enabled = false;
            GetComponent<PlayerInput>().enabled = false;
        }

        transform.GetChild(0).transform.localPosition = new Vector3(0, 1.375f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
