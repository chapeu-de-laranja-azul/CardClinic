using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSala : MonoBehaviour
{

    public Text salaNome;
    Conn conn;

    private void Start()
    {
        conn = FindObjectOfType<Conn>();
    }

    public void SetandoNomeSala(string _nomeSala)
    {
        salaNome.text = _nomeSala;
    }
    
    public void ClickItem()
    {
        conn.EntrandoSala(salaNome.text);
        conn.AbrirSalaLobby();
    }

}
