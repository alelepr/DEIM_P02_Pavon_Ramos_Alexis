using UnityEngine;

public class Inventario : MonoBehaviour
{
    public bool objectGrabbed;
    [SerializeField] public Transform objectParent;
    [SerializeField] public GameObject luz;

    private GameObject objectInHand;
    public Collider colliderDelObjectoTrigger;

    [SerializeField] private PlayerController playerController; // Referencia manual

    void Start()
    {
        objectGrabbed = false;
        luz.SetActive(false);

        if (playerController == null)
        {
            Debug.LogError("No se ha asignado PlayerController en el Inspector.");
        }
    }

    void Update()
    {
        if (colliderDelObjectoTrigger != null)
        {
            OnTriggerCandle(colliderDelObjectoTrigger);
        }
    }

    private void OnTriggerCandle(Collider other)
    {
        if (playerController != null && playerController.chestOpened)
        {
            if (Input.GetKeyDown(KeyCode.F) && !objectGrabbed && other.gameObject.CompareTag("Candle"))
            {
                objectGrabbed = true;
                objectInHand = other.gameObject;
                objectInHand.transform.SetParent(objectParent);
                objectInHand.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                luz.SetActive(true);

                foreach (Collider c in objectInHand.GetComponents<Collider>())
                {
                    c.enabled = false;
                }
            }
        }
        else
        {
            Debug.Log("No puedes recoger la vela hasta que el cofre esté abierto.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectGrabbed && other.CompareTag("Candle"))
        {
            colliderDelObjectoTrigger = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliderDelObjectoTrigger == other)
        {
            colliderDelObjectoTrigger = null;
        }
    }
}



/*
public bool objectGrabbed;
//[SerializeField] public GameObject item;
[SerializeField] public Transform objectParent;

private GameObject grabbedObject;

private GameObject objectInHand;

public Collider colliderDelObjectoTrigger;

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{
    objectGrabbed = false;

}

// Update is called once per frame
void Update()
{

    if (colliderDelObjectoTrigger != null)
    {
        OnTriggerBotella(colliderDelObjectoTrigger);

    }
}

public static void GrabObject()
{


}

private void OnTriggerBotella(Collider other)

{


        if (Input.GetKeyDown(KeyCode.F))
        {


            if (objectInHand != null)
            {
                objectInHand.transform.SetParent(null);
                objectInHand.transform.SetPositionAndRotation(other.gameObject.transform.position, other.gameObject.transform.rotation);
                Debug.Log("Suelta");
                foreach (Collider c in objectInHand.GetComponents<Collider>())
                {
                    c.enabled = true;
                }

            }

            objectGrabbed = true;
            other.gameObject.transform.SetParent(objectParent);
            switch (other.gameObject.tag)
            {
                case "Bottle":
                other.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0f, -0.108f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));

                break;


                case "Book":
                other.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(0f, -90f, 0f)));

                break;

            }
            objectInHand= other.gameObject;
            foreach (Collider c in objectInHand.GetComponents<Collider>())
            {
                c.enabled = false;
            }
        }



}
private void OnTriggerEnter(Collider other)
{
    colliderDelObjectoTrigger = other;
}
private void OnTriggerExit(Collider other)
{
    colliderDelObjectoTrigger = null;

}*/


