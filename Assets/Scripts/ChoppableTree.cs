using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{

    // Start is called before the first frame update


    public bool playerInRange;
    public bool canBeChopped;
    public float treeMaxHealth;
    public float treeHealth;


    void Start()
    {
        treeHealth = treeMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Artýk kesilemez
            canBeChopped = false;

            // Hâlâ seçiliyse seçimi temizle
            if (SelectionManager.Instance != null &&
                SelectionManager.Instance.selectedTree == gameObject)
            {
                SelectionManager.Instance.selectedTree = null;
                SelectionManager.Instance.chopHolder.SetActive(false);
            }
        }
    }


    public void GetHit()
    {
        treeHealth--;
    }

    private void Update()
    {
        if(canBeChopped)
        {
            GlobalState.Instance.resourceHealth= treeHealth;
            GlobalState.Instance.resourceMaxHealth= treeMaxHealth;
        }
    }
}
