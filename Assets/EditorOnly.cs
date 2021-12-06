using UnityEngine;

public class EditorOnly : MonoBehaviour
{
#if !UNITY_EDITOR
    private void Awake()
    {
        gameObject.SetActive(false);
    }
#endif
}
