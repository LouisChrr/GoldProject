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

    public static void SaveSkin(bool[] skins, bool[] skinsEquipped)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/skin.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript(skins, skinsEquipped);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save");

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
            Debug.Log("Skin File not found");
            SaveSkin();
            return LoadSkin();
        }
    }

    public static void SaveMoney(ScoreManager money)
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
            Debug.Log("Money File not found");
            SaveMoney(new ScoreManager());
            return LoadMoney();
        }
    }

    public static void SaveBestScore(MenuManager score)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/bestScore.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript(score);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static DataScript LoadBestScore()
    {

        string path = Application.persistentDataPath + "/bestScore.data";

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
            Debug.Log("BestScore File not found");
            SaveBestScore(new MenuManager());
            return LoadBestScore();
        }
    }

    public static void SaveNbDeathsAndNbWalls(GameManager deathAndWall)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/deathAndWall.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript(deathAndWall);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static DataScript LoadNbDeathsAndNbWalls()
    {

        string path = Application.persistentDataPath + "/deathAndWall.data";

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
            Debug.Log("DeathsAndWalls File not found");
            SaveNbDeathsAndNbWalls(new GameManager());
            return LoadNbDeathsAndNbWalls();
        }
    }

    //public static void SaveNbWalls(BilleMovement nbWalls)
    //{

    //    BinaryFormatter formatter = new BinaryFormatter();

    //    string path = Application.persistentDataPath + "/nbWalls.data";
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    DataScript data = new DataScript(nbWalls);

    //    formatter.Serialize(stream, data);
    //    stream.Close();
        
    //}

    //public static DataScript LoadNbWalls()
    //{

    //    string path = Application.persistentDataPath + "/nbWalls.data";

    //    if (File.Exists(path))
    //    {

    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(path, FileMode.Open);

    //        DataScript data = formatter.Deserialize(stream) as DataScript;
    //        stream.Close();

    //        return data;
    //    }
    //    else
    //    {
    //        Debug.Log("Nb Walls File not found");
    //        SaveNbWalls(new BilleMovement());
    //        return LoadNbWalls();
    //    }
    //}

    public static void SaveBoughtSkins(SkinMenu boughtSkins)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/boughtSkins.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataScript data = new DataScript(boughtSkins);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static DataScript LoadBoughtSkins()
    {

        string path = Application.persistentDataPath + "/boughtSkins.data";

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
            Debug.Log("Bought Skins File not found");
            SaveBoughtSkins(new SkinMenu());
            return LoadBoughtSkins();
        }
    }
}
