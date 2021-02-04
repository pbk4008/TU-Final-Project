using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    private int m_iReinforce;

    public int IReinforce { get => m_iReinforce; set => m_iReinforce = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_iReinforce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
