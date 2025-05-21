using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public bool isOpen;
    public GameObject itemInfoIU;

    public List<GameObject> slotList = new List<GameObject>();
    public List<String> itemList = new List<String>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public GameObject pickUpAlert;
    public Text pickUpName;
    public Image pickUpImage;

    public bool isFull;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList();
        Cursor.visible = false;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            isOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {

            inventoryScreenUI.SetActive(false);
            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            isOpen = false;
        }
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    public void AddToInventory(string itemName)
    {
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd =Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);
            RecalculateList();
            CraftingSystem.Instance.RefreshNeededItems();
            TriggerPickUpPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);
            StartCoroutine(ClosePickUpAlert());
    }

    void TriggerPickUpPopUp(string itemName, Sprite itemSprite)
    {
        pickUpAlert.SetActive(true);
        pickUpName.text= itemName;
        pickUpImage.sprite = itemSprite;
    }
    public IEnumerator ClosePickUpAlert()
    {
        yield return new WaitForSeconds(2f);

        pickUpAlert.SetActive(false);

    }
    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount> 0)
            {
                counter++;
            }

        }


        if (counter == 28)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }

        }
        return new GameObject();
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count -1; i>= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }
        RecalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void RecalculateList()
    {
        itemList.Clear();

        foreach  (GameObject slot in slotList)
        {

            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str = "(Clone)";
                string result = name.Replace(str, "");
                itemList.Add(result);
            }
        }
    }
}