using System.Collections;
using System.Collections.Generic;

namespace enums
{
    public enum ANIMTRIGGER { IDLE, HIT, ATTACK, SKILL, DIE, WIN, BUFF }
    public enum PLAYERSKILL { START, JAMJAM, MAHA, BUFF, DEBUFF, UCHE, END }
    // Start is called before the first frame update
    public enum BOSSSKILL { FIRE, STUN, BLEEDING, SLOW, POISON, STONING, FREEZE }
    public enum GRADE_MON { BASE, MASIC, DEF, RARE, BOSS}
    public enum BATTLE_PROCESS { BEFORE, DURING , END}
    public enum BATTLE_CLEAR { RUN, CLEAR,END}
    public enum QUEST_TYPE { MONSTER, BOSS, ITEM}
    public enum ITEM_TYPE { ETC,EQUIP, USE  }
    public enum EQUIP_TYPE {  USE, SWORD, KNUKLE, HEAD, BODY, FOOT}
    public enum ITEM_GRADE { BASIC, NORMAL, SPECIAL, UNIQUE, LEGENDARY}
}
