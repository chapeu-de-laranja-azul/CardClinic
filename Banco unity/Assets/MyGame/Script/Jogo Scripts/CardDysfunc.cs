using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDysfunc : MonoBehaviour
{
    // salvando a carta expandida
    public GameObject cartaEx; 

    /// <summary>
    /// quando o cursor do mouse entrar dentro do collaider desse script
    /// </summary>
    private void OnMouseEnter()
    {
        // alterando a imagem da carta expandida para a da carta
        cartaEx.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// quando o cursor do mouse sair de dentro do collaider desse script
    /// </summary>
    private void OnMouseExit()
    {
        // alterando a imagem da carta expandida para vazia
        cartaEx.GetComponent<SpriteRenderer>().sprite = null;
    }

}
