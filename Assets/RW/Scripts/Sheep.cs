using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    public float dropDestroyDelay;

    private bool hitByHay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;
    public GameObject ExplosionPrefab;

    public float heartOffset; 
    public GameObject heartPrefab;
    private bool hasDropped = false;



    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        if (sheepSpawner != null)
        {
            sheepSpawner.RemoveSheepFromList(gameObject);
        }

        hitByHay = true;
        runSpeed = 0f;

        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, gotHayDestroyDelay);

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; 
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        SoundManager.Instance.PlaySheepHitClip();
        GameStateManager.Instance.SavedSheep();




    }

    private void Drop()
    {
        if (hasDropped) return;
        hasDropped = true;

        GameStateManager.Instance.DroppedSheep();

        if (sheepSpawner != null)
        {
            sheepSpawner.RemoveSheepFromList(gameObject);
            SoundManager.Instance.PlaySheepDroppedClip();
        }

        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;

        Destroy(gameObject, dropDestroyDelay);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }

    }
}

