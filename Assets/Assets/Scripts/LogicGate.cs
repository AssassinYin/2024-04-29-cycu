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

    }

    private void StrikeLightning()
    {

    }

    private void SentShockwave()
    {
        Cable.OpenCable();
    }

    private void DamagingState()
    {

    }
}