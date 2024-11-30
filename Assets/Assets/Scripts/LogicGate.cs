using Unity.VisualScripting;
using UnityEngine;

public class LogicGate: EntityHealth
{
    public Gate Gate;
    public Cable Cable;
    public Switch Switch1, Switch2;

    private void Update()
    {
        
    }

    private void SwitchGate()
    {
        int i = Random.Range(0, 4);

    }

    private void StrikeLightning()
    {

    }

    private void SentShockwave()
    {
        Cable.OpenCable();
    }

    //private void DamagingState()
    //{
    //    switch (Gate.AnimName)
    //    {
    //        case GateType.And:
    //            if (Switch1.AnimName && Switch2.AnimName)
    //                ;
    //            break;
    //        case GateType.Or:
    //            if (Switch1.AnimName || Switch2.AnimName)
    //                ;
    //            break;
    //        case GateType.Nand:
    //            if (!(Switch1.AnimName && Switch2.AnimName))
    //                ;
    //            break;
    //        case GateType.Nor:
    //            if (Switch1.AnimName ^ Switch2.AnimName)
    //                ;
    //            break;
    //        case GateType.Xnor:
    //            if (!(Switch1.AnimName ^ Switch2.AnimName))
    //                ;
    //            break;
    //    }
    //}
}