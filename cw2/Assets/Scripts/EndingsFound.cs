using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingsFound : MonoBehaviour
{
    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;
    [SerializeField] private GameObject image3;
    [SerializeField] private GameObject image4;
    [SerializeField] private GameObject image5;
    [SerializeField] private GameObject image6;

    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;
    [SerializeField] private TMP_Text text5;
    [SerializeField] private TMP_Text text6;

    public static bool ending1 = false;
    public static bool ending2 = false;
    public static bool ending3 = false;
    public static bool ending4 = false;
    public static bool ending5 = false;
    public static bool ending6 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image1.SetActive(ending1);
        image2.SetActive(ending2);
        image3.SetActive(ending3);
        image4.SetActive(ending4);
        image5.SetActive(ending5);
        image6.SetActive(ending6);

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "Home Screen");
    }

    public static void setComplete(int level)
    {
        switch (level)
        {
            case 1:
                ending1 = true;
                break;
            case 2:
                ending2 = true;
                break;
            case 3:
                ending3 = true;
                break;
            case 4:
                ending4 = true;
                break;
            case 5:
                ending5 = true;
                break;
            case 6:
                ending6 = true;
                break;

        }
    }
}
