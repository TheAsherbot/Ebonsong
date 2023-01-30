using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpening;
    private Transform topSprite;
    private Transform bottumSprite;

    private void Awake()
    {
        topSprite = transform.GetChild(0);
        bottumSprite = transform.GetChild(1);
    }

    private void Update()
    {
        if (isOpening == true)
        {
            topSprite.localScale -= new Vector3(0, 1) * Time.deltaTime;
            bottumSprite.localScale += new Vector3(0, 1) * Time.deltaTime;
            if (bottumSprite.localScale.y >= 0 || topSprite.localScale.y <= 0)
            {
                DestroyImmediate(this.gameObject);
            }
        }
    }

    public void OpenDoors()
    {
        isOpening = true;
    }
}
