using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Dictionary

public class DataManager
{
    private static DataManager instance;
    public Dictionary<int, ItemData> dicItemData;

    private DataManager() { }

    public static DataManager Instance()
    {
        if (instance == null)
        {
            instance = new DataManager();
        }
        return instance;
    }

    public void LoadDatas()
    {
        var json = Resources.Load<TextAsset>("ItemData/ItemData").text;
        var arrItemData = JsonConvert.DeserializeObject<ItemData[]>(json);
        dicItemData = arrItemData.ToDictionary(x => x.Id);
    }
}
