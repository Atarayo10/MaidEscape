using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cutScene : MonoBehaviour
{
    [SerializeField] Image image;
    Sprite[] cutscene;
    int ran;

    private void Start()
    {
        ran = Random.Range(0, 2);
        image.sprite = cutscene[ran];
    }
}    

