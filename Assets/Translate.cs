using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    // Start is called before the first frame update
    public Space space;
    public Vector3 translationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(translationSpeed * Time.deltaTime, space);

    }
}
