using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;                               //adicionar um using para indicar estar utilizando o Photon.Pun
using Photon.Realtime;                          //adicionando a biblioteca do Photon Realtime

public class Conn : MonoBehaviourPunCallbacks   //para poder utilizar as chamadas do photon
{
    #region Private Serialized fields

    /// <summary>
    /// Uma String so para salvar em que scena estamos
    /// </summary>
    [SerializeField] private string nomeDoLevelDeJogo;

    /// <summary>
    /// para salvar os objetos de jogo (Aqui Apenas Paineis)
    /// </summary>
    [SerializeField] private GameObject painelMenuInicial, painelOpcoes, painelLogin, painelRoom, painelCriarRoom, painelLobby, painelMenuMultiplayer;

    /// <summary>
    /// utilizado para obter as informa��es que forem passadas pelos jogadores
    /// </summary>
    [SerializeField] private TMP_InputField nomeJogador, nomeSala;

    /// <summary>
    /// para salvar textos que forem passados
    /// </summary>
    [SerializeField] private TMP_Text txtNickName, txtSalaName;

    /// <summary>
    /// Para armazenar o maximo de players numa room de jogo BYTE(0 a 255)
    /// </summary>
    [SerializeField] private byte maxPlayerPerRoom = 4;

    /// <summary>
    /// Variavel para salvar as prefebs que v�o ser instanciadas no Lobby
    /// </summary>
    [SerializeField] private ItemSala itemSalaPrefab;
    
    /// <summary>
    /// guardando qual vai ser as posi��es que v�o ser instanceados os objetos no Lobby
    /// </summary>
    [SerializeField] private Transform contentObject;

    /// <summary>
    /// Uma lista para armazenar as Listas de salas para o Lobby
    /// </summary>
    List<ItemSala> itemSalaLista = new List<ItemSala>();

    /// <summary>
    /// Variaveis para cria��o de um anti-spam
    /// </summary>
    private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

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

    #region Controle dos Paineis do jogo

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

    public void AbrirLogin()
    {
        painelMenuInicial.SetActive(false);
        painelLogin.SetActive(true);
    }

    public void FecharLogin()
    {
        painelLogin.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    
    public void SairJogo()
    {
        Debug.Log("Saiu do Jogo");
        Application.Quit();
    }

    public void AbrirCriarRoom()
    {
        painelMenuMultiplayer.SetActive(false);
        painelCriarRoom.SetActive(true);
    }

    public void FecharCriarRoom()
    {
        painelMenuMultiplayer.SetActive(true);
        painelCriarRoom.SetActive(false);
    }

    public void AbrirLobby()
    {
        
        painelMenuMultiplayer.SetActive(false);
        painelLobby.SetActive(true);
    }

    public void FecharLobby()
    {
        painelMenuMultiplayer.SetActive(true);
        painelLobby.SetActive(false);
    }

    public void AbrirSalaLobby()
    {
        //Abrindo o painel de sala de jogo (do Lobby)
        painelRoom.SetActive(true);
        painelLobby.SetActive(false);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Inicia o processo de conex�o.
    /// - Se j� estiver conectado, so e passado o NickName do jogador
    /// - se n�o estiver conectado, passa o NickName do jogador e conecte esta inst�ncia do aplicativo � Photon Cloud Network
    /// </summary>
    public void ConectandoPhoton() 
    {

        //verificamos se estamos conectados ou n�o, ingressamos se estivermos, caso contr�rio iniciamos a conex�o com o servidor.
        if (nomeJogador.text.Length >= 1)
        {
            PhotonNetwork.NickName = nomeJogador.text;

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
            
            // CONECTANDO
            Debug.Log("Conectando...");

        }
        else
        {
            // ERRO DIGITE UM NICKNAME
            Debug.Log("Digite um nick name");
        }
        
    }

    /// <summary>
    /// Utilizada para quando quiser fazer a desconec��o do photon (multiplayer)
    /// </summary>
    public void DisconnectPhoton()
    {
        PhotonNetwork.Disconnect();
    }

    /// <summary>
    /// Utilizada para a cria��o de uma Room 
    /// </summary>
    public void CriarSala()//chamada quando um usuario quer criar uma nova sala de jogo
    {

        if (nomeSala.text.Length >= 1)
        {
            // Quais ser�o as op��es da room
            RoomOptions roomOptions = new RoomOptions() { MaxPlayers = maxPlayerPerRoom };

            // Criando a sala com o nome e as op��es escolhidas no lobby padr�o
            PhotonNetwork.CreateRoom(nomeSala.text, roomOptions, TypedLobby.Default);

            //Abrindo o painel de sala de jogo (do Criar Room)
            painelRoom.SetActive(true);
            painelCriarRoom.SetActive(false);
        }
        else
        {
            Debug.Log("Digite o nome da Sala");
        }
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

        txtSalaName.text = PhotonNetwork.CurrentRoom.Name;
        txtNickName.text = lista;
    }

    /// <summary>
    /// Utilizada quando alguem quitar da sala
    /// </summary>
    public void SairRoom()
    {
        PhotonNetwork.LeaveRoom();

    }

    /// <summary>
    /// Implementar mostar as salas disponiveis
    /// </summary>
    public void SalasDisponiveis(List<RoomInfo> list)
    {
        //Destruindo todas as prefebs de ItemSala (os bot�es)
        foreach (ItemSala item in itemSalaLista)
        {
            Destroy(item.gameObject);
            
        }

        //E limpando a lista (para evita instanciar os bot�es antigos)
        itemSalaLista.Clear();

        //Verificando cada sala da lista de salas aonde remove da lista as salas que foram removidas
        //E cria um bot�o para cada sala que foi criada com o nome da sala na frente
        for(int i=0; i<list.Count; i++)
        {
            RoomInfo room = list[i];

            if (room.RemovedFromList)
            {
                list.Remove(room);
            }
            else
            {
                ItemSala newSala = Instantiate(itemSalaPrefab, contentObject);
                newSala.SetandoNomeSala(room.Name);
                itemSalaLista.Add(newSala);
            }
            
        }
        
    }

    public void EntrandoSala(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
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

        //Abrindo o menu de multiplayer
        painelLogin.SetActive(false);
        painelMenuMultiplayer.SetActive(true);

        //Joinando em um Lobby apos logar no Photon #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Chamado ap�s desconectar do servidor Photon. Pode ser uma falha ou uma chamada de desconex�o expl�cita
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Causa da desconec��o: " + cause);

        //Fechando o menu de multiplayer
        painelLogin.SetActive(true);
        painelMenuMultiplayer.SetActive(false);

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

        //Joinando em um Lobby apos logar no Photon #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.JoinLobby();
    }

    #endregion

    #region IMatchmakingCallbacks

    /// <summary>
    /// Chamado quando o LoadBalancingClient entra em uma sala, n�o importa se esse cliente a criou ou simplesmente entrou.
    /// </summary>
    public override void OnJoinedRoom()
    {
        //Saindo do Lobby apos entrar em uma sala #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.LeaveLobby();

        Debug.Log("Entrou em uma sala");
        
        // mostrar o numero de players que est�o na sala
        print(PhotonNetwork.CurrentRoom.PlayerCount);

        //mostrar o nome da sala
        print(PhotonNetwork.CurrentRoom.Name);
        
        ObterListaJogadores();
    }

    /// <summary>
    /// Chamado quando o usu�rio/cliente local sai de uma sala, para que a l�gica do jogo possa limpar seu estado interno.
    /// </summary>
    public override void OnLeftRoom()
    {
        //UTILIZAR PARA QUANDO O PLAYER AIVAMENTE SAIA DO JOGO

        //Joinando em um Lobby apos logar no Photon #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.JoinLobby();

        // Fechando o painel de Sala e retornando para o MenuMultiplayer
        painelRoom.SetActive(false);
        painelMenuMultiplayer.SetActive(true);

    }

    /// <summary>
    /// Chamado quando uma chamada anterior de OpJoinRandom (ou OpJoinRandomOrCreateRoom etc.) falhou no servidor.
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("N�o encontrou nenhuma sala ou a sala esta cheia" + message);
    }

    #endregion

    #region ILobbyCallbacks

    /// <summary>
    /// Chamado ao entrar em um lobby no Master Server. As atualiza��es reais da lista de salas chamar�o OnRoomListUpdate.
    /// </summary>
    public override void OnJoinedLobby()
    {

        Debug.Log("OnJoinedLobby");

    }
    
    /// <summary>
    /// Chamado para qualquer atualiza��o da lista de salas enquanto estiver em um lobby (InLobby) no Servidor Mestre.
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        if (Time.time >= nextUpdateTime)
        {
            SalasDisponiveis(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }

    }
    
    #endregion

    #endregion
}
