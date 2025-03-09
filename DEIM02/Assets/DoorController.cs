using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Transform doorTransform;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpened = false;

    PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    public void ActivarGiro()
    {
        if(isOpened == false)
        {
            StartCoroutine(OpenDoor());

        }

    }
    

    public IEnumerator OpenDoor()
    {
        isOpened = true;
        Quaternion startRotation = doorTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0) * startRotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            doorTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        doorTransform.rotation = targetRotation;
    }
}