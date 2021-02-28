using System.Collections;
using System.Collections.Generic;

namespace enums
{
    public enum ANIMTRIGGER { IDLE, HIT, ATTACK, SKILL, DIE, WIN, BUFF, VICTORY }
    public enum PLAYERSKILL { START, JAMJAM, MAHA, BUFF, DEBUFF, UCHE, END }
    // Start is called before the first frame update
    public enum BOSSSKILL { FIRE, STUN, BLEEDING, SLOW, POISON, STONING, FREEZE }
    public enum GRADE_MON { BASE, MASIC, DEF, RARE, BOSS}
    public enum BATTLE_PROCESS { BEFORE, DURING , END, LEVELUP}
    public enum BATTLE_CLEAR { RUN, CLEAR, DIE,END}
    public enum QUEST_TYPE { MONSTER, BOSS, ITEM}
    public enum ITEM_TYPE { ETC,EQUIP, USE  }
    public enum EQUIP_TYPE {  USE, SWORD, KNUKLE, HEAD, BODY, FOOT}
    public enum ITEM_GRADE { BASIC, NORMAL, SPECIAL, UNIQUE, LEGENDARY, END}
    public enum ETCITEM_TYPE { NOR, BOSS,END}
    public enum SOUND_TYPE { REIN_SUC, REIN_FAIL, HIT, POTION, SELL, DIE, WIN, END}
    public enum BGM_TYPE { TITLE, LOBBY, FOREST, TOWN, DOOR, GARDEN,CASTLE, LAST_ROOM, VICTORY, FAIL,END}
    public enum WINVEN_TYPE { NONE, TRADE, REINFORCE, END }
}
