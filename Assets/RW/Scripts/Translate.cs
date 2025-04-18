using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 translationSpeed;
    public Space space;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(translationSpeed * Time.deltaTime, space);
        
    }
}
