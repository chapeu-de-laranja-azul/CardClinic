using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Variaveis utilizadas no GameManager
    // criando listas para o baralho - discarte - disfunções - players 
    public List<CardDysfunc> dysfunctions = new List<CardDysfunc>();
    public List<CardDysfunc> cardsDysfunctions = new List<CardDysfunc>();
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> deckPlayer1 = new List<Card>();
    public List<Card> deckPlayer2 = new List<Card>();
    public List<Card> deckPlayer3 = new List<Card>();
    public List<Card> deckPlayer4 = new List<Card>();

    // as arrays para salvar as posições de todas as cartas que estarão no jogo - e do RectTransform dos nomes dos players
    public Transform[] dysfunSlots;
    public Transform[] cardSlotsPlayer1;
    public Transform[] cardSlotsPlayer2;
    public Transform[] cardSlotsPlayer3;
    public Transform[] cardSlotsPlayer4;
    [SerializeField] private RectTransform[] nicknamesTransform;

    // para verificar se os slots estao vazios ou nao
    public bool[] slotsDisponiveisCartasPlayer1;
    public bool[] slotsDisponiveisCartasPlayer2;
    public bool[] slotsDisponiveisCartasPlayer3;
    public bool[] slotsDisponiveisCartasPlayer4;
    // verificadores para o controle de quando os jogadores podem discartar ou comprar cartas - verificador para resetar a carta de dysfunção ao jogador passar o turno
    public bool compraCarta = false;
    public bool discartaCarta = false;
    public bool resetDysfuncao = false;
    public bool rolarDado = true;

    // quantas cartas vai ser entregue inicialmente - quantos jogadores tem no jogo - controle de qual jogador vai jogar - e o numero que foi rolado no dado
    public int startingCards;
    public int numPlayerNoJogo;
    public int rodadaDoJogador = 0;
    [SerializeField] private int numAleatorio;

    // qual camada e a dos players - e uma variavel para guardar o numero de rodadas jogadas e a rodada final
    public int layerPl1 = 7;
    public int layerPl2 = 8;
    public int layerPl3 = 9;
    public int layerPl4 = 10;
    public int rodada = 0;
    public int rodadaFinal;
    public int[] creditos = new int[4];

    // salvando a ultima carta descartada - salvando a penultima carta descartada
    private Card lastCard;
    private Card penultima;

    // para armazenar o botao de descarte
    public Button buttonD;

    // salvando o texto do contador, nicks, creditos, numero do dado, texto do evento
    public TextMeshProUGUI contador;
    [SerializeField] private TextMeshProUGUI[] playersNicknames;
    [SerializeField] private TextMeshProUGUI[] textCreditos;
    [SerializeField] private TextMeshProUGUI numeroDado;
    [SerializeField] private TextMeshProUGUI textoEvento;

    // salvando uma imagem vazia de carta
    public Sprite imageSemcard;

    // salvando o painel para mostrar o evento aleatorio - salvando vectors 3 e 2 e quaternion para mover os nomes
    [SerializeField] private GameObject painelNum;

    [SerializeField] private Vector3[] nicknamesVector3T;
    [SerializeField] private Vector2[] nicknamesVector2P;
    [SerializeField] private Quaternion[] nicknamesQuaternion;
    
    
    #endregion

    /// <summary>
    /// classe executada quando comeca o jogo, 1 vez e primeira que as outras
    /// </summary>
    public void Start()
    {
        // Alterando os nomes dos players (Depois fazer a alteração com o escripiti da conecção)
        playersNicknames[0].text = "Jogador 1";
        playersNicknames[1].text = "Jogador 2";
        playersNicknames[2].text = "Jogador 3";
        playersNicknames[3].text = "Jogador 4";

        // Salvando a informação das posição iniciais dos nomes dos players
        for(int i = 0; i < nicknamesTransform.Length; i++)
        {
            nicknamesVector3T[i] = nicknamesTransform[i].position;
            nicknamesQuaternion[i] = nicknamesTransform[i].rotation;
            nicknamesVector2P[i] = nicknamesTransform[i].pivot;
        }

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
                        deckPlayer1.Add(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD.gameObject.layer = layerPl1;
                    randCardD.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD.transform.position = dysfunSlots[z].position;
                    randCardD.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho de disfunções - e adicionando em cardsDysfunctions
                    dysfunctions.Remove(randCardD);
                    cardsDysfunctions.Add(randCardD);

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
                        deckPlayer2.Add(randCard); 
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD1 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD1.gameObject.layer = layerPl2;
                    randCardD1.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD1.transform.position = dysfunSlots[z].position;
                    randCardD1.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho de disfunções  - e adicionando em cardsDysfunctions
                    dysfunctions.Remove(randCardD1);
                    cardsDysfunctions.Add(randCardD1);
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
                        deckPlayer3.Add(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD2 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD2.gameObject.layer = layerPl3;
                    randCardD2.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD2.transform.position = dysfunSlots[z].position;
                    randCardD2.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho de disfunções - e adicionando em cardsDysfunctions
                    dysfunctions.Remove(randCardD2);
                    cardsDysfunctions.Add(randCardD2);
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
                        deckPlayer4.Add(randCard);
                    }

                    // parte para colocar a carta de dysfuncao no jogo
                    CardDysfunc randCardD3 = dysfunctions[Random.Range(0, dysfunctions.Count)];

                    // Definindo a camada do player - mostrando ela
                    randCardD3.gameObject.layer = layerPl4;
                    randCardD3.gameObject.SetActive(true);

                    //Colocando a carta na posição e rotação certa do slot
                    randCardD3.transform.position = dysfunSlots[z].position;
                    randCardD3.transform.rotation = dysfunSlots[z].rotation;

                    // removendo ela do baralho - e adicionando em cardsDysfunctions
                    dysfunctions.Remove(randCardD3);
                    cardsDysfunctions.Add(randCardD3);
                    break;
            }
        
        }
        
    }

    /// <summary>
    /// Função para rolagem de dado e para os eventos da tabela
    /// </summary>
    public void RollDice()
    {
        // verificando se pode rolar o dado
        if (rolarDado)
        {
            // rolando o dado
            numAleatorio = Random.Range(1, 20);

            // escrevendo o numero do dado - e mostrando o painel
            numeroDado.text = numAleatorio.ToString();
            painelNum.SetActive(true); 

            // executando o evento que caiu no dado
            switch (numAleatorio)
            {
                case 1:

                    textoEvento.text = "Você perdeu 10 creditos";
                    textoEvento.color = Color.red;
                    numeroDado.color = Color.red;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] - 10;
                    
                    if(creditos[rodadaDoJogador] < 0)
                    {
                        creditos[rodadaDoJogador] = 0;
                    }

                    break;
                case 2:

                    textoEvento.text = "Você perdeu 5 creditos";
                    textoEvento.color = Color.red;
                    numeroDado.color = Color.red;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] - 5;

                    if (creditos[rodadaDoJogador] < 0)
                    {
                        creditos[rodadaDoJogador] = 0;
                    }

                    break;
                case 3:

                    textoEvento.text = "Você perdeu 3 creditos";
                    textoEvento.color = Color.red;
                    numeroDado.color = Color.red;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] - 3;

                    if (creditos[rodadaDoJogador] < 0)
                    {
                        creditos[rodadaDoJogador] = 0;
                    }

                    break;
                case 4:

                    textoEvento.text = "Você perdeu 2 creditos";
                    textoEvento.color = Color.red;
                    numeroDado.color = Color.red;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] - 2;

                    if (creditos[rodadaDoJogador] < 0)
                    {
                        creditos[rodadaDoJogador] = 0;
                    }

                    break;
                case 5 or 6:

                    textoEvento.text = "Você perdeu 1 creditos";
                    textoEvento.color = Color.red;
                    numeroDado.color = Color.red;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] - 1;

                    if (creditos[rodadaDoJogador] < 0)
                    {
                        creditos[rodadaDoJogador] = 0;
                    }

                    break;
                case 7 or 8 or 9:


                    textoEvento.text = "Você não ganhou creditos";
                    textoEvento.color = Color.gray;
                    numeroDado.color = Color.gray;

                    break;
                case 10 or 11 or 12:

                    textoEvento.text = "Você ganhou 1 creditos";
                    textoEvento.color = Color.green;
                    numeroDado.color = Color.green;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] + 1;

                    break;
                case 13 or 14 or 15:

                    textoEvento.text = "Você ganhou 3 creditos";
                    textoEvento.color = Color.green;
                    numeroDado.color = Color.green;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] + 3;

                    break;
                case 16 or 17:

                    textoEvento.text = "Você ganhou 4 creditos";
                    textoEvento.color = Color.green;
                    numeroDado.color = Color.green;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] + 4;

                    break;
                case 18 or 19:

                    textoEvento.text = "Você ganhou 5 creditos";
                    textoEvento.color = Color.green;
                    numeroDado.color = Color.green;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] + 5;

                    break;
                case 20:

                    textoEvento.text = "Você ganhou 15 cretidos";
                    textoEvento.color = Color.yellow;
                    numeroDado.color = Color.yellow;

                    creditos[rodadaDoJogador] = creditos[rodadaDoJogador] + 15;

                    break;
            }

            // alterando o valor dos creditos 
            textCreditos[rodadaDoJogador].text = "X" + Convert.ToString(creditos[rodadaDoJogador]);

            // liberando o jogador para comprar carta - Desativando rolar o dado
            compraCarta = true;
            rolarDado = false;
        }
    }

    public void ClosePainelNum()
    {
        // desativando painel de numero do dado
        painelNum.SetActive(false);
    }

    /// <summary>
    /// classe para quando for clicado o botao de compra de carta do baralho
    /// </summary>
    public void DrawCard()
    {
        // desativando painel de numero do dado
        painelNum.SetActive(false);

        // verificando se ainda tem carta na baralho e se o jogador pode comprar a carta
        if (deck.Count >= 1 && compraCarta)
        {
            // salvando uma carta aleatoria que esta no baralho
            Card randCard = deck[Random.Range(0, deck.Count)];

            // verificando qual jogador e da rodada
            switch (rodadaDoJogador)
            {
                // jogador 1
                case 0:
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
                            deckPlayer1.Add(randCard);

                            // impedindo do jogador de comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            //parando o looping
                            return;
                        }
                    }
                    break;
                // jogador 2
                case 1:
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
                            randCard.transform.position = cardSlotsPlayer1[i].position;
                            randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer2[i] = false;
                            deck.Remove(randCard);
                            deckPlayer2.Add(randCard);

                            // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            // parando o looping
                            return;
                        }
                    }
                    break;
                // jogador 3
                case 2:
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
                            randCard.transform.position = cardSlotsPlayer1[i].position;
                            randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer3[i] = false;
                            deck.Remove(randCard);
                            deckPlayer3.Add(randCard);

                            // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            // parando o looping
                            return;
                        }
                    }
                    break;
                // jogador 4
                case 3:
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
                            randCard.transform.position = cardSlotsPlayer1[i].position;
                            randCard.transform.rotation = cardSlotsPlayer1[i].rotation;

                            // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                            randCard.hasBeenPlayed = false;

                            // avisando que o slot esta ocupado - removendo a carta do deck
                            slotsDisponiveisCartasPlayer4[i] = false;
                            deck.Remove(randCard);
                            deckPlayer4.Add(randCard);

                            // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                            compraCarta = false;
                            discartaCarta = true;

                            // parando o looping
                            return;
                        }
                    }
                    break;
            }
        }
        // se o baralho esiver vazio
        else if (deck.Count <= 0)
        {
            // avisa que acabou as carta - embaralhando elas
            Debug.Log("Acabou as cartas do baralho");

            Shuffle();
        }



    }

    /// <summary>
    /// quando clicar no discarte (botao) comprar a ultima carta jogada e mostrar a anterior
    /// </summary>
    public void PurchaseDiscard()
    {
        // desativando painel de numero do dado
        painelNum.SetActive(false);

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

                    // verificando qual jogador e da rodada
                    switch (rodadaDoJogador)
                    {
                        //jogador 1
                        case 0:
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

                                    // avisando que o slot esta ocupado - removendo a carta do discarte - adicionando a carta no Deck do player
                                    slotsDisponiveisCartasPlayer1[y] = false;
                                    discardPile.Remove(lastCard);
                                    deckPlayer1.Add(lastCard);

                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;

                                        // parando o looping
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;

                                        // parando o looping
                                        return;
                                    }
                                }
                            }
                            break;

                        // jogador 2
                        case 1:
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
                                    lastCard.transform.position = cardSlotsPlayer1[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer1[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte - adicionando a carta no Deck do player
                                    slotsDisponiveisCartasPlayer2[y] = false;
                                    discardPile.Remove(lastCard);
                                    deckPlayer2.Add(lastCard);
                                    
                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;

                                        // parando o looping
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;

                                        // parando o looping
                                        return;
                                    }
                                }
                            }
                            break;

                        // jogador 3
                        case 2:
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
                                    lastCard.transform.position = cardSlotsPlayer1[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer1[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte - adicionando a carta no Deck do player
                                    slotsDisponiveisCartasPlayer3[y] = false;
                                    discardPile.Remove(lastCard);
                                    deckPlayer3.Add(lastCard);
                                    
                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;

                                        // parando o looping
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;

                                        // parando o looping
                                        return;
                                    }
                                }
                            }
                            break;

                        // jogador 4
                        case 3:
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
                                    lastCard.transform.position = cardSlotsPlayer1[y].position;
                                    lastCard.transform.rotation = cardSlotsPlayer1[y].rotation;

                                    // Ativando para pode clicar na carta (verificador para evitar varios clicks)
                                    lastCard.hasBeenPlayed = false;

                                    // avisando que o slot esta ocupado - removendo a carta do discarte - adicionando a carta no Deck do player
                                    slotsDisponiveisCartasPlayer4[y] = false;
                                    discardPile.Remove(lastCard);
                                    deckPlayer4.Add(lastCard);
                                    
                                    // impedindo do jogador comprar outras cartas - liberando o jogador para discartar uma carta
                                    compraCarta = false;
                                    discartaCarta = true;

                                    // verificando se nao contem penultima carta
                                    if (penultima != null)
                                    {
                                        // substituindo a imagem para a carta anterior
                                        buttonD.GetComponent<Image>().sprite = penultima.GetComponent<SpriteRenderer>().sprite;

                                        // parando o looping
                                        return;
                                    }
                                    else
                                    {
                                        // colocando a imagem de discarte vazio
                                        buttonD.GetComponent<Image>().sprite = imageSemcard;

                                        // parando o looping
                                        return;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
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

        // impedindo o jogador de discartar mais cartas - escondendo a carta da dysfunção - Permitindo rolar o dado da tabela
        discartaCarta = false;
        resetDysfuncao = true;
        rolarDado = true;

        // Girando os deks quando o player passar a vez
        switch (rodadaDoJogador)
        {
            case 0:
                // Player 1
                for (int i = 0; i < deckPlayer1.Count; i++)
                {
                    deckPlayer1[i].transform.position = cardSlotsPlayer4[deckPlayer1[i].handIndex].position;
                    deckPlayer1[i].transform.rotation = cardSlotsPlayer4[deckPlayer1[i].handIndex].rotation;
                }
                // Player 2
                for (int i = 0; i < deckPlayer2.Count; i++)
                {
                    deckPlayer2[i].transform.position = cardSlotsPlayer1[deckPlayer2[i].handIndex].position;
                    deckPlayer2[i].transform.rotation = cardSlotsPlayer1[deckPlayer2[i].handIndex].rotation;
                }
                // Player 3
                for (int i = 0;i < deckPlayer3.Count; i++)
                {
                    deckPlayer3[i].transform.position = cardSlotsPlayer2[deckPlayer3[i].handIndex].position;
                    deckPlayer3[i].transform.rotation = cardSlotsPlayer2[deckPlayer3[i].handIndex].rotation;
                }
                // Player 4
                for (int i = 0; i < deckPlayer4.Count; i++)
                {
                    deckPlayer4[i].transform.position = cardSlotsPlayer3[deckPlayer4[i].handIndex].position;
                    deckPlayer4[i].transform.rotation = cardSlotsPlayer3[deckPlayer4[i].handIndex].rotation;
                }
                // dysfunção player 1
                cardsDysfunctions[0].transform.position = dysfunSlots[3].position;
                cardsDysfunctions[0].transform.rotation = dysfunSlots[3].rotation;
                
                // dysfunção player 2
                cardsDysfunctions[1].transform.position = dysfunSlots[0].position;
                cardsDysfunctions[1].transform.rotation = dysfunSlots[0].rotation;
                
                // dysfunção player 3
                cardsDysfunctions[2].transform.position = dysfunSlots[1].position;
                cardsDysfunctions[2].transform.rotation = dysfunSlots[1].rotation;
                
                // dysfunção player 4
                cardsDysfunctions[3].transform.position = dysfunSlots[2].position;
                cardsDysfunctions[3].transform.rotation = dysfunSlots[2].rotation;
                
                // Alterando os nomes dos players de posições
                nicknamesTransform[0].position = nicknamesVector3T[3];
                nicknamesTransform[0].rotation = nicknamesQuaternion[3];
                nicknamesTransform[0].pivot = nicknamesVector2P[3];

                nicknamesTransform[1].position = nicknamesVector3T[0];
                nicknamesTransform[1].rotation = nicknamesQuaternion[0];
                nicknamesTransform[1].pivot = nicknamesVector2P[0];

                nicknamesTransform[2].position = nicknamesVector3T[1];
                nicknamesTransform[2].rotation = nicknamesQuaternion[1];
                nicknamesTransform[2].pivot = nicknamesVector2P[1];

                nicknamesTransform[3].position = nicknamesVector3T[2];
                nicknamesTransform[3].rotation = nicknamesQuaternion[2];
                nicknamesTransform[3].pivot = nicknamesVector2P[2];

                break;
            case 1:
                for (int i = 0; i < deckPlayer1.Count; i++)
                {
                    deckPlayer1[i].transform.position = cardSlotsPlayer3[deckPlayer1[i].handIndex].position;
                    deckPlayer1[i].transform.rotation = cardSlotsPlayer3[deckPlayer1[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer2.Count; i++)
                {
                    deckPlayer2[i].transform.position = cardSlotsPlayer4[deckPlayer2[i].handIndex].position;
                    deckPlayer2[i].transform.rotation = cardSlotsPlayer4[deckPlayer2[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer3.Count; i++)
                {
                    deckPlayer3[i].transform.position = cardSlotsPlayer1[deckPlayer3[i].handIndex].position;
                    deckPlayer3[i].transform.rotation = cardSlotsPlayer1[deckPlayer3[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer4.Count; i++)
                {
                    deckPlayer4[i].transform.position = cardSlotsPlayer2[deckPlayer4[i].handIndex].position;
                    deckPlayer4[i].transform.rotation = cardSlotsPlayer2[deckPlayer4[i].handIndex].rotation;
                }
                // dysfunção player 1
                cardsDysfunctions[0].transform.position = dysfunSlots[2].position;
                cardsDysfunctions[0].transform.rotation = dysfunSlots[2].rotation;
                // dysfunção player 2
                cardsDysfunctions[1].transform.position = dysfunSlots[3].position;
                cardsDysfunctions[1].transform.rotation = dysfunSlots[3].rotation;
                // dysfunção player 3
                cardsDysfunctions[2].transform.position = dysfunSlots[0].position;
                cardsDysfunctions[2].transform.rotation = dysfunSlots[0].rotation;
                // dysfunção player 4
                cardsDysfunctions[3].transform.position = dysfunSlots[1].position;
                cardsDysfunctions[3].transform.rotation = dysfunSlots[1].rotation;

                // Alterando os nomes dos players de posições
                nicknamesTransform[0].position = nicknamesVector3T[2];
                nicknamesTransform[0].rotation = nicknamesQuaternion[2];
                nicknamesTransform[0].pivot = nicknamesVector2P[2];

                nicknamesTransform[1].position = nicknamesVector3T[3];
                nicknamesTransform[1].rotation = nicknamesQuaternion[3];
                nicknamesTransform[1].pivot = nicknamesVector2P[3];

                nicknamesTransform[2].position = nicknamesVector3T[0];
                nicknamesTransform[2].rotation = nicknamesQuaternion[0];
                nicknamesTransform[2].pivot = nicknamesVector2P[0];

                nicknamesTransform[3].position = nicknamesVector3T[1];
                nicknamesTransform[3].rotation = nicknamesQuaternion[1];
                nicknamesTransform[3].pivot = nicknamesVector2P[1];
                break;
            case 2:
                for (int i = 0; i < deckPlayer1.Count; i++)
                {
                    deckPlayer1[i].transform.position = cardSlotsPlayer2[deckPlayer1[i].handIndex].position;
                    deckPlayer1[i].transform.rotation = cardSlotsPlayer2[deckPlayer1[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer2.Count; i++)
                {
                    deckPlayer2[i].transform.position = cardSlotsPlayer3[deckPlayer2[i].handIndex].position;
                    deckPlayer2[i].transform.rotation = cardSlotsPlayer3[deckPlayer2[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer3.Count; i++)
                {
                    deckPlayer3[i].transform.position = cardSlotsPlayer4[deckPlayer3[i].handIndex].position;
                    deckPlayer3[i].transform.rotation = cardSlotsPlayer4[deckPlayer3[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer4.Count; i++)
                {
                    deckPlayer4[i].transform.position = cardSlotsPlayer1[deckPlayer4[i].handIndex].position;
                    deckPlayer4[i].transform.rotation = cardSlotsPlayer1[deckPlayer4[i].handIndex].rotation;
                }
                // dysfunção player 1
                cardsDysfunctions[0].transform.position = dysfunSlots[1].position;
                cardsDysfunctions[0].transform.rotation = dysfunSlots[1].rotation;
                // dysfunção player 2
                cardsDysfunctions[1].transform.position = dysfunSlots[2].position;
                cardsDysfunctions[1].transform.rotation = dysfunSlots[2].rotation;
                // dysfunção player 3
                cardsDysfunctions[2].transform.position = dysfunSlots[3].position;
                cardsDysfunctions[2].transform.rotation = dysfunSlots[3].rotation;
                // dysfunção player 4
                cardsDysfunctions[3].transform.position = dysfunSlots[0].position;
                cardsDysfunctions[3].transform.rotation = dysfunSlots[0].rotation;

                // Alterando os nomes dos players de posições
                nicknamesTransform[0].position = nicknamesVector3T[1];
                nicknamesTransform[0].rotation = nicknamesQuaternion[1];
                nicknamesTransform[0].pivot = nicknamesVector2P[1];

                nicknamesTransform[1].position = nicknamesVector3T[2];
                nicknamesTransform[1].rotation = nicknamesQuaternion[2];
                nicknamesTransform[1].pivot = nicknamesVector2P[2];

                nicknamesTransform[2].position = nicknamesVector3T[3];
                nicknamesTransform[2].rotation = nicknamesQuaternion[3];
                nicknamesTransform[2].pivot = nicknamesVector2P[3];

                nicknamesTransform[3].position = nicknamesVector3T[0];
                nicknamesTransform[3].rotation = nicknamesQuaternion[0];
                nicknamesTransform[3].pivot = nicknamesVector2P[0];
                break;
            case 3:
                for (int i = 0; i < deckPlayer1.Count; i++)
                {
                    deckPlayer1[i].transform.position = cardSlotsPlayer1[deckPlayer1[i].handIndex].position;
                    deckPlayer1[i].transform.rotation = cardSlotsPlayer1[deckPlayer1[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer2.Count; i++)
                {
                    deckPlayer2[i].transform.position = cardSlotsPlayer2[deckPlayer2[i].handIndex].position;
                    deckPlayer2[i].transform.rotation = cardSlotsPlayer2[deckPlayer2[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer3.Count; i++)
                {
                    deckPlayer3[i].transform.position = cardSlotsPlayer3[deckPlayer3[i].handIndex].position;
                    deckPlayer3[i].transform.rotation = cardSlotsPlayer3[deckPlayer3[i].handIndex].rotation;
                }
                for (int i = 0; i < deckPlayer4.Count; i++)
                {
                    deckPlayer4[i].transform.position = cardSlotsPlayer4[deckPlayer4[i].handIndex].position;
                    deckPlayer4[i].transform.rotation = cardSlotsPlayer4[deckPlayer4[i].handIndex].rotation;
                }
                // dysfunção player 1
                cardsDysfunctions[0].transform.position = dysfunSlots[0].position;
                cardsDysfunctions[0].transform.rotation = dysfunSlots[0].rotation;
                // dysfunção player 2
                cardsDysfunctions[1].transform.position = dysfunSlots[1].position;
                cardsDysfunctions[1].transform.rotation = dysfunSlots[1].rotation;
                // dysfunção player 3
                cardsDysfunctions[2].transform.position = dysfunSlots[2].position;
                cardsDysfunctions[2].transform.rotation = dysfunSlots[2].rotation;
                // dysfunção player 4
                cardsDysfunctions[3].transform.position = dysfunSlots[3].position;
                cardsDysfunctions[3].transform.rotation = dysfunSlots[3].rotation;

                // Alterando os nomes dos players de posições
                nicknamesTransform[0].position = nicknamesVector3T[0];
                nicknamesTransform[0].rotation = nicknamesQuaternion[0];
                nicknamesTransform[0].pivot = nicknamesVector2P[0];

                nicknamesTransform[1].position = nicknamesVector3T[1];
                nicknamesTransform[1].rotation = nicknamesQuaternion[1];
                nicknamesTransform[1].pivot = nicknamesVector2P[1];

                nicknamesTransform[2].position = nicknamesVector3T[2];
                nicknamesTransform[2].rotation = nicknamesQuaternion[2];
                nicknamesTransform[2].pivot = nicknamesVector2P[2];

                nicknamesTransform[3].position = nicknamesVector3T[3];
                nicknamesTransform[3].rotation = nicknamesQuaternion[3];
                nicknamesTransform[3].pivot = nicknamesVector2P[3];
                break;
        }

        // passando para o proximo jogador jogar
        rodadaDoJogador++;

        // verificando se foi o ultimo jogador que jogou
        if (rodadaDoJogador == numPlayerNoJogo)
        {
            // recomeçando a rodada
            rodadaDoJogador = 0;

            // Adicionando para os jogadores os creditos semanais
            for(int i = 0; i < creditos.Length; i++)
            {
                creditos[i] = creditos[i] + 5;
                textCreditos[i].text = "X" + Convert.ToString(creditos[i]);
            }
            // e aumentando uma rodada no contador de turnos
            rodada++;
            contador.text = "Semana: " + rodada;

            // verificando se chegou na ultima rodada
            if (rodada == rodadaFinal)
            {
                //MODOS DE "PAUSAR O JOGO"
                rodadaDoJogador = 10;

                // chamando o parte dois do jogo (montar o tratamento)
                SetUpTreatment();
            }

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
    /// Aqui vai ser desemvolvida a parte onde os jogadores teram um tempo para montar o tratamento deles para apresentar
    /// para o mestre
    /// </summary>
    public void SetUpTreatment()
    {
        Debug.Log("COMEÇA A PARTE DOIS DO JOGO (MONTAR O TRATAMENTO COM AS CARTAS QUE TEM)");
    }

}
