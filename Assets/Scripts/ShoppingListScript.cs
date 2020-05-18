using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ShoppingListScript : MonoBehaviour
{
    private bool _visible;
    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _visible = true;
        _renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_visible)
            {
                _renderer.enabled = false;
                HideChildren();
                _visible = !_visible;
            }
            else
            {
                _renderer.enabled = true;
                ShowChildren();
                _visible = !_visible;
                Debug.Log("wqqw");
            }
        }
    }

    private void ShowChildren()
    {
        TextMeshProUGUI[] tMPros = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tm in tMPros)
        {
            tm.enabled = true;
        }
    }

    private void HideChildren()
    {
        TextMeshProUGUI[] tMPros = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tm in tMPros)
        {
            tm.enabled = false;
        }
    }

    public List<GameObject> CreateShoppingList(int size, GameObject[] foodObjects)
    {
        var shoppingList = new List<GameObject>();

        //TODO: maybe remove selected goodFood
        for (int i = 0; i < size; i++)
        {
            shoppingList.Add(foodObjects[Random.Range(0, foodObjects.Length - 1)]);
        }

        return shoppingList;
    }
}
