using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public List<CardDysfunc> dysfunctions = new List<CardDysfunc>();        // criando a lista de cartas de disfuncoes
    public List<Card> deck = new List<Card>();                              // criando a lista de cartas do baralho
    public List<Card> discardPile = new List<Card>();                       // criando a lista de cartas do descarte

    public Transform[] dysfunSlots;                                           // para salvar a localizacao dos slots das dysfunctions 
    public Transform[] cardSlotsPlayer1;                                      // para salvar a localizacao dos slots do player 1
    public Transform[] cardSlotsPlayer2;                                      // para salvar a localizacao dos slots do player 2
    public Transform[] cardSlotsPlayer3;                                      // para salvar a localizacao dos slots do player 3
    public Transform[] cardSlotsPlayer4;                                      // para salvar a localizacao dos slots do player 4

    public bool[] slotsDisponiveisCartasPlayer1;                                       // para verificar se os slots estao vazios ou nao
    public bool[] slotsDisponiveisCartasPlayer2;
    public bool[] slotsDisponiveisCartasPlayer3;
    public bool[] slotsDisponiveisCartasPlayer4;

    public bool compraCarta = true;
    public bool discartaCarta = false;

    public int startingCards;                                               // quantas cartas vai ter inicialmente
    public int numPlayerNoJogo;
    public int rodadaDoJogador = 0;
    public int layerPl1 = 7;
    public int layerPl2 = 8;
    public int layerPl3 = 9;
    public int layerPl4 = 10;

    private Card lastCard;                                                  // salvando a ultima carta descartada
    private Card penultima;                                                 // salvando a penultima carta descartada
    public Button buttonD;                                                  // para armazenar o botao de descarte
    public Sprite imageSemcard;                                             // salvando imagem vazia de carta

    public void Start()                                                     // classe executada quando comeca o jogo, 1 vez e primeira que as outras
    {
        for (int z = 0; z < numPlayerNoJogo; z++) // loop para entregar todas as cartas para os 4 players
        {
            switch (z) // Definido qual player vai ser estregue as cartas
            {
                case 0:     //Entregando carta para o player 1

                    for (int i = 0; i < startingCards; i++)                              // colocando o numero de cartas iniciais
                    {
                        Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                        randCard.gameObject.layer = layerPl1;
                        randCard.gameObject.SetActive(true);                        // vou mostrar a carta
                        randCard.handIndex = i;                                     // e salvar em qual slot ela esta

                        //Colocando a carta na posição e rotação certa
                        randCard.transform.position = cardSlotsPlayer1[i].position;
                        randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                        randCard.hasBeenPlayed = false;                             // avisando que a nao esta mais no baralho

                        slotsDisponiveisCartasPlayer1[i] = false;                              // avisando que o slot esta ocupado
                        deck.Remove(randCard);                                      // removendo a carta do deck
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    randCardD.gameObject.SetActive(true);                           // ligando ela

                    //Colocando a carta na posição e rotação certa
                    randCardD.transform.position = dysfunSlots[z].position;
                    randCardD.transform.rotation = dysfunSlots[z].rotation;

                    dysfunctions.Remove(randCardD);                                 // removendo ela do baralho
                    
                    break;

                case 1:     //Entregando carta para o player 2
                    for (int i = 0; i < startingCards; i++)                              // colocando o numero de cartas iniciais
                    {
                        Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                        randCard.gameObject.layer = layerPl2;
                        randCard.gameObject.SetActive(true);                        // vou mostrar a carta
                        randCard.handIndex = i;                                     // e salvar em qual slot ela esta

                        //Colocando a carta na posição e rotação certa
                        randCard.transform.position = cardSlotsPlayer2[i].position;
                        randCard.transform.rotation = cardSlotsPlayer2[i].rotation;

                        randCard.hasBeenPlayed = false;                             // avisando que a nao esta mais no baralho

                        slotsDisponiveisCartasPlayer2[i] = false;                              // avisando que o slot esta ocupado
                        deck.Remove(randCard);                                      // removendo a carta do deck
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD1 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    randCardD1.gameObject.SetActive(true);                           // ligando ela

                    //Colocando a carta na posição e rotação certa
                    randCardD1.transform.position = dysfunSlots[z].position;
                    randCardD1.transform.rotation = dysfunSlots[z].rotation;

                    dysfunctions.Remove(randCardD1);                                 // removendo ela do baralho
                    break;

                case 2:     //Entregando carta para o player 2
                    for (int i = 0; i < startingCards; i++)                              // colocando o numero de cartas iniciais
                    {
                        Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                        randCard.gameObject.layer = layerPl3;
                        randCard.gameObject.SetActive(true);                        // vou mostrar a carta
                        randCard.handIndex = i;                                     // e salvar em qual slot ela esta

                        //Colocando a carta na posição e rotação certa
                        randCard.transform.position = cardSlotsPlayer3[i].position;
                        randCard.transform.rotation = cardSlotsPlayer3[i].rotation;

                        randCard.hasBeenPlayed = false;                             // avisando que a nao esta mais no baralho

                        slotsDisponiveisCartasPlayer3[i] = false;                              // avisando que o slot esta ocupado
                        deck.Remove(randCard);                                      // removendo a carta do deck
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD2 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    randCardD2.gameObject.SetActive(true);                           // ligando ela

                    //Colocando a carta na posição e rotação certa
                    randCardD2.transform.position = dysfunSlots[z].position;
                    randCardD2.transform.rotation = dysfunSlots[z].rotation;

                    dysfunctions.Remove(randCardD2);                                 // removendo ela do baralho
                    break;

                case 3:     //Entregando carta para o player 2
                    for (int i = 0; i < startingCards; i++)                              // colocando o numero de cartas iniciais
                    {
                        Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                        randCard.gameObject.layer = layerPl4;
                        randCard.gameObject.SetActive(true);                        // vou mostrar a carta
                        randCard.handIndex = i;                                     // e salvar em qual slot ela esta

                        //Colocando a carta na posição e rotação certa
                        randCard.transform.position = cardSlotsPlayer4[i].position;
                        randCard.transform.rotation = cardSlotsPlayer4[i].rotation;

                        randCard.hasBeenPlayed = false;                             // avisando que a nao esta mais no baralho

                        slotsDisponiveisCartasPlayer4[i] = false;                              // avisando que o slot esta ocupado
                        deck.Remove(randCard);                                      // removendo a carta do deck
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD3 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    randCardD3.gameObject.SetActive(true);                           // ligando ela

                    //Colocando a carta na posição e rotação certa
                    randCardD3.transform.position = dysfunSlots[z].position;
                    randCardD3.transform.rotation = dysfunSlots[z].rotation;

                    dysfunctions.Remove(randCardD3);                                 // removendo ela do baralho
                    break;
            }
        
        }
    }

    public void DrawCard()                                                  // classe para quando for clicado o botao de compra de carta do baralho
    {

        switch (rodadaDoJogador)
        {
            case 0:
                if (deck.Count >= 1 && compraCarta)                                                // verificando se ainda tem carta na baralho
                {
                    Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                    for (int i = 0; i < slotsDisponiveisCartasPlayer1.Length; i++)             // verificando todos os slots de cartas ate a ultima
                    {
                        if (slotsDisponiveisCartasPlayer1[i] == true)                          // se esse slot estiver vazio
                        {
                            randCard.gameObject.SetActive(true);                    // vou mostrar a carta
                            randCard.handIndex = i;                                 // e salvar em qual slot ela esta
                            randCard.gameObject.layer = layerPl1;

                            randCard.transform.position = cardSlotsPlayer1[i].position;    // levar a carta para onde esta o slot
                            randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                            randCard.hasBeenPlayed = false;                         // avisando que a nao esta mais no baralho

                            slotsDisponiveisCartasPlayer1[i] = false;                          // avisando que o slot esta ocupado
                            deck.Remove(randCard);                                  // removendo a carta do deck

                            compraCarta = false;        //impedindo do jogador comprar outras cartas
                            discartaCarta = true;       //liberando o jogador para discartar uma carta

                            return;                                                 // parando o looping
                        }
                    }

                }
                else if (deck.Count <= 0)                                                               // se o baralho esiver vazio
                {
                    Debug.Log("Acabou as cartas do baralho");                       // avisa que acabou as cartas

                    Shuffle();                                                      // e embaralha elas
                }
                break;

            case 1:
                if (deck.Count >= 1 && compraCarta)                                                // verificando se ainda tem carta na baralho
                {
                    Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                    for (int i = 0; i < slotsDisponiveisCartasPlayer2.Length; i++)             // verificando todos os slots de cartas ate a ultima
                    {
                        if (slotsDisponiveisCartasPlayer2[i] == true)                          // se esse slot estiver vazio
                        {
                            randCard.gameObject.SetActive(true);                    // vou mostrar a carta
                            randCard.handIndex = i;                                 // e salvar em qual slot ela esta
                            randCard.gameObject.layer = layerPl2;

                            randCard.transform.position = cardSlotsPlayer2[i].position;    // levar a carta para onde esta o slot
                            randCard.transform.rotation = cardSlotsPlayer2[i].rotation;
                            
                            randCard.hasBeenPlayed = false;                         // avisando que a nao esta mais no baralho

                            slotsDisponiveisCartasPlayer2[i] = false;                          // avisando que o slot esta ocupado
                            deck.Remove(randCard);                                  // removendo a carta do deck

                            compraCarta = false;        //impedindo do jogador comprar outras cartas
                            discartaCarta = true;       //liberando o jogador para discartar uma carta

                            return;                                                 // parando o looping
                        }
                    }

                }
                else if (deck.Count <= 0)                                                               // se o baralho esiver vazio
                {
                    Debug.Log("Acabou as cartas do baralho");                       // avisa que acabou as cartas

                    Shuffle();                                                      // e embaralha elas
                }
                break;

            case 2:
                if (deck.Count >= 1 && compraCarta)                                                // verificando se ainda tem carta na baralho
                {
                    Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                    for (int i = 0; i < slotsDisponiveisCartasPlayer3.Length; i++)             // verificando todos os slots de cartas ate a ultima
                    {
                        if (slotsDisponiveisCartasPlayer3[i] == true)                          // se esse slot estiver vazio
                        {
                            randCard.gameObject.SetActive(true);                    // vou mostrar a carta
                            randCard.handIndex = i;                                 // e salvar em qual slot ela esta
                            randCard.gameObject.layer = layerPl3;

                            randCard.transform.position = cardSlotsPlayer3[i].position;    // levar a carta para onde esta o slot
                            randCard.transform.rotation = cardSlotsPlayer3[i].rotation;

                            randCard.hasBeenPlayed = false;                         // avisando que a nao esta mais no baralho

                            slotsDisponiveisCartasPlayer3[i] = false;                          // avisando que o slot esta ocupado
                            deck.Remove(randCard);                                  // removendo a carta do deck

                            compraCarta = false;        //impedindo do jogador comprar outras cartas
                            discartaCarta = true;       //liberando o jogador para discartar uma carta

                            return;                                                 // parando o looping
                        }
                    }

                }
                else if (deck.Count <= 0)                                                               // se o baralho esiver vazio
                {
                    Debug.Log("Acabou as cartas do baralho");                       // avisa que acabou as cartas

                    Shuffle();                                                      // e embaralha elas
                }
                break;

            case 3:
                if (deck.Count >= 1 && compraCarta)                                                // verificando se ainda tem carta na baralho
                {
                    Card randCard = deck[Random.Range(0, deck.Count)];              // salvando uma carta aleatoria que esta no baralho

                    for (int i = 0; i < slotsDisponiveisCartasPlayer4.Length; i++)             // verificando todos os slots de cartas ate a ultima
                    {
                        if (slotsDisponiveisCartasPlayer4[i] == true)                          // se esse slot estiver vazio
                        {
                            randCard.gameObject.SetActive(true);                    // vou mostrar a carta
                            randCard.handIndex = i;                                 // e salvar em qual slot ela esta
                            randCard.gameObject.layer = layerPl4;

                            randCard.transform.position = cardSlotsPlayer4[i].position;    // levar a carta para onde esta o slot
                            randCard.transform.rotation = cardSlotsPlayer4[i].rotation;

                            randCard.hasBeenPlayed = false;                         // avisando que a nao esta mais no baralho

                            slotsDisponiveisCartasPlayer4[i] = false;                          // avisando que o slot esta ocupado
                            deck.Remove(randCard);                                  // removendo a carta do deck

                            compraCarta = false;        //impedindo do jogador comprar outras cartas
                            discartaCarta = true;       //liberando o jogador para discartar uma carta

                            return;                                                 // parando o looping
                        }
                    }

                }
                else if (deck.Count <= 0)                                                               // se o baralho esiver vazio
                {
                    Debug.Log("Acabou as cartas do baralho");                       // avisa que acabou as cartas

                    Shuffle();                                                      // e embaralha elas
                }
                break;
        }



    }

    public void PurchaseDiscard()                                           // quando clicar no discarte (botao) comprar a ultima carta jogada e mostrar a anterior
    {

        switch(rodadaDoJogador)
        {
            case 0:
                if (discardPile.Count >= 1 && compraCarta)                                          // verificando se tem carta no descarte
                {

                    for (int i = 0; i < discardPile.Count; i++)                        // procurando a ultima carta descartada
                    {

                        if (i == discardPile.Count - 1)                               // verificando que e a ultima carta
                        {

                            lastCard = discardPile[i];                              // salvando a ultima carta do discarte
                            penultima = null;                                       // sempre indicando a variavel penultima camo null
                            if (i - 1 >= 0)                                           // verificando se nao e a ultima carta
                            {
                                penultima = discardPile[i - 1];                       // pegando a penultima carta
                            }

                            for (int y = 0; y < slotsDisponiveisCartasPlayer1.Length; y++)      // verificando todos os slots de cartas ate a ultima
                            {
                                if (slotsDisponiveisCartasPlayer1[y] == true)                   // se esse slot estiver vazio
                                {

                                    lastCard.gameObject.SetActive(true);            // vou mostrar a carta
                                    lastCard.handIndex = y;                         // e salvar em qual slot ela esta
                                    lastCard.gameObject.layer = layerPl1;

                                    // levar a carta para onde esta o slot
                                    lastCard.transform.position = cardSlotsPlayer1[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer1[y].rotation;
                                    lastCard.hasBeenPlayed = false;                 // avisando que a nao esta mais no descarte

                                    slotsDisponiveisCartasPlayer1[y] = false;                  // avisando que o slot esta ocupado
                                    discardPile.Remove(lastCard);                   // removendo a carta do discarte

                                    compraCarta = false;        //impedindo do jogador comprar outras cartas
                                    discartaCarta = true;       //liberando o jogador para discartar uma carta

                                    if (penultima != null)                           // verificando se nao contem penultima carta
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
                break;
            case 1:
                if (discardPile.Count >= 1 && compraCarta)                                          // verificando se tem carta no descarte
                {

                    for (int i = 0; i < discardPile.Count; i++)                        // procurando a ultima carta descartada
                    {

                        if (i == discardPile.Count - 1)                               // verificando que e a ultima carta
                        {

                            lastCard = discardPile[i];                              // salvando a ultima carta do discarte
                            penultima = null;                                       // sempre indicando a variavel penultima camo null
                            if (i - 1 >= 0)                                           // verificando se nao e a ultima carta
                            {
                                penultima = discardPile[i - 1];                       // pegando a penultima carta
                            }

                            for (int y = 0; y < slotsDisponiveisCartasPlayer2.Length; y++)      // verificando todos os slots de cartas ate a ultima
                            {
                                if (slotsDisponiveisCartasPlayer2[y] == true)                   // se esse slot estiver vazio
                                {

                                    lastCard.gameObject.SetActive(true);            // vou mostrar a carta
                                    lastCard.handIndex = y;                         // e salvar em qual slot ela esta
                                    lastCard.gameObject.layer = layerPl2;

                                    // levar a carta para onde esta o slot
                                    lastCard.transform.position = cardSlotsPlayer2[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer2[y].rotation;
                                    lastCard.hasBeenPlayed = false;                 // avisando que a nao esta mais no descarte

                                    slotsDisponiveisCartasPlayer2[y] = false;                  // avisando que o slot esta ocupado
                                    discardPile.Remove(lastCard);                   // removendo a carta do discarte

                                    compraCarta = false;        //impedindo do jogador comprar outras cartas
                                    discartaCarta = true;       //liberando o jogador para discartar uma carta

                                    if (penultima != null)                           // verificando se nao contem penultima carta
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
                break;
            case 2:
                if (discardPile.Count >= 1 && compraCarta)                                          // verificando se tem carta no descarte
                {

                    for (int i = 0; i < discardPile.Count; i++)                        // procurando a ultima carta descartada
                    {

                        if (i == discardPile.Count - 1)                               // verificando que e a ultima carta
                        {

                            lastCard = discardPile[i];                              // salvando a ultima carta do discarte
                            penultima = null;                                       // sempre indicando a variavel penultima camo null
                            if (i - 1 >= 0)                                           // verificando se nao e a ultima carta
                            {
                                penultima = discardPile[i - 1];                       // pegando a penultima carta
                            }

                            for (int y = 0; y < slotsDisponiveisCartasPlayer3.Length; y++)      // verificando todos os slots de cartas ate a ultima
                            {
                                if (slotsDisponiveisCartasPlayer3[y] == true)                   // se esse slot estiver vazio
                                {

                                    lastCard.gameObject.SetActive(true);            // vou mostrar a carta
                                    lastCard.handIndex = y;                         // e salvar em qual slot ela esta
                                    lastCard.gameObject.layer = layerPl3;

                                    // levar a carta para onde esta o slot
                                    lastCard.transform.position = cardSlotsPlayer3[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer3[y].rotation;
                                    lastCard.hasBeenPlayed = false;                 // avisando que a nao esta mais no descarte

                                    slotsDisponiveisCartasPlayer3[y] = false;                  // avisando que o slot esta ocupado
                                    discardPile.Remove(lastCard);                   // removendo a carta do discarte

                                    compraCarta = false;        //impedindo do jogador comprar outras cartas
                                    discartaCarta = true;       //liberando o jogador para discartar uma carta

                                    if (penultima != null)                           // verificando se nao contem penultima carta
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
                break;
            case 3:
                if (discardPile.Count >= 1 && compraCarta)                                          // verificando se tem carta no descarte
                {

                    for (int i = 0; i < discardPile.Count; i++)                        // procurando a ultima carta descartada
                    {

                        if (i == discardPile.Count - 1)                               // verificando que e a ultima carta
                        {

                            lastCard = discardPile[i];                              // salvando a ultima carta do discarte
                            penultima = null;                                       // sempre indicando a variavel penultima camo null
                            if (i - 1 >= 0)                                           // verificando se nao e a ultima carta
                            {
                                penultima = discardPile[i - 1];                       // pegando a penultima carta
                            }

                            for (int y = 0; y < slotsDisponiveisCartasPlayer4.Length; y++)      // verificando todos os slots de cartas ate a ultima
                            {
                                if (slotsDisponiveisCartasPlayer4[y] == true)                   // se esse slot estiver vazio
                                {

                                    lastCard.gameObject.SetActive(true);            // vou mostrar a carta
                                    lastCard.handIndex = y;                         // e salvar em qual slot ela esta
                                    lastCard.gameObject.layer = layerPl4;

                                    // levar a carta para onde esta o slot
                                    lastCard.transform.position = cardSlotsPlayer4[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer4[y].rotation;
                                    lastCard.hasBeenPlayed = false;                 // avisando que a nao esta mais no descarte

                                    slotsDisponiveisCartasPlayer4[y] = false;                  // avisando que o slot esta ocupado
                                    discardPile.Remove(lastCard);                   // removendo a carta do discarte

                                    compraCarta = false;        //impedindo do jogador comprar outras cartas
                                    discartaCarta = true;       //liberando o jogador para discartar uma carta

                                    if (penultima != null)                           // verificando se nao contem penultima carta
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
                break;
        }
        
    }

    public void MoveToDiscardPile(Card card)                                // classe criada para quando tiver que mover uma carta para pilha de discarte
    {
        discardPile.Add(card);                                              // adicionando a carta na pilha de discarte

                                                                            // para trocar a imagem do botao para a da carta
        buttonD.GetComponent<Image>().sprite = card.GetComponent<SpriteRenderer>().sprite;

        compraCarta = true;         //liberando o jogador para comprar carta
        discartaCarta = false;      //impedindo o jogador de discartar mais cartas
        rodadaDoJogador++;
        
        if(rodadaDoJogador == numPlayerNoJogo)
        {
            rodadaDoJogador = 0;
        }

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

}
