using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       //para utilizar entradas de Input
using UnityEngine.SceneManagement;          //para utilizar controles de scena

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial, painelOpcoes, painelLogin, painelSala;
    [SerializeField] private GameObject conectionManager, painelEntrarSala, painelLob;

    private Conn conn;

    void Start()
    {
        conn = conectionManager.GetComponent<Conn>();//pegando um objeto do jogo e selecionando um componente dele para salvar na variavel
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void AbrirLogin()
    {
        painelMenuInicial.SetActive(false);
        painelLogin.SetActive(true);
    }

    public void AbrirEntrarSala()
    {
        //conn.Connect(); VERIFICAR COMO FICA MELHOR (DEIXAR AQUI OU NO conn.Connect())
        conn.SalasDisponiveis();

        painelEntrarSala.SetActive(true);
        painelLogin.SetActive(false);
    }

    public void AbrirCriarSala()
    {
        conn.Connect();

        painelLogin.SetActive(false);
        painelSala.SetActive(true);
    }

    public void AbrirLob()
    {
        painelSala.SetActive(false);
        painelLob.SetActive(true);
        conn.photonView.RPC("ObterListaJogadores", Photon.Pun.RpcTarget.All);
        conn.CriarSala();
        
    }

    public void FecharOpcoes()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void FecharLogin()
    {
        painelLogin.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void FecharEntrarSala()
    {
        conn.DisconnectPhoton();
        
        painelEntrarSala.SetActive(false);
        painelLogin.SetActive(true);
    }

    public void FecharSala ()
    {
        conn.DisconnectPhoton();

        painelLogin.SetActive(true);
        painelSala.SetActive(false);
    }

    public void FecharLob()
    {
        conn.SairRoom();

        painelLob.SetActive(false);
        painelSala.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Saiu do Jogo");
        Application.Quit();
    }

}
