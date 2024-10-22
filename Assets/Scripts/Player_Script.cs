using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public Action m_acOnDeath;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BorderPlayer") m_acOnDeath?.Invoke();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tree") m_acOnDeath?.Invoke();
    }

}
