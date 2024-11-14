using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSensibilidade : MonoBehaviour
{

    public float sensibilidadeMouse = 100f;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        sensibilidadeMouse = PlayerPrefs.GetFloat("currentSensitivity", 100);
        slider.value = sensibilidadeMouse/10;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("currentSensitivity", sensibilidadeMouse);
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;    
    }

    public void MouseSense(float value)
    {
        sensibilidadeMouse = value * 10;
    }
}
