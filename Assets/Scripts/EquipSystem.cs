using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();


    public GameObject numbersHolder;
    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject toolHolder;
    public GameObject selectedItemModel;

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


    private void Start()
    {
        PopulateSlotList();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7); 
        }
    }

    private void SelectQuickSlot(int number)
    {   if(CheckIfSlotIsFull(number) == true)
        {
          if(selectedNumber !=number)
            {
                selectedNumber = number;

                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }
                selectedItem = getSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquippedModel(selectedItem);

                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                }
                Text toBeChanged = numbersHolder.transform.Find("Number" + number).transform.Find("Text").GetComponent<Text>();
                toBeChanged.color = Color.white;
            }
            else
            {
                selectedNumber = -1;
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                if(selectedItemModel!= null)
                {
                    DestroyImmediate(selectedItemModel.gameObject); 
                    selectedItemModel = null;
                }

                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                }
            }
        }
        
    }

    private void SetEquippedModel(GameObject selectedItem)
    {

        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"), new Vector3(1.1f, 0.8f, 1.5f), Quaternion.Euler(0, 160f, 0));
        selectedItemModel.transform.SetParent(toolHolder.transform, false); 

    }

    private GameObject getSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    private bool CheckIfSlotIsFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber -1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting clean name
        string cleanName = itemToEquip.name.Replace("(Clone)", "");
        // Adding item to list
        InventorySystem.Instance.itemList.Add(cleanName);

        InventorySystem.Instance.RecalculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

