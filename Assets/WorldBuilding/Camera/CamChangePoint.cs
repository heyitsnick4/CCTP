using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangePoint : MonoBehaviour
{
    [SerializeField] private GameObject CamMovePoint;
    [SerializeField] private GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cam.transform.position = CamMovePoint.transform.position;
        }
    }
}
