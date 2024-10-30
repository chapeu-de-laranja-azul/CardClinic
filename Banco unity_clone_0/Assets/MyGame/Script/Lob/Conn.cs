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
    /// utilizado para obter as informações que forem passadas pelos jogadores
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
    /// Variavel para salvar as prefebs que vão ser instanciadas no Lobby
    /// </summary>
    [SerializeField] private ItemSala itemSalaPrefab;
    
    /// <summary>
    /// guardando qual vai ser as posições que vão ser instanceados os objetos no Lobby
    /// </summary>
    [SerializeField] private Transform contentObject;

    /// <summary>
    /// Uma lista para armazenar as Listas de salas para o Lobby
    /// </summary>
    List<ItemSala> itemSalaLista = new List<ItemSala>();

    /// <summary>
    /// Variaveis para criação de um anti-spam
    /// </summary>
    private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

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
    /// Inicia o processo de conexão.
    /// - Se já estiver conectado, so e passado o NickName do jogador
    /// - se não estiver conectado, passa o NickName do jogador e conecte esta instância do aplicativo à Photon Cloud Network
    /// </summary>
    public void ConectandoPhoton() 
    {

        //verificamos se estamos conectados ou não, ingressamos se estivermos, caso contrário iniciamos a conexão com o servidor.
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
    /// Utilizada para quando quiser fazer a desconecção do photon (multiplayer)
    /// </summary>
    public void DisconnectPhoton()
    {
        PhotonNetwork.Disconnect();
    }

    /// <summary>
    /// Utilizada para a criação de uma Room 
    /// </summary>
    public void CriarSala()//chamada quando um usuario quer criar uma nova sala de jogo
    {

        if (nomeSala.text.Length >= 1)
        {
            // Quais serão as opções da room
            RoomOptions roomOptions = new RoomOptions() { MaxPlayers = maxPlayerPerRoom };

            // Criando a sala com o nome e as opções escolhidas no lobby padrão
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
            PhotonNetwork.LoadLevel(1);     //Este método envolve o carregamento de um nível de forma assíncrona e a pausa das mensagens de rede durante o processo
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
        //Destruindo todas as prefebs de ItemSala (os botões)
        foreach (ItemSala item in itemSalaLista)
        {
            Destroy(item.gameObject);
            
        }

        //E limpando a lista (para evita instanciar os botões antigos)
        itemSalaLista.Clear();

        //Verificando cada sala da lista de salas aonde remove da lista as salas que foram removidas
        //E cria um botão para cada sala que foi criada com o nome da sala na frente
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

        //Abrindo o menu de multiplayer
        painelLogin.SetActive(false);
        painelMenuMultiplayer.SetActive(true);

        //Joinando em um Lobby apos logar no Photon #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Chamado após desconectar do servidor Photon. Pode ser uma falha ou uma chamada de desconexão explícita
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Causa da desconecção: " + cause);

        //Fechando o menu de multiplayer
        painelLogin.SetActive(true);
        painelMenuMultiplayer.SetActive(false);

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

        //Joinando em um Lobby apos logar no Photon #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.JoinLobby();
    }

    #endregion

    #region IMatchmakingCallbacks

    /// <summary>
    /// Chamado quando o LoadBalancingClient entra em uma sala, não importa se esse cliente a criou ou simplesmente entrou.
    /// </summary>
    public override void OnJoinedRoom()
    {
        //Saindo do Lobby apos entrar em uma sala #IMPORTANTE (Necessario para mostrar as listas de salas)
        PhotonNetwork.LeaveLobby();

        Debug.Log("Entrou em uma sala");
        
        // mostrar o numero de players que estão na sala
        print(PhotonNetwork.CurrentRoom.PlayerCount);

        //mostrar o nome da sala
        print(PhotonNetwork.CurrentRoom.Name);
        
        ObterListaJogadores();
    }

    /// <summary>
    /// Chamado quando o usuário/cliente local sai de uma sala, para que a lógica do jogo possa limpar seu estado interno.
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
        Debug.Log("Não encontrou nenhuma sala ou a sala esta cheia" + message);
    }

    #endregion

    #region ILobbyCallbacks

    /// <summary>
    /// Chamado ao entrar em um lobby no Master Server. As atualizações reais da lista de salas chamarão OnRoomListUpdate.
    /// </summary>
    public override void OnJoinedLobby()
    {

        Debug.Log("OnJoinedLobby");

    }
    
    /// <summary>
    /// Chamado para qualquer atualização da lista de salas enquanto estiver em um lobby (InLobby) no Servidor Mestre.
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
