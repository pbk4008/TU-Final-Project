using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemCreate : MonoBehaviour
{
    private List<Item> m_Create;
    private GraphicRaycaster raycaster;
    private GameObject m_SelectObject;
    private bool m_bSelect;
    private int m_iNeedCount;
    public bool BSelect { get => m_bSelect; set => m_bSelect = value; }
    public int INeedCount { get => m_iNeedCount; set => m_iNeedCount = value; }
    public GameObject SelectObject { get => m_SelectObject; set => m_SelectObject = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_iNeedCount = 0;
        raycaster = gameObject.transform.parent.GetComponent<GraphicRaycaster>();
        m_bSelect = false;
        
        m_Create = new List<Item>();
        m_Create.AddRange(gameObject.GetComponentsInChildren<Item>());
        m_Create[0].ItemSetting(ITEM_TYPE.USE, EQUIP_TYPE.USE, ITEM_GRADE.NORMAL);
        m_Create[1].ItemSetting(ITEM_TYPE.USE, EQUIP_TYPE.USE, ITEM_GRADE.SPECIAL);
        m_Create[2].ItemSetting(ITEM_TYPE.USE, EQUIP_TYPE.USE, ITEM_GRADE.UNIQUE);
        m_Create[3].ItemSetting(ITEM_TYPE.USE, EQUIP_TYPE.USE, ITEM_GRADE.LEGENDARY);
        
        foreach(Item i in m_Create)
        {
            i.CodeSolve();
            i.ImageSetting();
            i.gameObject.GetComponent<Image>().sprite = i.SprImg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerData.position = Input.mousePosition;
            raycaster.Raycast(pointerData, results);
            if (results.Count == 0 || results[0].gameObject.tag != "ItemUI")
            {
                return;
            }
            Item tmpItem = results[0].gameObject.GetComponent<Item>();
            m_SelectObject = tmpItem.gameObject;

            Debug.Log(tmpItem.gameObject.name);
            tmpItem.CodeSolve();

            switch(tmpItem.EGrade)
            {
                case ITEM_GRADE.NORMAL:
                    m_iNeedCount = 10;
                    break;
                case ITEM_GRADE.SPECIAL:
                    m_iNeedCount = 15;
                    break;
                case ITEM_GRADE.UNIQUE:
                    m_iNeedCount = 30;
                    break;
                case ITEM_GRADE.LEGENDARY:
                    m_iNeedCount = 50;
                    break;
            }
            m_bSelect = true;
        }
    }
}
