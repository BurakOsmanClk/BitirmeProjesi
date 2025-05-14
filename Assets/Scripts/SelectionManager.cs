using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionManager : MonoBehaviour
{
    public GameObject interactionInfoUI;
    public float interactionRange = 5f;
    Text interactionText;

    public Image centerDot;
    public Image handIcon;



    void Start()
    {
        interactionText = interactionInfoUI.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionRange) )
        {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interactionText.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interactionInfoUI.SetActive(true);
                //Debug.Log("1. secenek oldu");
                if (selectionTransform.CompareTag("Pickable"))
                {
                    centerDot.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                }
                else
                {
                    centerDot.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);   
                }
            }
            else
            {
                interactionInfoUI.SetActive(false);
                centerDot.gameObject.SetActive(true);
                handIcon.gameObject.SetActive(false);
                //Debug.Log("2. secenek oldu");
            }
        }
        else
        {
            interactionInfoUI.SetActive(false);
            centerDot.gameObject.SetActive(true);
            handIcon.gameObject.SetActive(false);
            //Debug.Log("3. secenek oldu");
        }
    }
}
