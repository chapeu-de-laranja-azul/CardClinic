using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
using UnityEngine.Networking.PlayerConnection;
using Unity.VisualScripting;
using ExitGames.Client.Photon;

public class CardDysfunc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // salvando a carta expandida
    public GameObject cartaEx;

    // variaveis para salvar os sprites da frete e do verso da carta
    public Sprite cartaFundo;
    private Sprite cartaFrente;

    // salvando o objeto Game manager
    private GameManager gm;

    // variavel para fazer verificação se a carta esta virada ou não
    private bool cartaVirada = false;
    private bool soltou = true;
    private bool saiu = false;

    // variaveis para criação de um timer
    [SerializeField] private float tempEspera;
    [SerializeField] private float timer;

    /// <summary>
    /// função que ativa inicialmente antes das outras
    /// </summary>
    private void Start()
    {
        // salvando o objeto GameManager em uma variavel
        gm = FindObjectOfType<GameManager>();

        // salvando qual e a imagem da carta da frente
        cartaFrente = gameObject.GetComponent<Image>().sprite;

        // salvando a imagem da carta do fundo
        gameObject.GetComponent<Image>().sprite = cartaFundo;
    }

    /// <summary>
    /// função que e chamada a cada frame do jogo
    /// </summary>
    public void Update()
    {

        // verificando quem e que vai jogar
        switch (gm.rodadaDoJogador)
        {
            // se for o player 2 e a reset da carta for verdadeiro e a layer for a do player 1 vira a carta reseta o timer e as variaveis de verificação 
            case 1:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl1))
                {
                    timer = 0;

                    gameObject.GetComponent<Image>().sprite = cartaFundo;

                    cartaEx.GetComponent<Image>().sprite = gm.imagemVazio;

                    cartaVirada = false;

                    soltou = true;

                    gm.resetDysfuncao = false;
                }
                break;
            // se for o player 3 e a reset da carta for verdadeiro e a layer for a do player 2 vira a carta reseta o timer e as variaveis de verificação
            case 2:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl2))
                {
                    timer = 0;

                    gameObject.GetComponent<Image>().sprite = cartaFundo;

                    cartaEx.GetComponent<Image>().sprite = gm.imagemVazio;
                    
                    cartaVirada = false;

                    soltou = true;

                    gm.resetDysfuncao = false;
                }
                break;
            // se for o player 4 e a reset da carta for verdadeiro e a layer for a do player 3 vira a carta reseta o timer e as variaveis de verificação
            case 3:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl3))
                {
                    timer = 0;

                    gameObject.GetComponent<Image>().sprite = cartaFundo;
                    
                    cartaEx.GetComponent<Image>().sprite = gm.imagemVazio;

                    cartaVirada = false;

                    soltou = true;

                    gm.resetDysfuncao = false;
                }
                break;
            // se for o player 1 e a reset da carta for verdadeiro e a layer for a do player 4 vira a carta reseta o timer e as variaveis de verificação
            case 0:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl4))
                {
                    timer = 0;

                    gameObject.GetComponent<Image>().sprite = cartaFundo;

                    cartaEx.GetComponent<Image>().sprite = gm.imagemVazio;

                    cartaVirada = false;

                    soltou = true;
                    
                    gm.resetDysfuncao = false;
                }
                break;
        }
        
        // verificando se esta sendo precionada na area dela
        if (!soltou && !saiu)
        {
            // um timer que constantemente cresce
            timer += Time.deltaTime;

            // verificando se foi presionado por tempo suficiente para virar a carta
            if (timer >= tempEspera)
            {
                // mostrando a carta expandida
                gameObject.GetComponent<Image>().sprite = cartaFrente;

                // alterando a imagem da carta expandida para a da carta
                cartaEx.GetComponent<Image>().sprite = cartaFrente;

                // indicando que a carta foi virada
                cartaVirada = true;

                // fechando o painel do numero aleatorio do dado
                gm.ClosePainelNum();
            }
        }

    }

    /// <summary>
    /// quando o cursor do mouse entrar dentro do collaider desse script
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {

        // se a carta estiver virada
        if (cartaVirada)
        {
            // alterando a imagem da carta expandida para a imagem da carta
            cartaEx.GetComponent<Image>().sprite = cartaFrente;
        }

        // Indicando que o cursor do mouse entrou na area da carta
        saiu = false;
    }

    /// <summary>
    /// quando o cursor do mouse sair de dentro do collaider desse script
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // alterando a imagem da carta expandida para vazia
        cartaEx.GetComponent<Image>().sprite = gm.imagemVazio;
        
        // resetando o timer
        timer = 0;

        // indicando que o cursor do mouse saiu da area da carta
        saiu = true;
    }

    /// <summary>
    /// quando o botão do mouse mover para baixo 
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // verificando qual jogador e da rodada
        switch (gm.rodadaDoJogador)
        {
            case 0:
                // verificando a layer
                if (gameObject.layer == gm.layerPl1)
                {
                    // indicando que começou a ser pressionado o botão
                    soltou = false;
                }
                break;
            case 1:
                // verificando a layer
                if (gameObject.layer == gm.layerPl2)
                {
                    // indicando que começou a ser pressionado o botão
                    soltou = false;
                }
                break;
            case 2:
                // verificando a layer
                if (gameObject.layer == gm.layerPl3)
                {
                    // indicando que começou a ser pressionado o botão
                    soltou = false;
                }
                break;
            case 3:
                // verificando a layer
                if (gameObject.layer == gm.layerPl4)
                {
                    // indicando que começou a ser pressionado o botão
                    soltou = false;
                }
                break;
        }

        // Verificando se a carta esta virada
        if (cartaVirada)
        {
            // se estiver ele vira a carta para baixo
            soltou = true;
            cartaVirada = false;
            gameObject.GetComponent<Image>().sprite = cartaFundo;
            cartaEx.GetComponent<Image>().sprite = gm.imagemVazio;
            timer = 0;
        }
    }

    /// <summary>
    /// quando o botão do mouse mover para baixofor solto
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        // se não for precionado tempo suficiente 
        if (timer < tempEspera)
        {
            // zera o timer e indica que ele parou de pressionar
            timer = 0;
            soltou = true;
        }
        // se não
        else
        {
            // indica que a carta esta virada e que ele parou de pressionar e zera o timer
            cartaVirada = true;
            soltou = true;
            timer = 0;
        }
    }
}
