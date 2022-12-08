using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.BrawlStructs
{
    enum MoveType
    {
        Normal,
        Unique,
        Special,
        Gap
    }
    class Move
    {
        public string name;
        public int score;
        public int multiplier;
        public MoveType type;
        public Move(string name, MoveType type, int score=100, int multiplier=1)
        {
            this.name = name;
            this.type = type;
            this.score = score;
            this.multiplier = multiplier;
        }

        public override string ToString()
        {
            switch (type)
            {
                case MoveType.Normal:
                    return name;
                case MoveType.Unique:
                    return $"<color=#FFF4AA>{name}</color>";
                case MoveType.Special:
                    return $"<color=#FCBA03>{name}</color>";
                case MoveType.Gap:
                    return $"<color=#0049FF>{name}</color>";
                default:
                    return name;
            }
        }
    }
    static class Data
    {
        static Dictionary<int[], Move> moveNames = new Dictionary<int[], Move>(new EqualityComparer())
        {            
            { new int[]{ 1, 2 }, new Move("Double Jab", MoveType.Normal, 100)},
            { new int[]{ 1 }, new Move("Jab", MoveType.Normal, 50) },
            { new int[]{ 2 }, new Move("Jab 2", MoveType.Normal, 100) },
            { new int[]{ 3 }, new Move("Jab 3", MoveType.Normal, 150) },
            { new int[]{ 4 }, new Move("Rapid Jab", MoveType.Normal, 100) },
            { new int[]{ 5 }, new Move("Dash Attack", MoveType.Normal, 200) },
            { new int[]{ 6 }, new Move("Forward Tilt", MoveType.Normal) },
            { new int[]{ 7 }, new Move("Forward Tilt 2", MoveType.Normal) },
            { new int[]{ 9 }, new Move("Up Tilt", MoveType.Normal) },
            { new int[]{ 10 }, new Move("Down Tilt", MoveType.Normal) },
            { new int[]{ 11 }, new Move("Forward Smash", MoveType.Normal, 500) },
            { new int[]{ 12 }, new Move("Up Smash", MoveType.Normal, 500) },
            { new int[]{ 13 }, new Move("Down Smash", MoveType.Normal, 500) },
            { new int[]{ 14 }, new Move("Neutral Air", MoveType.Normal) },
            { new int[]{ 15 }, new Move("Forward Air", MoveType.Normal) },
            { new int[]{ 16 }, new Move("Back Air", MoveType.Normal) },
            { new int[]{ 17 }, new Move("Up Air", MoveType.Normal) },
            { new int[]{ 18 }, new Move("Down Air", MoveType.Normal) },
            { new int[]{ 19 }, new Move("Neutral B", MoveType.Normal) },
            { new int[]{ 58 }, new Move("Side B", MoveType.Normal) },
            { new int[]{ 59 }, new Move("Up B", MoveType.Normal) },
            { new int[]{ 60 }, new Move("Down B", MoveType.Normal) },
            { new int[]{ 62 }, new Move("Getup Attack", MoveType.Normal) },
            { new int[]{ 65 }, new Move("Pummel", MoveType.Normal, 10, 0) },
            { new int[]{ 66 }, new Move("Forward Throw", MoveType.Normal) },
            { new int[]{ 67 }, new Move("Back Throw", MoveType.Normal) },
            { new int[]{ 68 }, new Move("Up Throw", MoveType.Normal) },
            { new int[]{ 69 }, new Move("Down Throw", MoveType.Normal) },
            { new int[]{ 75 }, new Move("Ledge Getup Attack", MoveType.Normal) },
            { new int[]{ 77 }, new Move("Glide Attack", MoveType.Normal) },
            { new int[]{ 109 }, new Move("Link Bomb", MoveType.Normal) },
            { new int[]{ 110 }, new Move("Gyro Throw", MoveType.Normal) },
        };

        static Dictionary<int[], Move> falconMoves = moveNames.MergeLeft(new Dictionary<int[], Move>(new EqualityComparer())
        {
            { new int[]{ 1, 2, 3 }, new Move("Gentleman", MoveType.Unique, 200)},
            { new int[]{ 18, 15 }, new Move("The Classic", MoveType.Gap, 500, 2) },
            { new int[]{ 18, 15, 19 }, new Move("Sacred Combo", MoveType.Gap, 10000) },
            { new int[]{ 19, 19 }, new Move("How?", MoveType.Gap, 10000) },
            { new int[]{ 15 }, new Move("Knee", MoveType.Unique, 500) },
            { new int[]{ 18 }, new Move("Stomp", MoveType.Normal, 250) },
            { new int[]{ 19 }, new Move("Falcon Punch", MoveType.Special, 5000) },
            { new int[]{ 58 }, new Move("Raptor Boost", MoveType.Unique, 500) },
            { new int[]{ 59 }, new Move("Falcon Dive", MoveType.Unique, 500) },
            { new int[]{ 60 }, new Move("Falcon Kick", MoveType.Unique, 1000) },
        }).LengthSort();

        static Dictionary<int[], Move> ganonMoves = moveNames.MergeLeft(new Dictionary<int[], Move>(new EqualityComparer())
        {
            { new int[]{ 18, 18 }, new Move("Curb Stomper", MoveType.Gap, 200) },
            { new int[]{ 18, 18, 18 }, new Move("Stop he's already dead", MoveType.Gap, 200) },
            { new int[]{ 58, 60 }, new Move("Sakurai Combo", MoveType.Gap, 200) },
            { new int[]{ 67, 60 }, new Move("Roger Combo", MoveType.Gap, 200) },
            { new int[]{ 15 }, new Move("The Fist", MoveType.Unique, 500) },
            { new int[]{ 18 }, new Move("Stomp", MoveType.Normal, 500) },
            { new int[]{ 9 }, new Move("Volcano Kick", MoveType.Unique, 500) },
            { new int[]{ 6 }, new Move("Spartan Kick", MoveType.Normal, 500) },
            { new int[]{ 10 }, new Move("Leg Sweep", MoveType.Normal, 250) },
            { new int[]{ 19 }, new Move("Dead Man's Volley", MoveType.Unique, 5000) },
            { new int[]{ 58 }, new Move("Flame Choke", MoveType.Unique, 500) },
            { new int[]{ 59 }, new Move("Dark Dive", MoveType.Unique, 500) },
            { new int[]{ 60 }, new Move("Wizard's Foot", MoveType.Unique, 1000) },
        }).LengthSort();

        static Dictionary<int[], Move> marthMoves = moveNames.MergeLeft(new Dictionary<int[], Move>(new EqualityComparer())
        {
            { new int[]{ 15, 18 }, new Move("Ken Combo", MoveType.Gap, 500)},
        }).LengthSort();

        static Dictionary<int, Dictionary<int[], Move>> charMoves = new Dictionary<int, Dictionary<int[], Move>>
        {
            { 0x9, falconMoves},
            { 0x11, marthMoves},
            { 0x14, ganonMoves},
        };

        public static Dictionary<int[], Move> MergeLeft(this Dictionary<int[], Move> me, params Dictionary<int[], Move>[] others)
        {
            Dictionary<int[], Move> newMap = new Dictionary<int[], Move>(new EqualityComparer());
            foreach (IDictionary<int[], Move> src in
                (new List<IDictionary<int[], Move>> { me }).Concat(others))
            {
                // ^-- echk. Not quite there type-system.
                foreach (KeyValuePair<int[], Move> p in src)
                {
                    newMap[p.Key] = p.Value;
                }
            }
            return newMap;
        }

        public static Dictionary<int[], Move> LengthSort<T>(this T me) where T : Dictionary<int[], Move>, new()
        {
            return me.OrderBy(x => -x.Key.Length).ToDictionary(x => x.Key, x => x.Value, (new EqualityComparer()));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static bool Matches(List<int> a, int[] b, int startIndex)
        {
            if (b.Length + startIndex > a.Count) return false;
            for (int i = 0; i < b.Length; i++) {
                if (a[startIndex + i] != b[i]) return false;
            }
            return true;
        }

        public static List<T> Peek<T>(this List<T> a, int amount)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(a[a.Count-1-i]);
            }
            return result;
        }

        public static Tuple<string, int, int> GetComboString(int charId, List<int> moveIds, AudioSource audioSource)
        {
            Dictionary<int[], Move> moveDict = charMoves.GetValueOrDefault(charId, moveNames);
            int score = 0;
            int multiplier = 0;
            List<string> names = new List<string>();
            Move move = null;
            for (int i = 0; i < moveIds.Count;)
            {
                List<int> capturedGroup = null;
                foreach (KeyValuePair<int[], Move> moveMatch in moveDict)
                {
                    if (moveMatch.Key.Length == 1)
                    {
                        move = moveDict.GetValueOrDefault(new int[]{ moveIds[i] }, new Move("Unk:" + moveIds[i], MoveType.Normal));
                        i++;
                        break;
                    }
                    if (Matches(moveIds, moveMatch.Key, i))
                    {
                        move = moveMatch.Value;
                        capturedGroup = moveIds.Peek(moveMatch.Key.Length);
                        i += capturedGroup.Count;
                        break;
                    }
                }
                if (move.type == MoveType.Gap)
                {
                    capturedGroup.Reverse();
                    foreach (int moveid in capturedGroup)
                    {
                        Move tmp = moveDict.GetValueOrDefault(new int[]{ moveid }, new Move("Unk:" + moveid, MoveType.Normal));
                        names.Add(tmp.ToString());
                        score += tmp.score;
                        multiplier += tmp.multiplier;
                    }
                }
                names.Add(move.ToString());
                score += move.score;
                multiplier += move.multiplier;
            }
            if (move.type == MoveType.Gap || move.type == MoveType.Special)
            {
                audioSource.Play(0);
            }
            return new Tuple<string, int, int>(string.Join(" + ", names), score, multiplier);
        }
    }
}
