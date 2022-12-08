using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using DolphinMemoryEngine;
using DolphinMemoryEngine.MemoryWatch;
using DolphinMemoryEngine.Common;

public class Initializer : MonoBehaviour
{
    public void Awake()
    {
        DME.Init();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        string jsonText = File.ReadAllText(Application.dataPath + "/StreamingAssets/All_Data_DME_file.dmw");
        PopulateLabels(jsonText);
    }

    [System.Serializable]
    public class WatchList
    {
        public List<Node> watchList;
    }

    [System.Serializable]
    public class Node
    {
        public string label;
        public List<string> pointerOffsets;
        public int typeIndex;
        public string address;
        public bool unsigned;
        public string groupName;
        public List<Node> groupEntries;

        public bool IsLeaf()
        {
            return groupEntries == null;
        }
    }
    private List<Node> flatten(WatchList watchList)
    {
        List<Node> result = new List<Node>();
        foreach (Node node in watchList.watchList)
        {
            result.AddRange(flatten(node));
        }
        return result;
    }
    private List<Node> flatten(Node node, string Path = "")
    {
        List<Node> result = new List<Node>();
        if (node.IsLeaf())
        {
            node.label = $"{Path}/{node.label}";
            result.Add(node);
        }
        else
        {
            foreach (Node child in node.groupEntries)
            {
                string newPath = $"{Path}/{node.groupName}";
                result.AddRange(flatten(child, newPath));
            }
        }
        return result;
    }
    private void PopulateLabels(string json)
    {
        WatchList watchList = Newtonsoft.Json.JsonConvert.DeserializeObject<WatchList>(json);
        List<Node> entries = flatten(watchList);
        foreach (Node item in entries)
        {
            MemEntry entry = null;
            List<int> pointerOffsets = null;
            if (item.pointerOffsets != null)
            {
                pointerOffsets = new List<int>();
                foreach (string val in item.pointerOffsets)
                {
                    pointerOffsets.Add(int.Parse(val, System.Globalization.NumberStyles.HexNumber));
                }
            }
            int typeIndex = item.typeIndex;
            string label = item.label;
            uint address = uint.Parse(item.address, System.Globalization.NumberStyles.HexNumber);
            bool unsigned = item.unsigned;
            if (typeIndex == (int)MemType.type_byte)
                entry = new MemEntry<byte>(label, address, unsigned, pointerOffsets != null, pointerOffsets);
            if (typeIndex == (int)MemType.type_halfword)
                entry = new MemEntry<ushort>(label, address, unsigned, pointerOffsets != null, pointerOffsets);
            if (typeIndex == (int)MemType.type_word)
                entry = new MemEntry<uint>(label, address, unsigned, pointerOffsets != null, pointerOffsets);
            if (typeIndex == (int)MemType.type_float)
                entry = new MemEntry<float>(label, address, unsigned, pointerOffsets != null, pointerOffsets);
            if (typeIndex == (int)MemType.type_double)
                entry = new MemEntry<double>(label, address, unsigned, pointerOffsets != null, pointerOffsets);
            
            DME.AddEntry(label, entry);
        }
    }
}
