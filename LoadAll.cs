using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadAll : MonoBehaviour {

    public Room[] rooms;

	void Start () {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].items.Clear();
            rooms[i].nodes.Clear();
        }

        // 0_Beach0
        rooms[0].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/0_Beach1/apple.asset", typeof(Item)));
        rooms[0].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/0_Beach1/plank.asset", typeof(Item)));
        rooms[0].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/0_Beach1/plankNode.asset", typeof(Node)));

        // 1_Beach1
        rooms[1].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/1_Beach2/coconut.asset", typeof(Item)));

        // 2_JunglePassage1
        rooms[2].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/2_JunglePassage1/vines.asset", typeof(Node)));
        rooms[2].nodes[0].activated = false;

        // 3_T1Outside
        rooms[3].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/3_T1Outside/banana.asset", typeof(Item)));
        rooms[3].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/3_T1Outside/well.asset", typeof(Node)));

        // 4_T1Inside
        rooms[4].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/4_T1Inside/vines.asset", typeof(Node)));
        rooms[4].nodes[0].activated = false;
        if (rooms[4].exits.Count == 3)
            rooms[4].exits.RemoveAt(2);

        // 5_T1Hallway
        rooms[5].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/5_T1Hallway/libraryDoor.asset", typeof(Node)));
        if (rooms[5].exits.Count == 3)
            rooms[5].exits.RemoveAt(2);

        // 6_T1Library
        rooms[6].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/6_T1Library/pendant.asset", typeof(Item)));
        rooms[6].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/6_T1Library/bookcase.asset", typeof(Node)));

        // 7_T1Puzzle
        rooms[7].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/7_T1Puzzle/libraryKey.asset", typeof(Item)));
        rooms[7].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/7_T1Puzzle/puzzleDoor.asset", typeof(Node)));
        rooms[7].nodes[0].activated = false;
        if (rooms[7].exits.Count > 1)
            rooms[7].exits.RemoveAt(1);

        // 8_T1PoolArea
        rooms[8].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/8_T1PoolArea/pool.asset", typeof(Node)));

        // 9_T1Pool
        rooms[9].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/9_T1Pool/module.asset", typeof(Node)));
        rooms[9].nodes[0].activated = false;
        if (rooms[9].exits.Count > 1)
            rooms[9].exits.RemoveAt(1);

        // 10_T1Cellar
        rooms[10].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/10_T1Cellar/apple.asset", typeof(Item)));
        rooms[10].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/10_T1Cellar/door.asset", typeof(Node)));
        rooms[10].nodes[0].activated = false;
        if (rooms[10].exits.Count > 1)
            rooms[10].exits.RemoveAt(1);

        // 11_T1Final
        rooms[11].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/11_T1Final/strangeObject1.asset", typeof(Item)));

        // 12_Jungle
        rooms[12].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/12_Jungle/banana.asset", typeof(Item)));
        rooms[12].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/12_Jungle/coconut.asset", typeof(Item)));

        // 14_Device
        rooms[14].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/14_Device/banana.asset", typeof(Item)));
        rooms[14].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/14_Device/machete.asset", typeof(Item)));
        rooms[14].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/14_Device/strangeDevice.asset", typeof(Node)));
        rooms[14].nodes[0].activated = false;
        if (rooms[14].exits.Count > 1)
            rooms[14].exits.RemoveAt(1);

        // 15_DriedRiver
        rooms[15].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/15_DriedRiver/bridge.asset", typeof(Node)));
        rooms[15].nodes[0].activated = false;
        if (rooms[15].exits.Count > 1)
            rooms[15].exits.RemoveAt(1);

        // 16_T2Outside
        rooms[16].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/16_T2Outside/door.asset", typeof(Node)));
        rooms[16].nodes[0].activated = false;
        if (rooms[16].exits.Count > 1)
            rooms[16].exits.RemoveAt(1);

        // 17_T2Waterwheel
        rooms[17].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/17_T2Waterwheel/waterwheel.asset", typeof(Node)));
        rooms[17].nodes[0].activated = false;
        if (rooms[17].exits.Count > 1)
            rooms[17].exits.RemoveAt(1);

        // 18_T2Puzzle
        rooms[18].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/18_T2Puzzle/tabletEast.asset", typeof(Node)));
        rooms[18].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/18_T2Puzzle/tabletSouth.asset", typeof(Node)));
        rooms[18].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/18_T2Puzzle/tabletNorth.asset", typeof(Node)));
        rooms[18].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/18_T2Puzzle/tabletWest.asset", typeof(Node)));
        rooms[18].nodes[0].activated = false;
        rooms[18].nodes[1].activated = false;
        rooms[18].nodes[2].activated = false;
        rooms[18].nodes[3].activated = false;
        if (rooms[18].exits.Count > 2)
            rooms[18].exits.RemoveAt(2);

        // 19_T2Storeroom
        rooms[19].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/19_T2Storeroom/key.asset", typeof(Item)));
        rooms[19].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/19_T2Storeroom/coconut.asset", typeof(Item)));

        // 20_T2Crypt
        rooms[20].nodes.Add((Node)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/20_T2Crypt/door.asset", typeof(Node)));
        rooms[20].nodes[0].activated = false;
        if (rooms[20].exits.Count > 1)
            rooms[20].exits.RemoveAt(1);

        // 21_T2Final
        rooms[21].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/21_T2Final/strangeObject2.asset", typeof(Item)));
        
        // 22_Sea
        rooms[23].items.Add((Item)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Scripts/Rooms/22_Sea/strangeObject3.asset", typeof(Item)));

    }
}
