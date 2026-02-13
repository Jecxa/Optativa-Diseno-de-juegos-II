using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegadorEscenas : MonoBehaviour
{
    public void IrAEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
        Debug.Log("Cambiando a escena: " + nombreEscena);
    }
}