using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.BrawlStructs;
using DolphinMemoryEngine;

public class BrawlPlayer : MonoBehaviour
{
    public int id;
    public float Damage
    {
        get => DME.Get<float>($"/Damage/P{id} Damage");
    }

    public float Weight
    {
        get => DME.Get<float>($"/Weight/P{id}");
    }

    public uint CharID
    {
        get => DME.Get<uint>($"/Char ID/P{id}");
    }

    public uint TotalJumps
    {
        get => DME.Get<uint>($"/Total Jumps/P{id}");
    }

    public uint FramesUntilHitstunEnds
    {
        get => DME.Get<uint>($"/Misc Timers/Hitstun Frames Left/P{id}");
    }
    public uint StockCount
    {
        get => DME.Get<uint>($"/Current StockCount/P{id}");
    }

    public float FramesUntilGrabRelease
    {
        get => DME.Get<float>($"/Mashout Data/Frames till Grab release/P{id}");
    }

    public float RecievedKnockback
    {
        get => DME.Get<float>($"/Recieved Hit Data/P{id}/Knockback Velocity");
    }

    public uint ComboCounter
    {
        get => DME.Get<uint>($"/Combo Counter/P{id}");
    }

    public Vector3 Position
    {
        get {
            return DMEex.GetVector($"/Positional Data/Position/P{id} Position X",
                                 $"/Positional Data/Position/P{id} Position Y",
                                 $"/Positional Data/Position/P{id} Position Z");
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return DMEex.GetVector($"/Positional Data/Total Velocity/P{id} Total Velocity X",
                                 $"/Positional Data/Total Velocity/P{id} Total Velocity Y",
                                 $"/Positional Data/Total Velocity/P{id} Total Velocity Z");
        }
    }

    public Hitbox HitByHitbox
    {
        get
        {
            Hitbox result = new Hitbox();
            result.damage = DME.Get<uint>($"/Recieved Hit Data/P{id}/Hitbox Damage");
            result.offset = DMEex.GetVector($"/Recieved Hit Data/P{id}/Hitbox X Offset",
                                           $"/Recieved Hit Data/P{id}/Hitbox Y Offset",
                                           $"/Recieved Hit Data/P{id}/Hitbox Z Offset");
            result.size = DME.Get<float>($"/Recieved Hit Data/P{id}/Hitbox Size");
            result.trajectory = DME.Get<uint>($"/Recieved Hit Data/P{id}/Hitbox Trajectory");
            result.KBG = DME.Get<uint>($"/Recieved Hit Data/P{id}/KBG");
            result.WDSK = DME.Get<uint>($"/Recieved Hit Data/P{id}/WDSK");
            result.BKB = DME.Get<uint>($"/Recieved Hit Data/P{id}/BKB");
            result.triprate = DME.Get<uint>($"/Recieved Hit Data/P{id}/Trip Rate");
            result.hitlagMult = DME.Get<float>($"/Recieved Hit Data/P{id}/Hitlag Mult");
            result.sdiMult = DME.Get<float>($"/Recieved Hit Data/P{id}/SDI Mult");
            result.flags = DME.Get<uint>($"/Recieved Hit Data/P{id}/Hitbox Flags");
            return result;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
