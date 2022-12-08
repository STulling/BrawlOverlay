using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolphinMemoryEngine;
using DolphinMemoryEngine.MemoryWatch;
using UnityEngine;

namespace Assets.BrawlStructs
{
    public static class DMEex
    {

        public static Vector3 GetVector(string tagX, string tagY, string tagZ)
        {
            float x = DME.Get<float>(tagX);
            float y = DME.Get<float>(tagY);
            float z = DME.Get<float>(tagZ);
            return new Vector3(x, y, z);
        }

        private static uint[] prevStaleQueue = new uint[4];
        public static bool HasStaleQueueChanged(int id)
        {
            uint offset = DME.GetResolvedPointer($"/Staling Queue/P{id}");
            uint val = DME.Read<uint>(offset + 0x8);
            bool result = false;
            if (val != prevStaleQueue[id])
            {
                result = true;
            }
            prevStaleQueue[id] = val;
            return result;
        }

        public static (List<int>, int) GetStaleQueue(int id)
        {
            uint offset = DME.GetResolvedPointer($"/Staling Queue/P{id}");
            byte[] vals = DME.ReadBytes((uint)offset + 0xc, 9*3*4);
            int[] queue = new int[9 * 3];
            Buffer.BlockCopy(vals, 0, queue, 0, vals.Length);
            Dictionary<int, int> actions = new Dictionary<int, int>();
            int hash = 0;
            for (int i = 0; i < 9; i++)
            {
                actions[queue[i*3 + 2]] = queue[i*3 + 1];
                hash += queue[i * 3 + 2];
                hash += queue[i * 3 + 1];
            }
            List<int> result = new List<int>();
            foreach (KeyValuePair<int, int> actionInfo in actions.OrderBy(x => x.Key))
            {
                if (actionInfo.Key != 0) result.Add(actionInfo.Value);
            }
            return (result, hash);
        }
    }
}
