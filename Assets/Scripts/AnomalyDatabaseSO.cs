using UnityEngine;
using System;
using System.Collections.Generic;

public class AnomalyDatabase : ScriptableObject
{
    [Serializable] public class Entry
    {
        public string anomalyId;
        public bool   discovered;
    }

    public List<Entry> entries = new();
    public int DiscoveredCount => entries.FindAll(e => e.discovered).Count;

    private const string FILE = "anomaly_save.json";

    [Serializable] class SaveWrapper
    {
        public List<Entry> entries = new();
    }

    public void Load()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, FILE);
        if (!System.IO.File.Exists(path)) return;

        var json = System.IO.File.ReadAllText(path);
        var data = JsonUtility.FromJson<SaveWrapper>(json);
        if (data != null && data.entries != null)
            entries = data.entries;
    }

    public void Save()
    {
        var data = new SaveWrapper { entries = entries };
        string json = JsonUtility.ToJson(data, true);
        string path = System.IO.Path.Combine(Application.persistentDataPath, FILE);
        System.IO.File.WriteAllText(path, json);
    }

    public bool RegisterGoodDecision(string anomalyId)
    {
        var e = entries.Find(x => x.anomalyId == anomalyId);
        if (e == null) return false;

        if (e.discovered) return false;

        e.discovered = true;
        Save();
        return true;
    }
}