using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       //para utilizar entradas de Input
using UnityEngine.SceneManagement;          //para utilizar controles de scena

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial, painelOpcoes, painelLobby, painelSala;

    private Conn conn;

    void Start()
    {
        conn = GetComponent<Conn>();
    }
    public void Jogar()
    {
        conn.CriarSala();
        Debug.Log("jogou");
        //SceneManager.LoadScene(nomeDoLevelDeJogo);//carregando uma nova cena
    }

    public void Lobby()
    {
        painelMenuInicial.SetActive(false);
        painelLobby.SetActive(true);
    }
    
    public void Sala()
    {
        conn.Login();
        painelLobby.SetActive(false);
        painelSala.SetActive(true);
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void SairJogo()
    {
        Debug.Log("Saiu do Jogo");
        Application.Quit();
    }

}
