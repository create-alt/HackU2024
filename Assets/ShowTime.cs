using UnityEngine;
using TMPro;
public class ShowTime : MonoBehaviour
{
    public static float time = 45;
    [SerializeField] TextMeshProUGUI time_object;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // テキストの表示を入れ替える
        time_object.text = "time : " + ((int)time).ToString();
    }
}
