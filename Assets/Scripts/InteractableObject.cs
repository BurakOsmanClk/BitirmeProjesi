using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange = false;
    public float detectionRange = 5f;
    // playerInRange singleton ile yapýlabilir ancak bu kodda ekstra yük bindirse de raycast sistemini tekrar kullandým.
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        DetectPlayerLooking();

        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full");
            }

        }
    }

    public string GetItemName()
    {
        return ItemName;
    }

    void DetectPlayerLooking()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 5f mesafe içinde bu nesnenin collider'ýna ýþýn çarpýyorsa
        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.transform == transform)
            {
                playerInRange = true;
                return;
            }
        }

        playerInRange = false;
    }


}
