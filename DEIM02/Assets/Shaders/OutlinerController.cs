using UnityEngine;

public class OutlinePulse : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial; // Asigna el material con el slider en el Inspector

    private const string OutlineProperty = "_OutlineThickness"; // Nombre exacto de la propiedad en el Shader
    private const float MinOutline = 0.008f;
    private const float MaxOutline = 0.016f;
    private const float PulseSpeed = 2f;

    public bool isActive;

    private void Start()
    {
        outlineMaterial.SetFloat(OutlineProperty, 0f);
    }
    void Update()
    {
        if (isActive && outlineMaterial != null)
        {
            float newThickness = Mathf.Lerp(MinOutline, MaxOutline, Mathf.PingPong(Time.time * PulseSpeed, 1));
            outlineMaterial.SetFloat(OutlineProperty, newThickness);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
        }
    } 

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
            outlineMaterial.SetFloat(OutlineProperty, 0f); // Reinicia el grosor al mínimo
        }
    }
}
