using UnityEngine;
using UnityEngine.UI;

public class AbrirURL : MonoBehaviour
{
        public string urlParaAbrir;

    void Start()
    {
       
        Button boton = GetComponent<Button>();
        if (boton != null)
        {
            
            boton.onClick.AddListener(AbrirPaginaWeb);
        }
        else
        {
            Debug.LogError("El script AbrirURL debe estar en un GameObject con componente Button.");
        }
    }

    void AbrirPaginaWeb()
    {
        if (!string.IsNullOrEmpty(urlParaAbrir))
        {
            
            Application.OpenURL(urlParaAbrir);
            Debug.Log("Abriendo: " + urlParaAbrir);
        }
    }
}