using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public Animator anim;
    public GameObject kafaKamerasi;

    Rigidbody fizik;
    Vector3 vec;
    Vector3 kameraMesafe;
    RaycastHit hit;
    GameObject dusman;
    GameObject oyunKontrol;
    OyunKontrol kontrol;

    float vertical = 0, horizontal = 0;
    float kafaRotSagSol = 0;

    void Awake()
    {
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontrol");
        kontrol = oyunKontrol.GetComponent<OyunKontrol>();
        dusman = GameObject.FindGameObjectWithTag("dusman");
        kafaKamerasi.transform.rotation = Quaternion.Euler(0, 0, 0);
        anim = GetComponent<Animator>();
        fizik = GetComponent<Rigidbody>();
        kameraMesafe = kafaKamerasi.transform.position - transform.position;
    }
    void FixedUpdate()
    {
        Hareket();
        Rotasyon();
        Atak();
        Defans();

        if (anim.GetBool("isTouch"))
        {
            anim.SetBool("isAttack", false);
            kontrol.OyunBitti();
            gameObject.GetComponent<KarakterKontrol>().enabled = false;
        }
        
        
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="dusman")
        {
            if(anim.GetBool("isAttack"))
            {
                col.gameObject.SetActive(false);
            }
            else
            {
                anim.SetBool("isTouch", true);
            }
        }
        if(col.gameObject.tag=="oyunbitti")
        {
            kontrol.OyunBitti();
            gameObject.GetComponent<KarakterKontrol>().enabled = false;
        }

    }
    void Hareket()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        vec = new Vector3(horizontal, 0, vertical);
        vec = transform.TransformDirection(vec);
        transform.position += vec * Time.fixedDeltaTime * 3;
    }
    void Rotasyon()
    {
        kafaKamerasi.transform.position = transform.position + kameraMesafe;
        kafaRotSagSol = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * 150;
        Vector3 newRot = kafaKamerasi.transform.localEulerAngles;
        newRot.y += kafaRotSagSol;
        kafaKamerasi.transform.localEulerAngles = newRot;

        Physics.Raycast(Vector3.zero, kafaKamerasi.transform.GetChild(0).forward, out hit);
        transform.rotation = Quaternion.LookRotation(hit.point);
        Debug.DrawLine(Vector3.zero, hit.point, Color.magenta);
    }
    void Atak()
    {
        if(Input.GetMouseButton(0))
        {
            anim.SetBool("isAttack", true);
            
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
    void Defans()
    {
        if(Input.GetMouseButton(1))
        {
            dusman.transform.GetChild(0).GetComponent<MeshCollider>().isTrigger = false;
            transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<MeshCollider>().enabled = true;
            anim.SetBool("isDef", true);
        }
        else
        {
            dusman.transform.GetChild(0).GetComponent<MeshCollider>().isTrigger = true;
            transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<MeshCollider>().enabled = false;
            anim.SetBool("isDef", false);
        }
    }
}
