using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;

public class DB
{
    //public static void RecordInfo(DBStru newInfo)
    //{
    //    using(var db = new LiteDatabase("ExperInfo.db"))
    //    {
    //        var col = db.GetCollection<DBStru>("rankInfo");
    //        col.Insert(newInfo); 
    //    }
    //}
    public static void RecordBestPair(DBStru newInfo)
    {
        using (var db = new LiteDatabase("BestCos.db"))
        {
            var col = db.GetCollection<DBStru>("Cos");
            col.Insert(newInfo);
        }
    }

    public static void UpdateInfo(DBStru newInfo)
    {
        using (var db = new LiteDatabase("ExperInfo.db"))
        {
            var col = db.GetCollection<DBStru>("rankInfo");
            col.Update(newInfo);
        }
    }

    //public  static void QueryCardsInfoString(string newInfo)
    //{
    //    using (var db = new LiteDatabase("ExperInfo.db"))
    //    {
    //        var col = db.GetCollection<DBStru>("rankInfo");
    //        col.EnsureIndex(x => x.cardsInfo, "$.cardsInfo[*]");
            
    //        //var result = col.FindOne(x => ((IList<string>)x.cardsInfo).Contains(newInfo));

    //        var result = col.Find(Query.Contains("cardsInfo",newInfo));
    //        foreach (DBStru resualtBranch in result)
    //            Debug.Log(resualtBranch.cardsInfo[0]);
    //    }
    //}

    public static DBStru QueryBaseOnId(int id)
    {
        using (var db = new LiteDatabase("ExperInfo.db"))
        {
            var col = db.GetCollection<DBStru>("rankInfo");
            col.EnsureIndex(x => x.id);
            return col.FindOne(x => x.id == id);
        }
    }

}
