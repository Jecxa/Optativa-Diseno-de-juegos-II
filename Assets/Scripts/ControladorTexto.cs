using UnityEngine;
using TMPro;

public class ControladorTexto : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float incrementoTamano = 5f;

    public void AumentarTamano()
    {
        texto.fontSize += incrementoTamano;
        Debug.Log("Tamaño aumentado: " + texto.fontSize);
    }

    public void DisminuirTamano()
    {
        texto.fontSize -= incrementoTamano;
        Debug.Log("Tamaño disminuido: " + texto.fontSize);
    }

    public void ColorRojo()
    {
        texto.color = Color.red;
    }

    public void ColorAzul()
    {
        texto.color = Color.blue;
    }
}