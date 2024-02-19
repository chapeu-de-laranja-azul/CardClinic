using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSom : MonoBehaviour
{
    private bool estadoSom = true;
    [SerializeField] private AudioSource fundoMusical;

    public void LiagarDesligarSom()
    {
        estadoSom = !estadoSom;
        fundoMusical.enabled = estadoSom;
    }

    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value;
    }

}
