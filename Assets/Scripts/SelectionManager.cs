using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }
    public GameObject interactionInfoUI;
    public float interactionRange = 5f;
    Text interactionText;

    public Image centerDot;
    public Image handIcon;
    public bool handIsVisible;
    public GameObject selectedTree;
    public GameObject chopHolder;



    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        interactionText = interactionInfoUI.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, interactionRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            var selectionTransform = hit.transform;

            ChoppableTree choppableTree = selectionTransform.GetComponent<ChoppableTree>();

            if (choppableTree && choppableTree.playerInRange)
            {
                choppableTree.canBeChopped= true;
                selectedTree = choppableTree.gameObject;
                chopHolder.gameObject.SetActive(true);
            }
            else   
            {
                // Sadece seçili bir aðaç varsa temizle
                if (selectedTree != null)
                {
                    selectedTree.GetComponent<ChoppableTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.SetActive(false);   // aðaçla ilgili UI
                }
            }


            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interactionText.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interactionInfoUI.SetActive(true);
                //Debug.Log("1. secenek oldu");
                if (selectionTransform.CompareTag("Pickable"))
                {
                    centerDot.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                    handIsVisible= true;
                }
                else
                {
                    centerDot.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);   
                    handIsVisible= false;
                }
            }
            else
            {
                interactionInfoUI.SetActive(false);
                centerDot.gameObject.SetActive(true);
                handIcon.gameObject.SetActive(false);
                handIsVisible= false;
                //Debug.Log("2. secenek oldu");
            }
        }
        else
        {
            if (selectedTree != null)
            {
                selectedTree.GetComponent<ChoppableTree>().canBeChopped = false;
                selectedTree = null;
                chopHolder.SetActive(false);   // yalnýzca aðaçla ilgili UI
            }
            interactionInfoUI.SetActive(false);
            centerDot.gameObject.SetActive(true);
            handIcon.gameObject.SetActive(false);
            handIsVisible= false;
            //Debug.Log("3. secenek oldu");

        }
    }

    public void DisableSelection()
    {
        handIcon.enabled = false;
        centerDot.enabled = false;
        interactionInfoUI.SetActive(false);


    }

    public void EnableSelection()
    {
        handIcon.enabled =true;
        centerDot.enabled =true;
        interactionInfoUI.SetActive(true) ;
    }
}
