using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanKontrol : MonoBehaviour
{
    GameObject karakter;
    RaycastHit ray;
    public float hiz = 2;
    public Animator anim;

    Vector3 aradakiMesafe;

    void Start()
    {
        anim = GetComponent<Animator>();
        karakter = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        BeniGorduMu();
        if (ray.collider.tag=="Player")
        {
            Hareket();
        }
    }
    void BeniGorduMu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        Physics.Raycast(transform.position, rayYonum, out ray);
        transform.rotation = Quaternion.LookRotation(ray.point);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }
    void Hareket()
    {
        if (transform.GetChild(0).GetComponent<MeshCollider>().isTrigger == false)
        {
            hiz = 0.5f;
        }
        else
        {
            hiz = 2;
        }
        aradakiMesafe = (karakter.transform.position - transform.position).normalized;
        transform.position += aradakiMesafe * Time.fixedDeltaTime * hiz;
        anim.SetBool("isSee", true);
    }
}
