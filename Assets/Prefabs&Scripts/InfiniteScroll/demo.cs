using Mopsicus.InfiniteScroll;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class demo : MonoBehaviour {

    [SerializeField]
    private InfiniteScroll Scroll;

    [SerializeField]
    private int Count = 100; //số lượng max item show ra

    void Start () {
        Scroll.OnFill += OnFillItem;
        Scroll.OnHeight += OnHeightItem;

        Scroll.InitData (Count);
    }

    void OnFillItem (int index, GameObject item) {
        item.GetComponentInChildren<Text> ().text = index.ToString ();
    }

    int OnHeightItem (int index) {
        return 150;
    } //chiều cao của item (recttransform)

    public void SceneLoad (int index) {
        SceneManager.LoadScene (index);
    }

}