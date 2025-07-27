using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public GameObject HintUI;
    public Text npcText;
    public string hintText;
    void Start()
    {

    }


    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        npcText.text = hintText;
        HintUI.SetActive(true);
    }

}
