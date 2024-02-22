using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public bool hasBeenPlayed;                          // variavel para verificar se ja jogou a carta

    public int handIndex;                               // variavel para guardar em qual slot esta a carta

    public GameObject cartaExpandida;                   // variavel para salvar a carta de amostra

    private GameManager gm;                             // criando uma variavel do tipo GameManager com nome de gm

    private void Start()                                // classe executada quando comeca o jogo, 1 vez e primeira que as outras
    {
        gm = FindObjectOfType<GameManager>();           // procurando o objeto GameManager e colocamdo ele na variavel
    }

    private void OnMouseDown()                          // quando clicar com o mouse vai executar essa classe
    {
        if(hasBeenPlayed == false){                     // verificando se nao foi jogada a carta
            transform.position += Vector3.up * 1;       // levando a carta um pouco para cima 
            hasBeenPlayed = true;                       // avisando que ja foi jogada a carta (para evitar varios cliques seguidos levando a carta para cima)
            gm.availableCardSlots[handIndex] = true;    // avisando que aquele slot de carta esta vazio
            gm.MoveToDiscardPile(this);                 // chamando a funcao que esta no GameManager e passando essa carta junto

                                                        // desativando a carta de amostra
            cartaExpandida.GetComponent<SpriteRenderer>().sprite = null; 
            gameObject.SetActive(false);                // desativando a carta depois que ela for para a pilha de discarte
        }
    }

    private void OnMouseEnter()                         // classe para detectar a entrada do mouse 1 vez
    {
                                                        // alterando o sprite vazio para o da carta que o mouse esta em cima
        cartaExpandida.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // tirar a maioria das imagens e deixar so uma grande carta de amostra para servir como exemplo e so passar o sprite da carta que o mouse esta em cima

    // fazer parecido com o yougi oh dual links cartas mais juntas e expandi quando segurar o clica esquerdo e arrastar para descartar a carta
    // usar box colaider
    private void OnMouseExit()                          // classe para detectar a saida do mouse 1 vez
    {
        cartaExpandida.GetComponent<SpriteRenderer>().sprite = null;     // desativando a carta de amostra
    }

}
