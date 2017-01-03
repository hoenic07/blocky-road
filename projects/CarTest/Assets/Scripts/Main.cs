using Assets.Scripts;
using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    #region Set from Unity
    public GameObject carPrefab;
    public Editable currentEditing;
    #endregion

    #region Members
    public List<BasicStreetBlock> streetBlocks;
    public List<GameObject> staticBlocks;
    private Level level;
    private GameObject editPanel;
    private GameObject finishedMessage;
    #endregion

    #region Game State
    private bool isEditing = true;
    public bool isRemovingBlock = false;
    #endregion

    #region Parameters
    public static LevelMeta levelMeta;
    #endregion

    // Use this for initialization
    void Start () {
        streetBlocks = new List<BasicStreetBlock>();
        InitLevel(levelMeta);
        SetUpListeners();
        SetStartResetText();
        editPanel = GameObject.Find("EditPanel");
        finishedMessage = GameObject.Find("FinishedPanel");
        finishedMessage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void InitLevel(LevelMeta levelMeta)
    {
        Debug.Log("Load level " + levelMeta.id);
        var lvl = LevelLoader.GetLevel(levelMeta);
        level = lvl;
        foreach (var block in lvl.staticBlocks)
        {
            var prefab = StreetBlockFactory.LoadPrefabByType(block.type);
            var go = Instantiate(prefab, new Vector3(block.x, block.y), Quaternion.identity);

            var ed = go.GetComponentInChildren<Editable>();
            if(ed) ed.isEditable = false;
            if (block.isStart)
            {
                var car = Instantiate(carPrefab, new Vector3(block.x, block.y + 1), Quaternion.identity);
                car.transform.Rotate(0, 270, 0);
            }
            streetBlocks.Add(StreetBlockFactory.CreateStreetBlock(block.type, go));
            staticBlocks.Add(go);
        }

        var parent = GameObject.Find("AvailableBlocks");
        int i = 0;
        foreach (var block in lvl.editorBlocks)
        {
            var go = StreetBlockFactory.LoadEditBlockByType(block);
            go.transform.position = parent.transform.position + new Vector3(0, i * 110);
            go.transform.SetParent(parent.transform, true);
            i++;
        }
    }

    private void SetUpListeners()
    {
        var srBt = GameObject.Find("StartReset").GetComponent<Button>();
        srBt.onClick.AddListener(() =>
        {
            if (isEditing) StartRun();
            else Reset();
        });
        var rmBt = GameObject.Find("RemoveBlock").GetComponent<Button>();

        rmBt.onClick.AddListener(() =>
        {
            if (isRemovingBlock) CancelRemoving();
            else StartRemoving();
        });
    }

    #region Remove Block

    public void StartRemoving()
    {
        isRemovingBlock = true;
        GameObject.Find("RemoveBlock").GetComponentInChildren<Text>().text = "Cancel Removing";
    }

    public void CancelRemoving()
    {
        GameObject.Find("RemoveBlock").GetComponentInChildren<Text>().text = "Remove Block";
        isRemovingBlock = false;
    }

    public void RemoveObject(BasicStreetBlock block)
    {
        streetBlocks.Remove(block);
        DestroyObject(block.GameObject);
        level.editorBlocks.First(d => d.type == block.Type).ChangeCount(1);
        CancelRemoving();
    }

    #endregion

    #region Start / Reset

    public void StartRun()
    {
        isEditing = false;
        SetStartResetText();
        var c = FindObjectOfType<Car>();
        c.ApplyStartForce();
        editPanel.SetActive(false);
    }

    public void Reset()
    {
        isEditing = true;
        SetStartResetText();
        var c = FindObjectOfType<Car>();
        DestroyObject(c.gameObject);
        var startBlock = level.staticBlocks.First(s => s.isStart);
        var go = Instantiate(carPrefab, new Vector3(startBlock.x, startBlock.y + 1), Quaternion.identity);
        go.transform.Rotate(0, 270, 0);
        editPanel.SetActive(true);
    }

    #endregion

    private void SetStartResetText()
    {
        var bt = GameObject.Find("StartReset").GetComponentInChildren<Text>();
        bt.text = isEditing ? "Start" : "Reset";
    }

    public void EditBlockSelected(GameObject go)
    {
        CancelRemoving();
        foreach (var item in level.editorBlocks)
        {
            Debug.Log(item.gameObject);
        }
        Debug.Log(go);
        var eb = level.editorBlocks.FirstOrDefault(e => e.gameObject == go);
        if (eb != null && eb.count > 0)
        {
            eb.ChangeCount(-1);
            Debug.Log("Create!");
            if (currentEditing != null) currentEditing.isEditing = false;

            var eo = StreetBlockFactory.CreateStreetBlock(eb.type);
            var ed = eo.GameObject.GetComponent<Editable>();
            ed.isEditing = true;
            ed.creator = this;
            currentEditing = ed;
            ed.editorObject = eo;
            streetBlocks.Add(eo);
        }
        else
        {
            //TODO: Play error sound?
        }
    }

    public void ShowFinished()
    {
        GameObject.Find("StartReset").SetActive(false);
        finishedMessage.SetActive(true);
    }
}

