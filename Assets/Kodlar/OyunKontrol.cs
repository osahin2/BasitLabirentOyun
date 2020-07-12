using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{

    public Text oyunBittiText;
    public Text yenidenBaslaText;

    bool yenidenBaslaKontrol = false;
    bool oyunBittiKontrol = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && yenidenBaslaKontrol)
        {
            SceneManager.LoadScene("scene1");
        }
        if(oyunBittiKontrol)
        {
            yenidenBaslaText.text = "Yeniden Oynamak için 'R' Tuşuna Basın";
            yenidenBaslaKontrol = true;
        }
    }
    public void OyunBitti()
    {
        oyunBittiText.text = "Oyun Bitti !";
        oyunBittiKontrol = true;
    }
}
