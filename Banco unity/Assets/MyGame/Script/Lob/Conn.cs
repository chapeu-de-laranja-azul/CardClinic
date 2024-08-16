using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;                               //adicionar um using para indicar estar utilizando o Photon.Pun
using Photon.Realtime;                          //adicionando a biblioteca do Photon Realtime

public class Conn : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nomeJogador, nomeSala;
    [SerializeField] private Text txtNickName;
    public void Login() 
    {
        PhotonNetwork.NickName = nomeJogador.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CriarSala()
    {
        PhotonNetwork.JoinOrCreateRoom(nomeSala.text,new RoomOptions(),TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado");
        PhotonNetwork.JoinLobby();         //Entrando na sala
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Não esta conectado");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Não encontrou nenhuma sala");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou em uma sala");
        print(PhotonNetwork.NickName);
        print(PhotonNetwork.CurrentRoom.PlayerCount);
        print(PhotonNetwork.CurrentRoom.Name);
        txtNickName.text = PhotonNetwork.NickName;
    }

}
