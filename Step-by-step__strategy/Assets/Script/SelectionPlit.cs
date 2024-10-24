using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionPlit : MonoBehaviour
{
    [SerializeField]  Image img;

    private void Start()
    {
        img = this.transform.GetChild(0).GetComponent<Image>();
    }

    public void RedPlit()
    {
        img.color = Color.red;
    }
    public void WhitePlit()
    {
        img.color = Color.white;
    }
}
