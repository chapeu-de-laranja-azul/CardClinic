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
    /// utilizando para obter as informações do nomes das sala e jogador
    /// </summary>
    [SerializeField] private TMP_InputField nomeJogador, nomeSala;

    /// <summary>
    /// para salvar um componente da unity do tipo texto
    /// </summary>
    [SerializeField] private TMP_Text txtNickName, txtSalaName, txtLobby;

    #endregion

    #region Private Fields

    /// <summary>
    /// O número da versão deste cliente. Os usuários são separados uns dos outros por gameVersion (que permite fazer alterações importantes).
    /// </summary>

    string gameVersion = "1";

    /// <summary>
    /// para guardar a instancia desse script numa variavel
    /// </summary>
    public static Conn instance;

    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// Método MonoBehaviour chamado no GameObject pelo Unity durante a fase de inicialização inicial.
    /// </summary>
    void Awake()
    {
        //Não destuindo o gameObject quando carregar outra sala e evitando de duplicar ele
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
        //isso garante que possamos usar PhotonNetwork.LoadLevel() no cliente mestre e todos os clientes na mesma sala sincronizarão seus níveis automaticamente
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    /// <summary>
    /// Método MonoBehaviour chamado em GameObject pelo Unity durante a fase de inicialização.
    /// </summary>
    void Start()
    {

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Inicia o processo de conexão.
    /// - Se já estiver conectado, so e passado o NickName do jogador
    /// - se não estiver conectado, passa o NickName do jogador e conecte esta instância do aplicativo à Photon Cloud Network
    /// </summary>
    public void Connect()
    {

        //verificamos se estamos conectados ou não, ingressamos se estivermos, caso contrário iniciamos a conexão com o servidor.
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
    /// Utilizada para a criação de uma Room 
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
            PhotonNetwork.LoadLevel(1);     //Este método envolve o carregamento de um nível de forma assíncrona e a pausa das mensagens de rede durante o processo
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
    /// Utilizada para quando quiser fazer a desconecção do photon (multiplayer)
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
    /// Chamado para sinalizar que a "conexão de baixo nível" foi estabelecida, mas antes que o cliente possa chamar a operação no servidor
    /// </summary>
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    /// <summary>
    /// Chamado quando o cliente está conectado ao Servidor Mestre e pronto para correspondência e outras tarefas.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        
    }

    /// <summary>
    /// Chamado após desconectar do servidor Photon. Pode ser uma falha ou uma chamada de desconexão explícita
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Causa da desconecção: " + cause);
    }
    #endregion

    #region IInRoomCallbacks

    /// <summary>
    /// Chamado quando um jogador remoto entra na sala. Este jogador já foi adicionado à lista de jogadores.
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
    /// Chamado quando o LoadBalancingClient entra em uma sala, não importa se esse cliente a criou ou simplesmente entrou.
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou em uma sala");
        print(PhotonNetwork.CurrentRoom.PlayerCount);   //mostrar o numero de players que estão na sala
        print(PhotonNetwork.CurrentRoom.Name);          //mostrar o nome da sala
        ObterListaJogadores();

    }

    /// <summary>
    /// Chamado quando o usuário/cliente local sai de uma sala, para que a lógica do jogo possa limpar seu estado interno.
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
        Debug.Log("Não encontrou nenhuma sala ou a sala esta cheia");
    }

    #endregion

    #region ILobbyCallbacks

    /// <summary>
    /// Chamado ao entrar em um lobby no Master Server. As atualizações reais da lista de salas chamarão OnRoomListUpdate.
    /// </summary>
    public override void OnJoinedLobby()
    {
        //
    }

    /// <summary>
    /// Chamado para qualquer atualização da lista de salas enquanto estiver em um lobby (InLobby) no Servidor Mestre.
    /// </summary>
    /// <param name="roomList"></param>
    //Chamado para qualquer atualização da lista de salas enquanto estiver em um lobby (InLobby) no Servidor Mestre.
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
