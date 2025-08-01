using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

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

    // variavel para pegar o canvas
    public Canvas canvas;

    /// <summary>
    /// classe executada quando comeca o jogo, 1 vez e primeira que as outras
    /// </summary>
    private void Start()
    {
        // procurando o objeto GameManager e colocando ele na variavel
        gm = FindObjectOfType<GameManager>();

        // pegando o componente do objeto que tem esse script e salvando na variavel
        canvas = gameObject.GetComponent<Canvas>();
    }
    
    /// <summary>
    /// quando clicar com o mouse vai executar essa classe
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // diminuindo a carta
        transform.localScale = new Vector3(0.5f, 0.5f, 1);

        // verificando qual jogador e da vez
        switch (gm.rodadaDoJogador)
        {
            // jogador 1
            case 0:
                // verificando se nao foi jogada a carta se pode discartar e se é a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl1)
                {
                    // avisando que ja foi jogada a carta       
                    hasBeenPlayed = true;

                    // alterando a imagem da carta expandida para vazia - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = gm.imagemVazio;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.deckPlayer1.Remove(this);
                    gm.MoveToDiscardPile(this);

                    // avisando que aquele slot de carta esta vazio
                    if (gm.slotCartaExtraDisponivel == true)
                    {
                        gm.cartaExtra = null;

                        // verificando se esta clicando no slot extra
                        if (handIndex != 9)
                        {
                            // avisando que aquele slot de carta esta vazio
                            gm.slotsDisponiveisCartasPlayer1[handIndex] = true;
                        }
                    }
                    // se o slot extra nao estiver vazio, entao ele vai pegar a carta que esta no slot extra e colocar na mao do jogador
                    else
                    {
                        // avisando que aquele slot de carta esta vazio
                        gm.slotsDisponiveisCartasPlayer1[handIndex] = true;

                        // pegando a carta que esta no slot extra e colocando na mao do jogador
                        gm.AlterandoPosiçãoSlotExtra();
                    }

                }
                break;

            // jogador 2
            case 1:
                // verificando se nao foi jogada a carta se pode discartar e se é a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl2)
                {
                    // avisando que ja foi jogada a carta
                    hasBeenPlayed = true;

                    // alterando a imagem da carta expandida para vazia - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = gm.imagemVazio;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.deckPlayer2.Remove(this);
                    gm.MoveToDiscardPile(this);

                    // avisando que aquele slot de carta esta vazio
                    if (gm.slotCartaExtraDisponivel == true)
                    {
                        gm.cartaExtra = null;

                        // verificando se esta clicando no slot extra
                        if (handIndex != 9)
                        {
                            // avisando que aquele slot de carta esta vazio
                            gm.slotsDisponiveisCartasPlayer2[handIndex] = true;
                        }
                    }
                    // se o slot extra nao estiver vazio, entao ele vai pegar a carta que esta no slot extra e colocar na mao do jogador
                    else
                    {
                        // avisando que aquele slot de carta esta vazio
                        gm.slotsDisponiveisCartasPlayer2[handIndex] = true;

                        // pegando a carta que esta no slot extra e colocando na mao do jogador
                        gm.AlterandoPosiçãoSlotExtra();
                    }
                }
                break;

            // jogador 3
            case 2:
                // verificando se nao foi jogada a carta se pode discartar e se é a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl3)
                {
                    // avisando que ja foi jogada a carta
                    hasBeenPlayed = true;

                    // alterando a imagem da carta expandida para vazia - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = gm.imagemVazio;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.deckPlayer3.Remove(this);
                    gm.MoveToDiscardPile(this);

                    // avisando que aquele slot de carta esta vazio
                    if (gm.slotCartaExtraDisponivel == true)
                    {
                        gm.cartaExtra = null;

                        // verificando se esta clicando no slot extra
                        if (handIndex != 9)
                        {
                            // avisando que aquele slot de carta esta vazio
                            gm.slotsDisponiveisCartasPlayer3[handIndex] = true;
                        }
                    }
                    // se o slot extra nao estiver vazio, entao ele vai pegar a carta que esta no slot extra e colocar na mao do jogador
                    else
                    {
                        // avisando que aquele slot de carta esta vazio
                        gm.slotsDisponiveisCartasPlayer3[handIndex] = true;

                        // pegando a carta que esta no slot extra e colocando na mao do jogador
                        gm.AlterandoPosiçãoSlotExtra();
                    }

                }
                break;

            // jogador 4
            case 3:
                // verificando se nao foi jogada a carta se pode discartar e se é a camada do player certa
                if (hasBeenPlayed == false && gm.discartaCarta && gameObject.layer == gm.layerPl4)
                {
                    // avisando que ja foi jogada a carta
                    hasBeenPlayed = true;

                    // alterando a imagem da carta expandida para vazia - desativando a carta depois que ela for para a pilha de discarte
                    cartaExpandida.GetComponent<Image>().sprite = gm.imagemVazio;
                    gameObject.SetActive(false);

                    // chamando a funcao que esta no GameManager e passando essa carta junto
                    gm.deckPlayer4.Remove(this);
                    gm.MoveToDiscardPile(this);

                    // avisando que aquele slot de carta esta vazio
                    if (gm.slotCartaExtraDisponivel == true)
                    {
                        gm.cartaExtra = null;

                        // verificando se esta clicando no slot extra
                        if (handIndex != 9)
                        {
                            // avisando que aquele slot de carta esta vazio
                            gm.slotsDisponiveisCartasPlayer4[handIndex] = true;
                        }
                    }
                    // se o slot extra nao estiver vazio, entao ele vai pegar a carta que esta no slot extra e colocar na mao do jogador
                    else
                    {
                        // avisando que aquele slot de carta esta vazio
                        gm.slotsDisponiveisCartasPlayer4[handIndex] = true;

                        // pegando a carta que esta no slot extra e colocando na mao do jogador
                        gm.AlterandoPosiçãoSlotExtra();
                    }

                }
                break;
        }

    }

    /// <summary>
    /// quando o cursor do mouse entrar dentro do collaider desse script
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // deixando a carta na frente de todas
        canvas.sortingOrder = 100;

        // expandido a carta
        transform.localScale = new Vector3(0.6f, 0.6f, 1);

        // alterando a imagem da carta expandida para a da carta
        cartaExpandida.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;

        // fechando o painel do numero aleatorio do dado
        gm.ClosePainelNum();

    }

    /// <summary>
    /// quando o cursor do mouse sair de dentro do collaider desse script
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // retornando a carta para a ordem dela na mão
        canvas.sortingOrder = handIndex;

        // diminuindo a carta
        transform.localScale = new Vector3(0.5f, 0.5f, 1);

        // alterando a imagem da carta expandida para vazia
        cartaExpandida.GetComponent<Image>().sprite = gm.imagemVazio;
    }

}
