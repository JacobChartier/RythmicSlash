using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject menu1;
    [SerializeField] private GameObject menu2;
    [SerializeField] private GameObject menu3;

    [Header("Points in scene")]
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;


    void Update()
    {
        if (PauseMenu.activeSelf == false)
        {
            if (point1.position.x >= this.transform.position.x)
            {
                menu1.SetActive(true);
            }
            else
            {
                menu1.SetActive(false);
            }

            if (point1.position.x < this.transform.position.x && point2.position.x >= this.transform.position.x)
            {
                menu2.SetActive(true);
            }
            else
            {
                menu2.SetActive(false);
            }

            if (point2.position.x < this.transform.position.x && point3.position.x >= this.transform.position.x)
            {
                menu3.SetActive(true);
            }
            else
            {
                menu3.SetActive(false);
            }
        }
        else
        {
            menu1.SetActive(false);
            menu2.SetActive(false);
            menu3.SetActive(false);
        }
    }
}
