using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject DoorToDestroy;

    private bool CanOpen = false;

    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Dash.DoorInput.performed += ctx => DoorCanOpen();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(CanOpen)
            {
                Destroy(DoorToDestroy);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanOpen = false;
        }
    }

    void OnEnable()
    {
        controls.Dash.Enable();
    }

    void OnDisable()
    {
        controls.Dash.Disable();
    }

    void DoorCanOpen()
    {
        if (CanOpen)
        {
            Destroy(DoorToDestroy);
        }
    }
}
