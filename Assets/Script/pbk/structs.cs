using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD

namespace Structs
{
=======
namespace Structs
{ 
>>>>>>> dba6358622c7a6b50998ea3d29bd580bc774db9a
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
<<<<<<< HEAD
        public void setLevel(ref tagInfo argInfo, int argLevel) { argInfo.m_iLevel = argLevel; }
=======
        public void setLevel(ref tagInfo argInfo, int argLevel) {  argInfo.m_iLevel = argLevel; }
>>>>>>> dba6358622c7a6b50998ea3d29bd580bc774db9a
        public void setAtk(ref tagInfo argInfo, int argAtk) { argInfo.m_iAtk = argAtk; }
        public void setMatk(ref tagInfo argInfo, int argMatk) { argInfo.m_iMatk = argMatk; }
        public void setMaxHp(ref tagInfo argInfo, int argMaxHp) { argInfo.m_iMaxHp = argMaxHp; }
        public void setCurrentHp(ref tagInfo argInfo, int argCurrentHp) { argInfo.m_iCurrentHp = argCurrentHp; }
        public void setAtkSpeed(ref tagInfo argInfo, int argAtkSpeed) { argInfo.m_iAtkSpeed = argAtkSpeed; }
        public void setDef(ref tagInfo argInfo, int argDef) { argInfo.m_iDef = argDef; }
        public void setCri(ref tagInfo argInfo, float argCri) { argInfo.m_fCri = argCri; }
        public void setCriDmg(ref tagInfo argInfo, float argCriDmg) { argInfo.m_fCriDmg = argCriDmg; }
        
    }
    [System.Serializable]
    public struct tagStat
    { 
        [UnityEngine.SerializeField]
        public int m_iPow;
        [UnityEngine.SerializeField]
        public int m_iDex;
        [UnityEngine.SerializeField]
        public int m_iInt;
        [UnityEngine.SerializeField]
        public int m_iStat;

        public int IPow { get => m_iPow; set => m_iPow = value; }
        public int IDex { get => m_iDex; set => m_iDex = value; }
        public int IInt { get => m_iInt; set => m_iInt = value; }
        public int IStat { get => m_iStat; set => m_iStat = value; }

<<<<<<< HEAD
        public void setPow(ref tagStat argInfo, int argPow) { argInfo.m_iPow = argPow; }
        public void setDex(ref tagStat argInfo, int argDex) { argInfo.m_iDex = argDex; }
        public void setInt(ref tagStat argInfo, int argInt) { argInfo.m_iInt = argInt; }
        public void setStat(ref tagStat argInfo, int argStat) { argInfo.m_iStat = argStat; }
=======
        public void setPow(ref tagStat argStat, int argPow) { argStat.m_iPow = argPow; }
        public void setDex(ref tagStat argStat, int argDex) { argStat.m_iDex = argDex; }
        public void setInt(ref tagStat argStat, int argInt) { argStat.m_iInt = argInt; }
        public void setStat(ref tagStat argStat, int argStatPoint) { argStat.m_iStat = argStatPoint; }
        
        
>>>>>>> dba6358622c7a6b50998ea3d29bd580bc774db9a
    }
}