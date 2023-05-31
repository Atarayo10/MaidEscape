using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public int HP;
    public float AD;
    public float AP;
    public float AS;
    public float DEF;
    public float MDEF;
    public float Speed;

    public PlayerStat(int _HP, float _AD, float _AP, float _AS, float _DEF, float _MDEF, float _Speed)
    {
        HP = _HP;
        AD = _AD;
        AP = _AP;
        AS = _AS;
        DEF = _DEF;
        MDEF = _MDEF;
        Speed = _Speed;
    }
}
