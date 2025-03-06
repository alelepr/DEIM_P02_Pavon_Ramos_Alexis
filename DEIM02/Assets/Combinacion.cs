using UnityEngine;
using UnityEngine.UI;

public class Combinacion : MonoBehaviour
{
    // Variables para el puzzle
    [SerializeField] public GameObject cajaFuerte;  // Caja fuerte en el juego
    [SerializeField] public GameObject canvasCombinacion;  // Canvas que contiene el panel con los botones de la combinación
    [SerializeField] public GameObject linterna;  // El objeto (linterna) que el jugador obtendrá al abrir la caja fuerte
    [SerializeField] public GameObject tapaCofre;

    private string combinacionCorrecta = "4586";  // La combinación correcta
    private string combinacionIngresada = "";  // La combinación que el jugador ingresa
    private bool combinacionCorrectaFlag = false;  // Verifica si la combinación ingresada es correcta

    void Start()
    {
        // Asegurarse de que la linterna esté desactivada al inicio
        linterna.SetActive(false);
        // Asegurarse de que el canvas de la combinación esté desactivado al inicio
        canvasCombinacion.SetActive(false);
    }

    // Este método se llama cuando el jugador interactúa con la caja fuerte
    public void AbrirCajaFuerte()
    {
        // Muestra el canvas de la combinación
        canvasCombinacion.SetActive(true);
    }

    // Método para ingresar el número en la combinación
    public void IngresarNumero(string numero)
    {
        // Añadir el número al final de la combinación ingresada
        combinacionIngresada += numero;
        Debug.Log("Combinación Ingresada: " + combinacionIngresada);
    }

    // Método para verificar si la combinación ingresada es correcta
    public void VerificarCombinacion()
    {
        // Verifica si la combinación es correcta
        if (combinacionIngresada == combinacionCorrecta)
        {
            combinacionCorrectaFlag = true;
            AbrirCajaFuerteAccion();  // Abre la caja fuerte
        }
        else
        {
            // Si la combinación es incorrecta, reinicia el ingreso
            combinacionIngresada = "";
            Debug.Log("Combinación incorrecta.");
        }
    }

    // Acciones para abrir la caja fuerte cuando la combinación es correcta
    private void AbrirCajaFuerteAccion()
    {
        if (combinacionCorrectaFlag)
        {
            // Desactivar el canvas de la combinación
            canvasCombinacion.SetActive(false);

            // Aquí activamos la linterna (o cualquier objeto que el jugador deba obtener)
            linterna.SetActive(true);

            // Si deseas reproducir una animación de la caja fuerte abriéndose, también podrías hacerla aquí.
            Debug.Log("¡Combinación correcta! La caja fuerte se ha abierto.");
        }
    }

    // Método para cancelar el ingreso de la combinación (puedes agregar un botón para esto)
    public void CancelarIngreso()
    {
        combinacionIngresada = "";  // Reinicia la combinación ingresada
        Debug.Log("Ingreso cancelado.");
    }
}