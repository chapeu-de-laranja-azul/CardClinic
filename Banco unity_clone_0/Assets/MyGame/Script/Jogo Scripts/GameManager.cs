using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public List<CardDysfunc> dysfunctions = new List<CardDysfunc>();                      // criando a lista de cartas de disfuncoes
    public List<Card> deck = new List<Card>();                              // criando a lista de cartas do baralho
    public List<Card> discardPile = new List<Card>();                       // criando a lista de cartas do descarte

    public Transform dysfunSlots;                                           // para salvar a localizacao dos slots das dysfunctions 
    public Transform[] cardSlots;                                           // para salvar a localizacao dos slots
    public bool[] availableCardSlots;                                       // para verificar se os slots estao vazios ou nao

    public Text deckSizeText;                                               // para guardar e escrever o numero das cartas no baralho
    public Text discardPileText;                                            // para guardar e escrever o numero das cartas no descarte

    public int startingCards;                                               // quantas cartas vai ter inicialmente

    private Card lastCard;                                                  // salvando a ultima carta descartada
    private Card penultima;                                                 // salvando a penultima carta descartada
    public Button buttonD;                                                  // para armazenar o botao de descarte
    public Sprite imageSemcard;                                             // salvando imagem vazia de carta

    public void Start()                                                     // classe executada quando comeca o jogo, 1 vez e primeira que as outras
    {
        for(int i = 0; i < startingCards; i++)                              // colocando o numero de cartas iniciais
        {
            Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                randCard.gameObject.SetActive(true);                        // vou mostrar a carta
                randCard.handIndex = i;                                     // e salvar em qual slot ela esta

                randCard.transform.position = cardSlots[i].position;        // levar a carta para onde esta o slot
                randCard.hasBeenPlayed = false;                             // avisando que a nao esta mais no baralho

                availableCardSlots[i] = false;                              // avisando que o slot esta ocupado
                deck.Remove(randCard);                                      // removendo a carta do deck
        }

                                                                            // parte para colocar a carta de dysfuncao no jogo
        CardDysfunc randCardD = dysfunctions[Random.Range(0, dysfunctions.Count)];

            randCardD.gameObject.SetActive(true);                           // ligando ela

            randCardD.transform.position = dysfunSlots.position;            // colocando na posicao

            dysfunctions.Remove(randCardD);                                 // removendo ela do baralho

    }

    public void DrawCard()                                                  // classe para quando for clicado o botao de compra de carta do baralho
    {

        if (deck.Count >= 1)                                                // verificando se ainda tem carta na baralho
        {
            Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho
            
            for (int i = 0; i < availableCardSlots.Length; i++)             // verificando todos os slots de cartas ate a ultima
            {
                if (availableCardSlots[i] == true)                          // se esse slot estiver vazio
                {
                    randCard.gameObject.SetActive(true);                    // vou mostrar a carta
                    randCard.handIndex = i;                                 // e salvar em qual slot ela esta

                    randCard.transform.position = cardSlots[i].position;    // levar a carta para onde esta o slot
                    randCard.hasBeenPlayed = false;                         // avisando que a nao esta mais no baralho

                    availableCardSlots[i] = false;                          // avisando que o slot esta ocupado
                    deck.Remove(randCard);                                  // removendo a carta do deck
                    return;                                                 // parando o looping
                }
            }

        }
        else                                                                // se o baralho esiver vazio
        {
            Debug.Log("Acabou as cartas do baralho");                       // avisa que acabou as cartas

            Shuffle();                                                      // e embaralha elas
        }

    }

    public void PurchaseDiscard()                                           // quando clicar no discarte (botao) comprar a ultima carta jogada e mostrar a anterior
    {
        if(discardPile.Count >= 1)                                          // verificando se tem carta no descarte
        {
            for(int i=0; i < discardPile.Count; i++)                        // procurando a ultima carta descartada
            {

                if(i == discardPile.Count -1)                               // verificando que e a ultima carta
                {
                    
                    lastCard = discardPile[i];                              // salvando a ultima carta do discarte
                    penultima = null;                                       // sempre indicando a variavel penultima camo null
                    if(i-1 >=  0)                                           // verificando se nao e a ultima carta
                    {
                        penultima = discardPile[i-1];                       // pegando a penultima carta
                    }

                    for(int y = 0; y < availableCardSlots.Length; y++)      // verificando todos os slots de cartas ate a ultima
                    {
                        if(availableCardSlots[y] == true)                   // se esse slot estiver vazio
                        {

                            lastCard.gameObject.SetActive(true);            // vou mostrar a carta
                            lastCard.handIndex = y;                         // e salvar em qual slot ela esta

                                                                            // levar a carta para onde esta o slot
                            lastCard.transform.position = cardSlots[y].position;
                            lastCard.hasBeenPlayed = false;                 // avisando que a nao esta mais no descarte

                            availableCardSlots[y] = false;                  // avisando que o slot esta ocupado
                            discardPile.Remove(lastCard);                   // removendo a carta do discarte
                            
                            if(penultima != null)                           // verificando se nao contem penultima carta
                            {
                                                                            // substituindo a imagem para a carta anterior
                                buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite; 
                                return;
                            }
                            else
                            {                                               // colocando a imagem de discarte vazio
                                buttonD.GetComponent<Image>().sprite = imageSemcard;
                                return;                                     // parando o looping
                            }
                            
                        }

                    }

                }

            }

        }
    }

    public void MoveToDiscardPile(Card card)                                // classe criada para quando tiver que mover uma carta para pilha de discarte
    {
        discardPile.Add(card);                                              // adicionando a carta na pilha de discarte

                                                                            // para trocar a imagem do botao para a da carta
        buttonD.GetComponent<Image>().sprite = card.GetComponent<SpriteRenderer>().sprite; 
    
    }

    public void Shuffle()                                                   // classe para embaralhar
    {
        if(discardPile.Count >=1){                                          // verificando se tem cartas no descarte
            foreach(Card card in discardPile){                              // executando um looping pelo numero de cartas que tem no descarte
                deck.Add(card);                                             // adicionamdo a carta no baralho
            }
            discardPile.Clear();                                            // esvaziando a Array
        }
    }

    private void Update()                                                   // verificando a todo momento
    {
        deckSizeText.text = deck.Count.ToString();                          // atualizando o texto com o numero de cartas que tem no baralho
        discardPileText.text = discardPile.Count.ToString();                // atualizando o texto com o numero de cartas que tem no descarte
    

    }


}
