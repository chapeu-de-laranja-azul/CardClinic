using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // variavel para evitar do jogador clicar varias vezes na mesma carta
    public bool hasBeenPlayed;

    // variavel para guardar em qual slot esta a carta
    public int handIndex;

    // variavel para salvar a carta de amostra
    public GameObject cartaExpandida;

    // criando uma variavel do tipo GameManager com nome de gm
    private GameManager gm;

    /// <summary>
    /// classe executada quando comeca o jogo, 1 vez e primeira que as outras
    /// </summary>
    private void Start()
    {
        // procurando o objeto GameManager e colocamdo ele na variavel
        gm = FindObjectOfType<GameManager>();
        
    }
    
    /// <summary>
    /// quando clicar com o mouse vai executar essa classe
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // verificando qual jogador e da vez
        switch (gm.rodadaDoJogador)
        {
            // jogador 1
            case 0:
                // verificando se nao foi jogada a carta se pode discartar e se � a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl1)
                {
                    // levando a carta um pouco para cima - avisando que ja foi jogada a carta
                    transform.position += Vector3.up * 1;       
                    hasBeenPlayed = true;

                    // avisando que aquele slot de carta esta vazio
                    gm.slotsDisponiveisCartasPlayer1[handIndex] = true;

                    // desativando a carta de amostra - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = null;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.deckPlayer1.Remove(this);
                    gm.MoveToDiscardPile(this);
                }
                break;

            // jogador 2
            case 1:
                // verificando se nao foi jogada a carta se pode discartar e se � a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl2)
                {
                    // levando a carta um pouco para cima - avisando que ja foi jogada a carta
                    transform.position += Vector3.up * 1;
                    hasBeenPlayed = true;

                    // avisando que aquele slot de carta esta vazio
                    gm.slotsDisponiveisCartasPlayer2[handIndex] = true;

                    // desativando a carta de amostra - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = null;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.MoveToDiscardPile(this);
                    gm.deckPlayer2.Remove(this);
                }
                break;

            // jogador 3
            case 2:
                // verificando se nao foi jogada a carta se pode discartar e se � a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl3)
                {
                    // levando a carta um pouco para cima - avisando que ja foi jogada a carta
                    transform.position += Vector3.up * 1;
                    hasBeenPlayed = true;

                    // avisando que aquele slot de carta esta vazio
                    gm.slotsDisponiveisCartasPlayer3[handIndex] = true;

                    // desativando a carta de amostra - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = null;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.MoveToDiscardPile(this);
                    gm.deckPlayer3.Remove(this);
                }
                break;

            // jogador 4
            case 3:
                // verificando se nao foi jogada a carta se pode discartar e se � a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl4)
                {
                    // levando a carta um pouco para cima - avisando que ja foi jogada a carta
                    transform.position += Vector3.up * 1;
                    hasBeenPlayed = true;

                    // avisando que aquele slot de carta esta vazio
                    gm.slotsDisponiveisCartasPlayer4[handIndex] = true;

                    // desativando a carta de amostra - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = null;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.MoveToDiscardPile(this);
                    gm.deckPlayer4.Remove(this);
                }
                break;
        }

    }

    /// <summary>
    /// quando o cursor do mouse entrar dentro do collaider desse script
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // alterando a imagem da carta expandida para a da carta
        cartaExpandida.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
    }

    /// <summary>
    /// quando o cursor do mouse sair de dentro do collaider desse script
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // alterando a imagem da carta expandida para vazia
        cartaExpandida.GetComponent<Image>().sprite = gm.imageSemcard;
    }

}
