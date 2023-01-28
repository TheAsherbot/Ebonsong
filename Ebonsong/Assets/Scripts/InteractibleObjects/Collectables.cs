using UnityEngine;
using PlayerNameSpace;

public class Collectables : MonoBehaviour
{
    public virtual void OnCollect()
    {
        Destroy(gameObject);
    }
}
