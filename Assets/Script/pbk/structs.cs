using System.Collections;
using System.Collections.Generic;

public class structs
{
    [System.Serializable]
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

        public void setName(string argName) { m_sName = argName; }
        public void setLevel(int argLevel) { m_iLevel = argLevel; }
        public void setAtk(int argAtk) { m_iAtk = argAtk; }
        public void setMatk(int argMatk) { m_iMatk = argMatk; }
        public void setMaxHp(int argMaxHp) { m_iMaxHp = argMaxHp; }
        public void setCurrentHp(int argCurrentHp) { m_iCurrentHp = argCurrentHp; }
        public void setAtkSpeed(int argAtkSpeed) { m_iAtkSpeed = argAtkSpeed; }
        public void setDef(int argDef) { m_iDef = argDef; }
        public void setCri(float argCri) { m_fCri = argCri; }
        public void setCriDmg(float argCriDmg) { m_fCriDmg = argCriDmg; }
        
    }
    [System.Serializable]
    public struct tagStat
    { 
        [UnityEngine.SerializeField]
        private int m_iPow;
        [UnityEngine.SerializeField]
        private int m_iDex;
        [UnityEngine.SerializeField]
        private int m_iInt;
        [UnityEngine.SerializeField]
        private int m_iStat;

        public int IPow { get => m_iPow; set => m_iPow = value; }
        public int IDex { get => m_iDex; set => m_iDex = value; }
        public int IInt { get => m_iInt; set => m_iInt = value; }
        public int IStat { get => m_iStat; set => m_iStat = value; }

        public void setPow(int argPow) { m_iPow = argPow; }
        public void setDex(int argDex) { m_iDex = argDex; }
        public void setInt(int argInt) { m_iInt = argInt; }
        public void setStat(int argStat) { m_iStat = argStat; }
        
        
    }
}
