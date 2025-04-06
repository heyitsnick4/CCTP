using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject CamWorldPos;

    private bool ShouldFollow = false;

    private float PlayerY;
    private float CamXPos;

    // Start is called before the first frame update
    void Start()
    {
        CamXPos = CamWorldPos.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
       PlayerY = Player.transform.position.y;
       if(ShouldFollow)
        {
            Cam.transform.position = new Vector3(CamXPos, PlayerY,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShouldFollow = true;
        }
    }

    public void StopFollow()
    {
        ShouldFollow = false;
    }
}