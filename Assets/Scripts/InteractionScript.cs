using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    public float raycastRange = 3.0f; // Maximum distance for the raycast
    private Transform highlight;
    private RaycastHit raycastHit;

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, raycastRange))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Interactable"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }

                // Check for left or right mouse button clicks
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    Debug.Log("Left mouse button clicked on " + highlight.name);
                    if (highlight.gameObject.name == "startButton")
                    {
                        Debug.Log("Start button clicked");
                        LevelStartScript levelStartScript = GameObject.Find("Player").GetComponent<LevelStartScript>();
                        if (levelStartScript != null)
                        {
                            StartCoroutine(levelStartScript.StartLevel());
                        }
                    }
                    else if (highlight.gameObject.name == "Microwave_Oven")
                    {
                        Debug.Log("Microwave oven clicked");
                        Animator mikro = highlight.gameObject.GetComponentInChildren<Animator>();
                        mikro.SetBool("Nyitva", mikro.GetBool("Nyitva") ? false : true);
                    }
                    else if (highlight.gameObject.name == "Fork")
                    {
                        PickupScript pickupScript = GameObject.Find("Player").GetComponent<PickupScript>();
                        if (pickupScript != null)
                        {
                            pickupScript.ItemPickedUp(highlight.gameObject, "left");
                        }
                    }
                    else if (highlight.gameObject.name == "WholePlate")
                    {
                        PickupScript pickupScript = GameObject.Find("Player").GetComponent<PickupScript>();
                        if (pickupScript != null)
                        {
                            pickupScript.ItemPickedUp(highlight.gameObject, "left");
                        }
                    }


                }
                // TODO: Törölni ezt a faszba:
                Debug.Log(highlight.gameObject.name);
                if (Input.GetMouseButtonDown(1)) // Right mouse button
                {
                    Debug.Log("Right mouse button clicked on " + highlight.name);
                    // Add your right-click interaction logic here
                    if (highlight.gameObject.name == "startButton")
                    {
                        Debug.Log("Start button clicked");
                        LevelStartScript levelStartScript = GameObject.Find("Player").GetComponent<LevelStartScript>();
                        if (levelStartScript != null)
                        {
                            StartCoroutine(levelStartScript.StartLevel());
                        }
                    }
                    else if (highlight.gameObject.name == "Microwave_Oven")
                    {
                        Debug.Log("Microwave oven clicked");
                        Animator mikro = highlight.gameObject.GetComponentInChildren<Animator>();
                        mikro.SetBool("Nyitva", mikro.GetBool("Nyitva") ? false : true);
                    }
                    else if (highlight.gameObject.name == "Fork")
                    {
                        PickupScript pickupScript = GameObject.Find("Player").GetComponent<PickupScript>();
                        if (pickupScript != null)
                        {
                            pickupScript.ItemPickedUp(highlight.gameObject, "right");
                        }
                    }
                    else if (highlight.gameObject.name == "WholePlate")
                    {
                        PickupScript pickupScript = GameObject.Find("Player").GetComponent<PickupScript>();
                        if (pickupScript != null)
                        {
                            pickupScript.ItemPickedUp(highlight.gameObject, "right");
                        }
                    }
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
}