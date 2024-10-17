using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;                               //adicionar um using para indicar estar utilizando o Photon.Pun
using Photon.Realtime;                          //adicionando a biblioteca do Photon Realtime

public class Conn : MonoBehaviourPunCallbacks   //para poder utilizar as chamadas do photon
{
    #region Private Serialized fields

    /// <summary>
    /// utilizando para obter as informa��es do nomes das sala e jogador
    /// </summary>
    [SerializeField] private TMP_InputField nomeJogador, nomeSala;

    /// <summary>
    /// para salvar um componente da unity do tipo texto
    /// </summary>
    [SerializeField] private TMP_Text txtNickName, txtSalaName, txtLobby;

    #endregion

    #region Private Fields

    /// <summary>
    /// O n�mero da vers�o deste cliente. Os usu�rios s�o separados uns dos outros por gameVersion (que permite fazer altera��es importantes).
    /// </summary>

    string gameVersion = "1";

    /// <summary>
    /// para guardar a instancia desse script numa variavel
    /// </summary>
    public static Conn instance;

    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// M�todo MonoBehaviour chamado no GameObject pelo Unity durante a fase de inicializa��o inicial.
    /// </summary>
    void Awake()
    {
        //N�o destuindo o gameObject quando carregar outra sala e evitando de duplicar ele
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //#Critical
        //isso garante que possamos usar PhotonNetwork.LoadLevel() no cliente mestre e todos os clientes na mesma sala sincronizar�o seus n�veis automaticamente
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    /// <summary>
    /// M�todo MonoBehaviour chamado em GameObject pelo Unity durante a fase de inicializa��o.
    /// </summary>
    void Start()
    {

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Inicia o processo de conex�o.
    /// - Se j� estiver conectado, so e passado o NickName do jogador
    /// - se n�o estiver conectado, passa o NickName do jogador e conecte esta inst�ncia do aplicativo � Photon Cloud Network
    /// </summary>
    public void Connect()
    {

        //verificamos se estamos conectados ou n�o, ingressamos se estivermos, caso contr�rio iniciamos a conex�o com o servidor.
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = nomeJogador.text;
        }
        else
        {
            PhotonNetwork.NickName = nomeJogador.text;

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

    }

    /// <summary>
    /// Utilizada para a cria��o de uma Room 
    /// </summary>
    public void CriarSala()//chamada quando um usuario criar uma nova sala de jogo
    {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 4 };

        PhotonNetwork.CreateRoom(nomeSala.text, roomOptions, TypedLobby.Default);

        txtSalaName.text = nomeSala.text;
    }


    /// <summary>
    /// Utilizar para quando precisar atualizar a lista de jogadores dentro de uma Room
    /// </summary>
    //[PunRPC]
    public void ObterListaJogadores()
    {
        var lista = "";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            lista += player.NickName + "\n";
        }

        txtNickName.text = lista;
    }

    /// <summary>
    /// Utilizada para levar os jogadores para o jogo mas so quando o dono da sala permitir
    /// </summary>
    public void StartJogo()                 //chamada para quando o dono da sala iniciar o jogo
    {
        if (PhotonNetwork.IsMasterClient)   //Se for o Cliente Master (o jogador que criou a sala)
        {
            PhotonNetwork.LoadLevel(1);     //Este m�todo envolve o carregamento de um n�vel de forma ass�ncrona e a pausa das mensagens de rede durante o processo
        }
    }


    /// <summary>
    /// Utilizada quando alguem quitar da sala
    /// </summary>
    public void SairRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    /// <summary>
    /// Utilizada para quando quiser fazer a desconec��o do photon (multiplayer)
    /// </summary>
    public void DisconnectPhoton()
    {
        PhotonNetwork.Disconnect();
    }

    /// <summary>
    /// Implementar mostar as salas disponiveis
    /// </summary>
    public void SalasDisponiveis()
    {
        Connect();
        //CODIGO PARA MOSTRAR AS ROONS

        
    }

    #endregion

    #region MonoBehaviourPunnCallbacks Callbacks

    #region IConnectionCallbacks
    /// <summary>
    /// Chamado para sinalizar que a "conex�o de baixo n�vel" foi estabelecida, mas antes que o cliente possa chamar a opera��o no servidor
    /// </summary>
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    /// <summary>
    /// Chamado quando o cliente est� conectado ao Servidor Mestre e pronto para correspond�ncia e outras tarefas.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        
    }

    /// <summary>
    /// Chamado ap�s desconectar do servidor Photon. Pode ser uma falha ou uma chamada de desconex�o expl�cita
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Causa da desconec��o: " + cause);
    }
    #endregion

    #region IInRoomCallbacks

    /// <summary>
    /// Chamado quando um jogador remoto entra na sala. Este jogador j� foi adicionado � lista de jogadores.
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ObterListaJogadores();
    }

    /// <summary>
    /// Chamado quando um jogador remoto saiu da sala ou ficou inativo. Verifique otherPlayer.IsInactive.
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //UTILIZAR PARA DEIXAR O PLAYER EM ESTADO INATIVO (AONDE PODE RETORNAR PARA O JOGO)
        ObterListaJogadores();
    }

    #endregion

    #region IMatchmakingCallbacks

    /// <summary>
    /// Chamado quando o LoadBalancingClient entra em uma sala, n�o importa se esse cliente a criou ou simplesmente entrou.
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou em uma sala");
        print(PhotonNetwork.CurrentRoom.PlayerCount);   //mostrar o numero de players que est�o na sala
        print(PhotonNetwork.CurrentRoom.Name);          //mostrar o nome da sala
        ObterListaJogadores();

    }

    /// <summary>
    /// Chamado quando o usu�rio/cliente local sai de uma sala, para que a l�gica do jogo possa limpar seu estado interno.
    /// </summary>
    public override void OnLeftRoom()
    {
        //UTILIZAR PARA QUANDO O PLAYER AIVAMENTE SAIA DO JOGO
    }

    /// <summary>
    /// Chamado quando uma chamada anterior de OpJoinRandom (ou OpJoinRandomOrCreateRoom etc.) falhou no servidor.
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("N�o encontrou nenhuma sala ou a sala esta cheia");
    }

    #endregion

    #region ILobbyCallbacks

    /// <summary>
    /// Chamado ao entrar em um lobby no Master Server. As atualiza��es reais da lista de salas chamar�o OnRoomListUpdate.
    /// </summary>
    public override void OnJoinedLobby()
    {
        //
    }

    /// <summary>
    /// Chamado para qualquer atualiza��o da lista de salas enquanto estiver em um lobby (InLobby) no Servidor Mestre.
    /// </summary>
    /// <param name="roomList"></param>
    //Chamado para qualquer atualiza��o da lista de salas enquanto estiver em um lobby (InLobby) no Servidor Mestre.
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        var room = "";
        foreach(var roomL in roomList)
        {
            room += roomL + "/n";
        }
        
        txtLobby.text = room;
    }
    
    #endregion

    #endregion
}
