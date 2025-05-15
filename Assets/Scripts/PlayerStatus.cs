using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public static PlayerStatus Instance { get; set; }

    //Health
    public float currentHealth;
    public float maxHealth;

    //Calories
    public float currentCalories;
    public float maxCalories;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    public GameObject playerBody;

    //Hydration
    public float currentHydrationPercent;
    public float maxHydrationPercent;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance= this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(DecreaseHydration());
    }
    IEnumerator DecreaseHydration()
    {
        while(true)
        {
            currentHydrationPercent -= 1;
            yield return new WaitForSeconds(20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition= playerBody.transform.position;

        if(distanceTravelled >= 5)
        {
            distanceTravelled= 0;
            currentCalories--;
        }
    }

    internal void setHealth(float maxHealth)
    {
        throw new NotImplementedException();
    }

    internal void setCalories(float maxCalories)
    {
        throw new NotImplementedException();
    }

    internal void setHydration(float maxHydration)
    {
        throw new NotImplementedException();
    }
}
