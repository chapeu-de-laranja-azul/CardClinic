using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventTable : MonoBehaviour
{

    [SerializeField] float numAleatorio;
    [SerializeField] GameObject painelNum;
    [SerializeField] TextMeshProUGUI numero;
    [SerializeField] TextMeshProUGUI textoEvento;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void Evento()
    {
        numAleatorio = Random.Range(1, 20);

        Debug.Log("Numero: " + numAleatorio);
        numero.text = numAleatorio.ToString();
        painelNum.SetActive(true);

        switch (numAleatorio)
        {
            case 1:

                textoEvento.text = "A clínica deu um problema grave fique 1 turno sem jogar";

                break;
            case 2:

                textoEvento.text = "2";

                break;
            case 3:

                textoEvento.text = "3";

                break;
            case 4:

                textoEvento.text = "4";

                break;
            case 5:

                textoEvento.text = "5";

                break;
            case 6:

                textoEvento.text = "6";

                break;
            case 7:

                textoEvento.text = "7";

                break;
            case 8:

                textoEvento.text = "8";

                break;
            case 9:

                textoEvento.text = "9";

                break;
            case 10:

                textoEvento.text = "10";

                break;
            case 11:

                textoEvento.text = "11";

                break;
            case 12:

                textoEvento.text = "12";

                break;
            case 13:

                textoEvento.text = "13";

                break;
            case 14:

                textoEvento.text = "14";

                break;
            case 15:

                textoEvento.text = "15";

                break;
            case 16:

                textoEvento.text = "16";

                break;
            case 17:

                textoEvento.text = "17";

                break;
            case 18:

                textoEvento.text = "18";

                break;
            case 19:

                textoEvento.text = "19";

                break;
            case 20:

                textoEvento.text = "20";

                break;
        }
    }
}
