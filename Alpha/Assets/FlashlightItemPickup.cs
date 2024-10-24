using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightItemPickup : MonoBehaviour
{
    public GameObject flashlightitem;
    public GameObject playerFlashlight;
    public GameObject textUI;
    private bool happened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!happened)
            {
                textUI.SetActive(true);
                playerFlashlight.SetActive(true);
                flashlightitem.SetActive(false);
            }

        }
    }


}
