using UnityEngine;

public enum GateType { And, Or, Nand, Nor, Xnor }

public class Gate : MonoBehaviour
{
    public Sprite And, Or, Nand, Nor, Xnor;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public GateType AnimName { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite(GateType.And);
    }

    public void SetSprite(GateType animName)
    {
        switch (animName)
        {
            case GateType.And:
                SpriteRenderer.sprite = And;
                break;
            case GateType.Or:
                SpriteRenderer.sprite = Or;
                break;
            case GateType.Nand:
                SpriteRenderer.sprite = Nand;
                break;
            case GateType.Nor:
                SpriteRenderer.sprite = Nor;
                break;
            case GateType.Xnor:
                SpriteRenderer.sprite = Xnor;
                break;
        }
    }
}
