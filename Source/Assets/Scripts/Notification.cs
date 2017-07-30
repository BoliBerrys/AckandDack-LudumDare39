using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float duration = 3;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Show()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        anim.SetBool("Show", true);
        yield return new WaitForSeconds(duration);
        anim.SetBool("Show", false);
    }
}
