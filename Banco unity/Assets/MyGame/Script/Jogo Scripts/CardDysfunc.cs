using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDysfunc : MonoBehaviour
{
    public GameObject cartaEx; 

    private void OnMouseEnter()
    {
        cartaEx.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnMouseExit()
    {
        cartaEx.GetComponent<SpriteRenderer>().sprite = null;
    }

}
