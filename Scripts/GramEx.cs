using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class GramEx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(locateOrder("name1.txt", "MFA-00.1247b.jpg"));
        //Debug.Log(locateCos("name1Cos.txt", 16, 9));
        //Debug.Log(Application.dataPath);
        MainFlow();
    }

    public int locateOrder(string txtName, string imageName)
    {
        int count = 0;
        string line;
        StreamReader file = new StreamReader(txtName);
        while ((line = file.ReadLine()) != null)
        {
            if (line == imageName)
                return count;
            count++;
        }
        return 999;
    }

    public double locateCos(string hdf5txt,int x, int y)
    {
        int count = 0;
        string line;
        
        StreamReader file = new StreamReader(hdf5txt);
        while((line = file.ReadLine())!= null)
        {
            if(count == x)
            {
                string[] value = line.Split('\t');
                return double.Parse(value[y]);
            }
            count++;
        }
        return 999;
    }

    public void findBestCos(List<string> setImageName)
    {
        DBStru newInfo = new DBStru();
        List<int> setImageLocation = new List<int>();
        List<double> setCosValue = new List<double>();
        Dictionary<string, double> test = new Dictionary<string, double>();
        for(int i = 0; i<5; i++)
        {
            setImageLocation.Add(locateOrder("nameCollect.txt", setImageName[i]));
        }
        //foreach (string name in setImageName)
        //{
        //    setImageLocation.Add(locateOrder("name"+(setNum+1).ToString()+".txt",name));            
        //}

        test.Add((setImageName[0] + setImageName[1] + " 12"), locateCos("GramEx.txt", setImageLocation[0], setImageLocation[1]));
        test.Add((setImageName[0] + setImageName[2] + " 13"), locateCos("GramEx.txt", setImageLocation[0], setImageLocation[2]));
        test.Add((setImageName[0] + setImageName[3] + " 14"), locateCos("GramEx.txt", setImageLocation[0], setImageLocation[3]));
        test.Add((setImageName[0] + setImageName[4] + " 15"), locateCos("GramEx.txt", setImageLocation[0], setImageLocation[4]));
        test.Add((setImageName[1] + setImageName[2] + " 23"), locateCos("GramEx.txt", setImageLocation[1], setImageLocation[2]));
        test.Add((setImageName[1] + setImageName[3] + " 24"), locateCos("GramEx.txt", setImageLocation[1], setImageLocation[3]));
        test.Add((setImageName[1] + setImageName[4] + " 25"), locateCos("GramEx.txt", setImageLocation[1], setImageLocation[4]));
        test.Add((setImageName[2] + setImageName[3] + " 34"), locateCos("GramEx.txt", setImageLocation[2], setImageLocation[3]));
        test.Add((setImageName[2] + setImageName[4] + " 35"), locateCos("GramEx.txt", setImageLocation[2], setImageLocation[4]));
        test.Add((setImageName[3] + setImageName[4] + " 45"), locateCos("GramEx.txt", setImageLocation[3], setImageLocation[4]));
        
        newInfo.bestCos = test.Select(x => x.Value).Max();
        newInfo.userChoice = test.FirstOrDefault(x => x.Value == newInfo.bestCos).Key;
        
        DB.RecordBestPair(newInfo);
    }

    public void MainFlow()
    {
        int lineCount = 1;
        int setImageCount = 1;
        string line;
        List<string> setImageName = new List<string>();

        StreamReader file = new StreamReader("nameCollect.txt");
        while ((line = file.ReadLine()) != null)
        {
            if (setImageCount == 5)
            {
                setImageName.Add(line);
                //if (lineCount == 100)
                //    Debug.Log(setImageName[0]+setImageName[4]);
                findBestCos(setImageName);
                setImageName.Clear();
                setImageCount = 1;
            }
            else
            {
                setImageName.Add(line);
                setImageCount++;
            }
            lineCount++;

        }

    }
}
