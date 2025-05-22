using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject heartParent;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private List<Image> heartImage = new List<Image>();

    private void Start()
    {
        if (health != null)
        {
            for (int i = 0; i < health; i++)
            {
                GameObject heart = Instantiate(heartPrefab, heartParent.transform);
                heartImage.Add(heart.GetComponent<Image>());
            }
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            Destroy(this.gameObject);
        }
        HeartsUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            health -= 1f;
            Destroy(other);
        }
    }

    public void HeartsUpdate()
    {
        for (int i = 0; i < heartImage.Count; i++)
        {
            if (i < health)
            {
                heartImage[i].sprite = fullHeart;
            }
            else
            {
                heartImage[i].sprite = emptyHeart;
            }
        }
    }
}
