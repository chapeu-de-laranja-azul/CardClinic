using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// o que foi utilizado nesse script 
using UnityEngine.UI;
using UnityEngine.EventSystems;

// utilizado para poder detectar eventos de mouse com elementos de GUI 
public class DiscartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Uma variavel para salvar a carta de amostra
    public GameObject cartaExpandida;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    /// <summary>
    ///  quando o cursor do mouse entrar dentro da area desse script
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        cartaExpandida.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;

        // fechando o painel do numero aleatorio do dado
        gm.ClosePainelNum();

    }

    /// <summary>
    ///  quando o cursor do mouse sair de dentro da area desse script
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        cartaExpandida.GetComponent<Image>().sprite = gm.imagemVazio;
    }

    /// <summary>
    ///  quando o mouse clicar dentro da area desse script
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        cartaExpandida.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;

    }

}
