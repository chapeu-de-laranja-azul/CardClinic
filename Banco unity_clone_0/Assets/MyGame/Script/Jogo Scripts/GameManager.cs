using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region Variaveis utilizadas no GameManager
    // criando listas para o baralho, discarte e uma separada para as disfunções
    public List<CardDysfunc> dysfunctions = new List<CardDysfunc>();
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    // as arrays para salvar as posições de todas as cartas que estarão no jogo
    public Transform[] dysfunSlots;
    public Transform[] cardSlotsPlayer1;
    public Transform[] cardSlotsPlayer2;
    public Transform[] cardSlotsPlayer3;
    public Transform[] cardSlotsPlayer4;

    // para verificar se os slots estao vazios ou nao
    public bool[] slotsDisponiveisCartasPlayer1;
    public bool[] slotsDisponiveisCartasPlayer2;
    public bool[] slotsDisponiveisCartasPlayer3;
    public bool[] slotsDisponiveisCartasPlayer4;

    // verificadores para o controle de quando os jogadores podem discartar ou comprar cartas
    public bool compraCarta = true;
    public bool discartaCarta = false;

    // verificador para resetar a carta de dysfunção ao jogador passar o turno
    public bool resetDysfuncao = false;

    // quantas cartas vai ser entregue inicialmente - quantos jogadores tem no jogo - controle de qual jogador vai jogar
    public int startingCards;
    public int numPlayerNoJogo;
    public int rodadaDoJogador = 0;

    // qual camada e a dos players - e uma variavel para guardar o numero de rodadas jogadas e a rodada final
    public int layerPl1 = 7;
    public int layerPl2 = 8;
    public int layerPl3 = 9;
    public int layerPl4 = 10;
    public int rodada = 0;
    public int rodadaFinal;

    // salvando a ultima carta descartada - salvando a penultima carta descartada
    private Card lastCard;
    private Card penultima;

    // para armazenar o botao de descarte
    public Button buttonD;

    // salvando o texto do contador
    public TextMeshProUGUI contador;

    // salvando uma imagem vazia de carta
    public Sprite imageSemcard;

    #endregion

    /// <summary>
    /// classe executada quando comeca o jogo, 1 vez e primeira que as outras
    /// </summary>
    public void Start()
    {
        // loop para entregar todas as cartas do inicio do jogo (para o numero de jogadores que forem jogar)
        for (int z = 0; z < numPlayerNoJogo; z++)
        {
            // Definido qual player vai ser estregue as cartas
            switch (z)
            {
                // Entregando as cartas para o player 1
                case 0:

                    // entregando o numero de cartas iniciais, que foi definido antes
                    for (int i = 0; i < startingCards; i++)
                    {
                        // salvando uma carta aleatoria que esta no baralho
                        Card randCard = deck[Random.Range(0, deck.Count)];

                        // Definindo a camada do player - mostrando a carta - e salvando em qual slot da mão ela ficara
                        randCard.gameObject.layer = layerPl1;
                        randCard.gameObject.SetActive(true);
                        randCard.handIndex = i;

                        //Colocando a carta na posição e rotação certa do slot
                        randCard.transform.position = cardSlotsPlayer1[i].position;
                        randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                        // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                        randCard.hasBeenPlayed = false;

                        // avisando que o slot esta ocupado e removendo a carta do deck
                        slotsDisponiveisCartasPlayer1[i] = false;
                        deck.Remove(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD.gameObject.layer = layerPl1;
                    randCardD.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD.transform.position = dysfunSlots[z].position;
                    randCardD.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho de disfunções
                    dysfunctions.Remove(randCardD);
                    
                    break;

                //Entregando carta para o player 2
                case 1:

                    // entregando o numero de cartas iniciais, que foi definido antes
                    for (int i = 0; i < startingCards; i++)
                    {
                        // salvando uma carta aleatoria que esta no baralho
                        Card randCard = deck[Random.Range(0, deck.Count)];

                        // Definindo a camada do player - mostrando a carta - e salvando em qual slot da mão ela ficara
                        randCard.gameObject.layer = layerPl2;
                        randCard.gameObject.SetActive(true);
                        randCard.handIndex = i;

                        //Colocando a carta na posição e rotação certa do slot
                        randCard.transform.position = cardSlotsPlayer2[i].position;
                        randCard.transform.rotation = cardSlotsPlayer2[i].rotation;

                        // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                        randCard.hasBeenPlayed = false;

                        // avisando que o slot esta ocupado e removendo a carta do deck
                        slotsDisponiveisCartasPlayer2[i] = false;
                        deck.Remove(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD1 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD1.gameObject.layer = layerPl2;
                    randCardD1.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD1.transform.position = dysfunSlots[z].position;
                    randCardD1.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho de disfunções
                    dysfunctions.Remove(randCardD1);
                    break;

                //Entregando carta para o player 3
                case 2:

                    // entregando o numero de cartas iniciais, que foi definido antes
                    for (int i = 0; i < startingCards; i++)
                    {
                        // salvando uma carta aleatoria que esta no baralho
                        Card randCard = deck[Random.Range(0, deck.Count)];

                        // Definindo a camada do player - mostrando a carta - e salvando em qual slot da mão ela ficara
                        randCard.gameObject.layer = layerPl3;
                        randCard.gameObject.SetActive(true);
                        randCard.handIndex = i;

                        //Colocando a carta na posição e rotação certa do slot
                        randCard.transform.position = cardSlotsPlayer3[i].position;
                        randCard.transform.rotation = cardSlotsPlayer3[i].rotation;

                        // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                        randCard.hasBeenPlayed = false;

                        // avisando que o slot esta ocupado e removendo a carta do deck
                        slotsDisponiveisCartasPlayer3[i] = false;
                        deck.Remove(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD2 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD2.gameObject.layer = layerPl3;
                    randCardD2.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD2.transform.position = dysfunSlots[z].position;
                    randCardD2.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho de disfunções
                    dysfunctions.Remove(randCardD2);
                    break;

                //Entregando carta para o player 4
                case 3:

                    // entregando o numero de cartas iniciais, que foi definido antes
                    for (int i = 0; i < startingCards; i++)
                    {
                        // salvando uma carta aleatoria que esta no baralho
                        Card randCard = deck[Random.Range(0, deck.Count)];

                        // Definindo a camada do player - mostrando a carta - e salvando em qual slot da mão ela ficara
                        randCard.gameObject.layer = layerPl4;
                        randCard.gameObject.SetActive(true);
                        randCard.handIndex = i;

                        //Colocando a carta na posição e rotação certa do slot
                        randCard.transform.position = cardSlotsPlayer4[i].position;
                        randCard.transform.rotation = cardSlotsPlayer4[i].rotation;

                        // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                        randCard.hasBeenPlayed = false;

                        // avisando que o slot esta ocupado e removendo a carta do deck
                        slotsDisponiveisCartasPlayer4[i] = false;
                        deck.Remove(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD3 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD3.gameObject.layer = layerPl4;
                    randCardD3.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD3.transform.position = dysfunSlots[z].position;
                    randCardD3.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho
                    dysfunctions.Remove(randCardD3);
                    break;
            }
        
        }
        
    }

    /// <summary>
    /// classe para quando for clicado o botao de compra de carta do baralho
    /// </summary>
    public void DrawCard()
    {

        // verificando qual jogador e da rodada
        switch (rodadaDoJogador)
        {

            // jogador 1
            case 0:

                // verificando se ainda tem carta na baralho e se o jogador pode comprar a carta
                if (deck.Count >= 1 && compraCarta)
                {
                    // salvando uma carta aleatoria que esta no baralho
                    Card randCard = deck[Random.Range(0, deck.Count)];

                    // verificando todos os slots de cartas
                    for (int i = 0; i < slotsDisponiveisCartasPlayer1.Length; i++)
                    {
                        // se esse slot estiver vazio
                        if (slotsDisponiveisCartasPlayer1[i] == true)
                        {
                            // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                            randCard.gameObject.SetActive(true); 
                            randCard.handIndex = i;
                            randCard.gameObject.layer = layerPl1;

                            // colocando a carta na posição e rotação certa do slot
                            randCard.transform.position = cardSlotsPlayer1[i].position;
                            randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer1[i] = false;
                            deck.Remove(randCard);

                            // impedindo do jogador de comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            //parando o looping
                            return;
                        }
                    }

                }

                // se o baralho esiver vazio
                else if (deck.Count <= 0)
                {
                    // avisa que acabou as carta - embaralhando elas
                    Debug.Log("Acabou as cartas do baralho");

                    Shuffle();
                }
                break;

            // jogador 2
            case 1:

                // verificando se ainda tem carta na baralho e se o jogador pode comprar carta
                if (deck.Count >= 1 && compraCarta)
                {
                    // salvando uma carta aleatoria que esta no baralho
                    Card randCard = deck[Random.Range(0, deck.Count)];

                    // verificando todos os slots de cartas
                    for (int i = 0; i < slotsDisponiveisCartasPlayer2.Length; i++)
                    {
                        // se esse slot estiver vazio
                        if (slotsDisponiveisCartasPlayer2[i] == true)
                        {
                            // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                            randCard.gameObject.SetActive(true);
                            randCard.handIndex = i;
                            randCard.gameObject.layer = layerPl2;

                            // colocando a carta na posição e rotação certa do slot
                            randCard.transform.position = cardSlotsPlayer2[i].position;
                            randCard.transform.rotation = cardSlotsPlayer2[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer2[i] = false;
                            deck.Remove(randCard);

                            // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            // parando o looping
                            return;
                        }
                    }

                }

                // se o baralho esiver vazio
                else if (deck.Count <= 0)
                {
                    // avisa que acabou as cartas - embaralha elas
                    Debug.Log("Acabou as cartas do baralho");

                    Shuffle();
                }
                break;

            // jogador 3
            case 2:

                // verificando se ainda tem carta na baralho e se o jogador pode comprar carta
                if (deck.Count >= 1 && compraCarta)
                {
                    // salvando uma carta aleatoria que esta no baralho
                    Card randCard = deck[Random.Range(0, deck.Count)];

                    // verificando todos os slots de cartas
                    for (int i = 0; i < slotsDisponiveisCartasPlayer3.Length; i++)
                    {
                        // se esse slot estiver vazio
                        if (slotsDisponiveisCartasPlayer3[i] == true)
                        {
                            // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                            randCard.gameObject.SetActive(true);
                            randCard.handIndex = i;
                            randCard.gameObject.layer = layerPl3;

                            // colocando a carta na posição e rotação certa do slot
                            randCard.transform.position = cardSlotsPlayer3[i].position;
                            randCard.transform.rotation = cardSlotsPlayer3[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer3[i] = false;
                            deck.Remove(randCard);

                            // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            // parando o looping
                            return;
                        }
                    }

                }

                // se o baralho esiver vazio
                else if (deck.Count <= 0)
                {
                    // avisa que acabou as cartas - embaralha elas
                    Debug.Log("Acabou as cartas do baralho");

                    Shuffle();
                }
                break;

            // jogador 4
            case 3:

                // verificando se ainda tem carta na baralho e se o jogador pode comprar carta
                if (deck.Count >= 1 && compraCarta)
                {
                    // salvando uma carta aleatoria que esta no baralho
                    Card randCard = deck[Random.Range(0, deck.Count)];

                    // verificando todos os slots de cartas
                    for (int i = 0; i < slotsDisponiveisCartasPlayer4.Length; i++)
                    {
                        // se esse slot estiver vazio
                        if (slotsDisponiveisCartasPlayer4[i] == true)
                        {
                            // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                            randCard.gameObject.SetActive(true);
                            randCard.handIndex = i;
                            randCard.gameObject.layer = layerPl4;

                            // colocando a carta na posição e rotação certa do slot
                            randCard.transform.position = cardSlotsPlayer4[i].position;
                            randCard.transform.rotation = cardSlotsPlayer4[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer4[i] = false;
                            deck.Remove(randCard);

                            // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            // parando o looping
                            return;
                        }
                    }

                }

                // se o baralho esiver vazio
                else if (deck.Count <= 0)
                {
                    // avisa que acabou as cartas - embaralha elas
                    Debug.Log("Acabou as cartas do baralho");

                    Shuffle();
                }
                break;
        }



    }

    /// <summary>
    /// quando clicar no discarte (botao) comprar a ultima carta jogada e mostrar a anterior
    /// </summary>
    public void PurchaseDiscard()
    {

        // verificando qual jogador e da rodada
        switch (rodadaDoJogador)
        {
            //jogador 1
            case 0:

                // verificando se ainda tem carta na baralho e se o jogador pode comprar a carta
                if (discardPile.Count >= 1 && compraCarta)
                {

                    // procurando a ultima carta descartada
                    for (int i = 0; i < discardPile.Count; i++)
                    {
                        // verificando que e a ultima carta
                        if (i == discardPile.Count - 1)
                        {

                            // salvando a ultima carta do discarte - definindo a penultima camo null
                            lastCard = discardPile[i];
                            penultima = null;

                            // verificando se nao e a ultima carta - salvando a penultima carta
                            if (i - 1 >= 0)
                            {
                                penultima = discardPile[i - 1];
                            }

                            // verificando todos os slots de cartas
                            for (int y = 0; y < slotsDisponiveisCartasPlayer1.Length; y++)
                            {
                                // se esse slot estiver vazio
                                if (slotsDisponiveisCartasPlayer1[y] == true)
                                {

                                    // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                                    lastCard.gameObject.SetActive(true);
                                    lastCard.handIndex = y;
                                    lastCard.gameObject.layer = layerPl1;

                                    // colocando a carta na posição e rotação certa do slot
                                    lastCard.transform.position = cardSlotsPlayer1[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer1[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte
                                    slotsDisponiveisCartasPlayer1[y] = false;
                                    discardPile.Remove(lastCard);

                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior - parando o looping
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio - parando o looping
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;
                                        return;                                     
                                    }

                                }

                            }

                        }

                    }

                }
                break;

            // jogador 2
            case 1:
                // verificando se ainda tem carta na baralho e se o jogador pode comprar a carta
                if (discardPile.Count >= 1 && compraCarta)
                {

                    // procurando a ultima carta descartada
                    for (int i = 0; i < discardPile.Count; i++)
                    {
                        // verificando que e a ultima carta
                        if (i == discardPile.Count - 1)
                        {

                            // salvando a ultima carta do discarte - definindo a penultima camo null
                            lastCard = discardPile[i];
                            penultima = null;

                            // verificando se nao e a ultima carta - salvando a penultima carta
                            if (i - 1 >= 0)
                            {
                                penultima = discardPile[i - 1];
                            }

                            // verificando todos os slots de cartas
                            for (int y = 0; y < slotsDisponiveisCartasPlayer2.Length; y++)
                            {
                                // se esse slot estiver vazio
                                if (slotsDisponiveisCartasPlayer2[y] == true)
                                {
                                    // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                                    lastCard.gameObject.SetActive(true);
                                    lastCard.handIndex = y;
                                    lastCard.gameObject.layer = layerPl2;

                                    // colocando a carta na posição e rotação certa do slot
                                    lastCard.transform.position = cardSlotsPlayer2[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer2[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte
                                    slotsDisponiveisCartasPlayer2[y] = false;
                                    discardPile.Remove(lastCard);
                                    
                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior - parando o looping
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio - parando o looping
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;
                                        return;
                                    }

                                }

                            }

                        }

                    }

                }
                break;

            // jogador 3
            case 2:
                // verificando se ainda tem carta na baralho e se o jogador pode comprar a carta
                if (discardPile.Count >= 1 && compraCarta)
                {

                    // procurando a ultima carta descartada
                    for (int i = 0; i < discardPile.Count; i++)
                    {
                        // verificando que e a ultima carta
                        if (i == discardPile.Count - 1)
                        {

                            // salvando a ultima carta do discarte - definindo a penultima camo null
                            lastCard = discardPile[i];
                            penultima = null;

                            // verificando se nao e a ultima carta - salvando a penultima carta
                            if (i - 1 >= 0)
                            {
                                penultima = discardPile[i - 1];
                            }

                            // verificando todos os slots de cartas
                            for (int y = 0; y < slotsDisponiveisCartasPlayer3.Length; y++)
                            {
                                // se esse slot estiver vazio
                                if (slotsDisponiveisCartasPlayer3[y] == true)
                                {
                                    // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                                    lastCard.gameObject.SetActive(true);
                                    lastCard.handIndex = y;
                                    lastCard.gameObject.layer = layerPl3;

                                    // colocando a carta na posição e rotação certa do slot
                                    lastCard.transform.position = cardSlotsPlayer3[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer3[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte
                                    slotsDisponiveisCartasPlayer3[y] = false;
                                    discardPile.Remove(lastCard);
                                    
                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior - parando o looping
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio - parando o looping
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;
                                        return;
                                    }

                                }

                            }

                        }

                    }

                }
                break;

            // jogador 4
            case 3:
                // verificando se ainda tem carta na baralho e se o jogador pode comprar a carta
                if (discardPile.Count >= 1 && compraCarta)
                {

                    // procurando a ultima carta descartada
                    for (int i = 0; i < discardPile.Count; i++)
                    {
                        // verificando que e a ultima carta
                        if (i == discardPile.Count - 1)
                        {

                            // salvando a ultima carta do discarte - definindo a penultima camo null
                            lastCard = discardPile[i];
                            penultima = null;

                            // verificando se nao e a ultima carta - salvando a penultima carta
                            if (i - 1 >= 0)
                            {
                                penultima = discardPile[i - 1];
                            }

                            // verificando todos os slots de cartas
                            for (int y = 0; y < slotsDisponiveisCartasPlayer4.Length; y++)
                            {
                                // se esse slot estiver vazio
                                if (slotsDisponiveisCartasPlayer4[y] == true)
                                {
                                    // vou mostrar a carta - salvar em qual slot ela esta - definindo a camada da carta para a do player
                                    lastCard.gameObject.SetActive(true);
                                    lastCard.handIndex = y;
                                    lastCard.gameObject.layer = layerPl4;

                                    // colocando a carta na posição e rotação certa do slot
                                    lastCard.transform.position = cardSlotsPlayer4[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer4[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte
                                    slotsDisponiveisCartasPlayer4[y] = false;
                                    discardPile.Remove(lastCard);
                                    
                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior - parando o looping
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio - parando o looping
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;
                                        return;
                                    }

                                }

                            }

                        }

                    }

                }
                break;
        }
        
    }

    /// <summary>
    /// função para embaralhar as cartas que estão no baralho de descarte
    /// </summary>
    public void Shuffle()
    {
        // verificando se tem cartas no descarte
        if (discardPile.Count >= 1)
        {

            // executando um looping pelo numero de cartas que tem no descarte
            foreach (Card card in discardPile)
            {

                // adicionamdo a carta no baralho
                deck.Add(card);
            }
            // esvaziando a Array
            discardPile.Clear();
        }
    }

    /// <summary>
    /// função criada para quando uma carta for jogada para a pilha de discarte
    /// </summary>
    /// <param name="card"></param>
    public void MoveToDiscardPile(Card card)
    {

        // adicionando a carta na pilha de discarte
        discardPile.Add(card);

        // para trocar a imagem do botao para a da carta que foi descartada
        buttonD.GetComponent<Image>().sprite = card.GetComponent<SpriteRenderer>().sprite;

        // CRIAR A PARTE DA ROLAGEM DE DADO E TABELA
        FindObjectOfType<EventTable>().Evento();
        
        // liberando o jogador para comprar carta - impedindo o jogador de discartar mais cartas - escondendo a carta da dysfunção
        compraCarta = true;
        discartaCarta = false;
        resetDysfuncao = true;

        // passando para o proximo jogador jogar
        rodadaDoJogador++;

        // verificando se foi o ultimo jogador que jogou
        if (rodadaDoJogador == numPlayerNoJogo)
        {
            // recomeçando a rodada
            rodadaDoJogador = 0;

            // e aumentando uma rodada no contador de turnos
            rodada++;
            contador.text = "Turno: " + rodada;

            // verificando se chegou na ultima rodada
            if (rodada == rodadaFinal)
            {
                //MODOS DE "PAUSAR O JOGO"
                //rodadaDoJogador = 10;
                compraCarta = false;

                // chamando o parte dois do jogo (montar o tratamento)
                SetUpTreatment();
            }

        }
        

    }

    /// <summary>
    /// Aqui vai ser desemvolvida a parte onde os jogadores teram um tempo para montar o tratamento deles para apresentar
    /// para o mestre
    /// </summary>
    public void SetUpTreatment()
    {
        Debug.Log("COMEÇA A PARTE DOIS DO JOGO (MONTAR O TRATAMENTO COM AS CARTAS QUE TEM)");
    }

}
