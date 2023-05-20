using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase
{
    private static DataBase instance;

    public static DataBase Instance()
    {
        if (instance == null)
        {
            instance = new DataBase();
        }
        return DataBase.instance;
    }

    public ItemData SetData(int _id /*, string _type, string _name, string _spName, string _info*/)
    {
        DataManager.Instance().LoadDatas();

        var data = DataManager.Instance().dicItemData[_id];

        return data;
    }
}
