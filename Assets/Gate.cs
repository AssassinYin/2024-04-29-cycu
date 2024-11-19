using UnityEngine;

public class Gate : MonoBehaviour
{
    public Sprite And, Or, Nand, Nor, Xnor;
    public SpriteRenderer SpriteRenderer { get; private set; }
    private string _animName;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite("And");
    }

    public void SetSprite(string animName)
    {
        _animName = animName;
        SpriteRenderer.sprite = And;
    }
}
