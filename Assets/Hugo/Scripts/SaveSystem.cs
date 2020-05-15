using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveSkin()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/skin.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript();

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveSkin(bool[] skins)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/skin.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript(skins);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static DataScript LoadSkin()
    {

        string path = Application.persistentDataPath + "/skin.data";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataScript data = formatter.Deserialize(stream) as DataScript;
            stream.Close();

            return data;
        }
        else
        {
            SaveSkin();
            return LoadSkin();
        }
    }

    public static void SaveMoney(SkinMenu money)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/money.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript(money);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static DataScript LoadMoney()
    {

        string path = Application.persistentDataPath + "/money.data";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataScript data = formatter.Deserialize(stream) as DataScript;
            stream.Close();

            return data;
        }
        else
        {

            return LoadSkin();
        }
    }
}
