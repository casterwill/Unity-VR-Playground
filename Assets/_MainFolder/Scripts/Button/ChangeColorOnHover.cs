using UnityEngine;

public class ChangeColorOnHover : MonoBehaviour
{
    private Color HoverEnterColor = Color.yellow;
    private Color HoverExitColor = Color.green;
    private Color PokeButtonColor = Color.red;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterHover()
    {
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", HoverEnterColor);
    }

    public void ExitHover()
    {
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", HoverExitColor);
    }

    public void PokeButton()
    {
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", PokeButtonColor);
    }
}
