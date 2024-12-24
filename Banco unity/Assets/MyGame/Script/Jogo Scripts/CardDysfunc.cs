using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDysfunc : MonoBehaviour
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
        cartaFrente = gameObject.GetComponent<SpriteRenderer>().sprite;

        // salvando a imagem da carta do fundo
        gameObject.GetComponent<SpriteRenderer>().sprite = cartaFundo;
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

                    gameObject.GetComponent<SpriteRenderer>().sprite = cartaFundo;

                    cartaVirada = false;

                    gm.resetDysfuncao = false;
                }
                break;
            // se for o player 3 e a reset da carta for verdadeiro e a layer for a do player 2 vira a carta reseta o timer e as variaveis de verificação
            case 2:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl2))
                {
                    timer = 0;

                    gameObject.GetComponent<SpriteRenderer>().sprite = cartaFundo;

                    cartaVirada = false;

                    gm.resetDysfuncao = false;
                }
                break;
            // se for o player 4 e a reset da carta for verdadeiro e a layer for a do player 3 vira a carta reseta o timer e as variaveis de verificação
            case 3:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl3))
                {
                    timer = 0;

                    gameObject.GetComponent<SpriteRenderer>().sprite = cartaFundo;

                    cartaVirada = false;

                    gm.resetDysfuncao = false;
                }
                break;
            // se for o player 1 e a reset da carta for verdadeiro e a layer for a do player 4 vira a carta reseta o timer e as variaveis de verificação
            case 0:
                if (gm.resetDysfuncao && (gameObject.layer == gm.layerPl4))
                {
                    timer = 0;

                    gameObject.GetComponent<SpriteRenderer>().sprite = cartaFundo;

                    cartaVirada = false;

                    gm.resetDysfuncao = false;
                }
                break;
        }
    }

    /// <summary>
    /// quando o cursor do mouse entrar dentro do collaider desse script
    /// </summary>
    private void OnMouseEnter()
    {
        // se a carta estiver virada
        if (cartaVirada)
        {
            // alterando a imagem da carta expandida para a imagem da carta
            cartaEx.GetComponent<SpriteRenderer>().sprite = cartaFrente;
        }
    }

    /// <summary>
    /// quando o cursor do mouse sair de dentro do collaider desse script
    /// </summary>
    private void OnMouseExit()
    {
        // alterando a imagem da carta expandida para vazia
        cartaEx.GetComponent<SpriteRenderer>().sprite = null;
    }

    /// <summary>
    /// quando clikar com o mouse 
    /// </summary>
    private void OnMouseDown()
    {
        // setando o timer para zero
        timer = 0;
        
        // alterando a imagem da carta expandida para vazia
        cartaEx.GetComponent<SpriteRenderer>().sprite = null;

        // e alterando a carta para a imagem de fundo
        gameObject.GetComponent<SpriteRenderer>().sprite = cartaFundo;

        // avisando que a carta esta virada
        cartaVirada = false;
    }

    /// <summary>
    /// quando presionar o botão do mouse 
    /// </summary>
    private void OnMouseDrag ()
    {
        // verificando qual e o jogador da rodada
        switch (gm.rodadaDoJogador)
        {
            // caso for o jogador 1 verifica se eles esta precionando na sua carta assim aumente o tempo 
            case 0:
                if (gameObject.layer == gm.layerPl1)
                {
                    timer += Time.deltaTime;
                    
                    // quando passar o tempo de espera vira a carta para frente
                    if (timer >= tempEspera)
                    {
                        // mostrando a carta expandida
                        gameObject.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        // alterando a imagem da carta expandida para a da carta
                        cartaEx.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        cartaVirada = true;

                    }
                }

                break;
            // caso for o jogador 2 verifica se eles esta precionando na sua carta assim aumente o tempo
            case 1:
                if (gameObject.layer == gm.layerPl2)
                {
                    timer += Time.deltaTime;

                    // quando passar o tempo de espera vira a carta para frente
                    if (timer >= tempEspera)
                    {
                        // mostrando a carta expandida
                        gameObject.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        // alterando a imagem da carta expandida para a da carta
                        cartaEx.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        cartaVirada = true;

                    }
                }
                break;
            // caso for o jogador 3 verifica se eles esta precionando na sua carta assim aumente o tempo
            case 2:
                if (gameObject.layer == gm.layerPl3)
                {
                    timer += Time.deltaTime;
                    // quando passar o tempo de espera vira a carta para frente
                    if (timer >= tempEspera)
                    {
                        // mostrando a carta expandida
                        gameObject.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        // alterando a imagem da carta expandida para a da carta
                        cartaEx.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        cartaVirada = true;

                    }
                }
                break;
            // caso for o jogador 4 verifica se eles esta precionando na sua carta assim aumente o tempo
            case 3:
                if (gameObject.layer == gm.layerPl4)
                {
                    timer += Time.deltaTime;

                    // quando passar o tempo de espera vira a carta para frente
                    if (timer >= tempEspera)
                    {
                        // mostrando a carta expandida
                        gameObject.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        // alterando a imagem da carta expandida para a da carta
                        cartaEx.GetComponent<SpriteRenderer>().sprite = cartaFrente;

                        cartaVirada = true;

                    }
                }
                break;
        }

    }

}
