using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;                                       //adicionar um using para indicar estar utilizando o Photon.Pun
using Photon.Realtime;                                  //adicionando a biblioteca do Photon Realtime

public class Conn : MonoBehaviourPunCallbacks           //para poder utilizar as chamadas do photon
{
                                                        //utilizando para obter as informa��es do nomes das sala e jogador
    [SerializeField] private TMP_InputField nomeJogador, nomeSala;
    [SerializeField] private TMP_Text txtNickName, txtSalaName;          //para salvar um componente da unity do tipo texto

    public static Conn instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;    //Defini se todos os cliente de uma sala dever�o carregar automaticamente o mesmo nivel do Cliente Master
    }
    
    public void Login() 
    {
        PhotonNetwork.NickName = nomeJogador.text;      //salvando o nick do jogador que vai entrar na sala
        PhotonNetwork.ConnectUsingSettings();           //conectando ao claud do photon
    }

    public void CriarSala()
    {
                                                        //criando um sala de jogo (nome da sala, op��es da sala criada, tipo do lobby a ser criado)
        PhotonNetwork.CreateRoom(nomeSala.text,new RoomOptions(),TypedLobby.Default);

        txtSalaName.text = nomeSala.text;
        txtNickName.text = nomeJogador.text;
    }

    public void StartJogo()
    {
        if (PhotonNetwork.IsMasterClient)               //Se for o Cliente Master (o jogador que criou a sala)
        {
            PhotonNetwork.LoadLevel(1);                 //Este m�todo envolve o carregamento de um n�vel de forma ass�ncrona e a pausa das mensagens de rede durante o processo
        }
    }

    public void DisconnectPhoton()
    {
        PhotonNetwork.Disconnect();
    }


    #region Fun��es de verifica��os e erros de conec��o
    public override void OnConnectedToMaster()          //Chamado quando o cliente est� conectado ao Servidor Mestre e pronto para matchmaking e outras tarefas.
    {
        Debug.Log("Conectado");
        
    }
                                                        //chamada quando o jogador desconectar da sala
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("N�o esta conectado");
    }
                                                        //Chamado quando uma chamada OpJoinRandom anterior falhou no servidor.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("N�o encontrou nenhuma sala ou a sala esta cheia");
    }

    public override void OnJoinedRoom()                 //Se o jogador entrar na sala com sucesso
    {
        Debug.Log("Entrou em uma sala");
        print(PhotonNetwork.CurrentRoom.PlayerCount);   //mostrar o numero de players que est�o na sala
        print(PhotonNetwork.CurrentRoom.Name);          //mostrar o nome da sala
    }
    #endregion
}
