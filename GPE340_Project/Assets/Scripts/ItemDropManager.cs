using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroppableItem
{
    public GameObject itemToDrop;
    public float chanceOfDrop = 1.0f; 
}

public class ItemDropManager : MonoBehaviour
{   
    [SerializeField] 
    private Vector3 dropOffset;
    public DroppableItem[] dropTable;
    public float[] CDA;
    public GameObject chosenItem;

    // Start is called before the first frame update
    void Start()
    {
        AssignCDA();
        chosenItem = ObtainRandomObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        SpawnItemDrop(chosenItem);
    }

    public void AssignCDA()
    {
        CDA = new float [dropTable.Length];

        float currentTotal = 0;
        
        for (int i=0; i<dropTable.Length; i++)
        {
            currentTotal = currentTotal + dropTable[i].chanceOfDrop;
            CDA[i] = currentTotal;
        }
    }

    public GameObject ObtainRandomObject ()
    {
        float randomNum = Random.Range(0, CDA[CDA.Length-1]);
        int selectedIndex = System.Array.BinarySearch(CDA, randomNum);
        if (selectedIndex < 0)
            selectedIndex = ~selectedIndex;
        return dropTable[selectedIndex].itemToDrop;
        
    }

    public void SpawnItemDrop(GameObject item)
    {
        GameObject droppedItem = Instantiate(item) as GameObject;
        droppedItem.transform.position = this.transform.position + dropOffset;
    }
}
