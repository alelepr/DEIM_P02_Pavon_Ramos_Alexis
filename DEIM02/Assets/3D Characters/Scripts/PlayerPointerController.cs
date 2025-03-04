using UnityEngine;

public class PlayerPointerController : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] private float maxRayCastDistance;
    [SerializeField] private LayerMask layerMask;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo,  maxRayCastDistance, layerMask))
        {
            transform.position = hitInfo.point;
        }
            
            
        
    }
}
