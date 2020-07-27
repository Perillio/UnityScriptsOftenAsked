using UnityEngine;
using System.Data;
using System.IO;

// Use EasySaveAndLoadData.SaveData as a bool (e.g.: if(EasySaveAndLoadData.SaveData("DATA","DATANAME") == true) then ...) 
// Use EasySaveAndLoadData.LoadData(string dataName) which returns the data as string.

// This script creates xml files according to the name you give the data.

public class EasySaveAndLoadData : MonoBehaviour
{
    public DataTable saving = new DataTable("saving");
    public bool SaveData(string data, string dataName)
    {
        saving = new DataTable("saving");
        if (data == "")
        {
            return false;
        }
        if(dataName == "")
        {
            return false;
        }
        if (File.Exists($"{dataName}.xml"))
        {
            File.Delete($"{dataName}.xml");
        }
        saving.Columns.Add(dataName, typeof(string));
        saving.Rows.Add(data);
        saving.WriteXml($"{dataName}.xml", XmlWriteMode.WriteSchema);
        return true;
    }
    public string LoadData(string dataName)
    {
        if (File.Exists($"{dataName}.xml"))
        {
            saving = new DataTable("saving");
            saving.ReadXml($"{dataName}.xml");
            DataRow r = saving.Rows[0];
            string result = r[$"{dataName}"].ToString();
            return result;
        }
        return "";
    }
}
