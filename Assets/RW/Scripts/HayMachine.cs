using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public Vector3 translationSpeed;
    public float limitX;
    public GameObject heyBalePrefab;

    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;

    public Transform modelParent;

    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    void Start()
    {
        LoadModel(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= limitX)
        {
            transform.Translate(translationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -limitX)
        {
            transform.Translate(-translationSpeed * Time.deltaTime);
        }

        UpdateShooting();
    }

    private void LoadModel()
    {
        if (modelParent.childCount > 0)
        {
            Destroy(modelParent.GetChild(0).gameObject);
        }

        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }

    private void ShootHay()
    {
        Instantiate(heyBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }
}


