using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{
    public GameObject StonePrefab;

    public float launchTime; 
    public float launchCounter;
    public bool isLaunchCold;
    private void Awake()
    {
        launchTime = 5f;
        launchCounter = launchTime;
    }
    private void LaunchTimeCounter()
    {
        if (isLaunchCold)
        {
            launchCounter -= Time.deltaTime;
            if (launchCounter <= 0f)
            {
                isLaunchCold = false;
                launchCounter = launchTime;
               
            }
        }
    }
    private void Update()
    {
        LaunchTimeCounter();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLaunchCold)
            {
                Instantiate(StonePrefab, transform.transform.position,Quaternion.identity);
                isLaunchCold = true;
            }
           
        }
    }
    
}
