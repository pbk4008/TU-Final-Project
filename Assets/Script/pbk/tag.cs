using System.Collections;
using System.Collections.Generic;

public class tag
{
    public const int MAXSIZE = 1024;

    public struct tagInfo
    {
        private string m_sName;
        private int m_iLevel;
        private int m_iAtk;
        private int m_iMatk;
        private int m_iMaxHp;
        private int m_iCurrentHp;
        private int m_iAtkSpeed;
        private int m_iDef;
        private float m_fCri;
        private float m_fCriDmg;

        public string SName { get => m_sName; set => m_sName = value; }
        public int ILevel { get => m_iLevel; set => m_iLevel = value; }
        public int IAtk { get => m_iAtk; set => m_iAtk = value; }
        public int IMatk { get => m_iMatk; set => m_iMatk = value; }
        public int IMaxHp { get => m_iMaxHp; set => m_iMaxHp = value; }
        public int ICurrentHp { get => m_iCurrentHp; set => m_iCurrentHp = value; }
        public int IAtkSpeed { get => m_iAtkSpeed; set => m_iAtkSpeed = value; }
        public int IDef { get => m_iDef; set => m_iDef = value; }
        public float FCri { get => m_fCri; set => m_fCri = value; }
        public float FCriDmg { get => m_fCriDmg; set => m_fCriDmg = value; }
    }
}
