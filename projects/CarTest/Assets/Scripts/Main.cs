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
    private GameObject actionPanel;
    private GameObject finishedMessage;
    public static float globalScale = 0.006f;
    #endregion

    #region Game State
    private bool isEditing = true;
    public bool isRemovingBlock = false;
    private bool isFinished = false;
    #endregion

    #region Parameters
    public static LevelMeta levelMeta;
    #endregion

    // Use this for initialization
    void Start () {
        globalScale = 0.006f;
        Physics.gravity = new Vector3(0, -9.81f * globalScale, 0);
        streetBlocks = new List<BasicStreetBlock>();
        if (levelMeta == null)
        {
            levelMeta = LevelLoader.GetAllLevelMeta()[0];
        }
        InitLevel(levelMeta);
        SetUpListeners();
        SetStartResetText();
        editPanel = GameObject.Find("EditPanel");
        actionPanel = GameObject.Find("ActionPanel");
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
            go.transform.SetParent(gameObject.transform, false);
            go.transform.Rotate(0, 0, block.rotation);
            var ed = go.GetComponentInChildren<Editable>();
            if(ed) ed.isEditable = false;
            if (block.isStart)
            {
                var car = Instantiate(carPrefab, new Vector3(block.x, block.y + 0.41f), Quaternion.identity);
                car.transform.SetParent(gameObject.transform, false);
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
            go.transform.position = new Vector3(60, i * -100 - 65);
            go.transform.SetParent(parent.transform, false);
            i++;
        }
    }

    private void SetUpListeners()
    {
        var srBt = GameObject.Find("StartReset").GetComponent<Button>();
        srBt.onClick.AddListener(() =>
        {
            if (isFinished)
            {
                finishedMessage.SetActive(false);
                Reset();
                StartRun();
            }
            else if (isEditing) StartRun();
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
        actionPanel.SetActive(false);
    }

    public void Reset()
    {
        isEditing = true;
        SetStartResetText();
        var c = FindObjectOfType<Car>();
        DestroyObject(c.gameObject);
        var startBlock = level.staticBlocks.First(s => s.isStart);
        var go = Instantiate(carPrefab, new Vector3(startBlock.x, startBlock.y + 0.41f), Quaternion.identity);
        go.transform.SetParent(gameObject.transform, false);
        go.transform.Rotate(0, 270, 0);
        editPanel.SetActive(true);
        actionPanel.SetActive(true);
    }

    #endregion

    private void SetStartResetText()
    {
        var bt = GameObject.Find("StartReset").GetComponentInChildren<Text>();
        if (isFinished)
        {
            bt.text = "Rerun";
        }
        else
        {
            bt.text = isEditing ? "Start" : "Reset";
        }
        
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
            eo.GameObject.transform.SetParent(gameObject.transform, false);
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
        isFinished = true;
        SetStartResetText();
        LevelLoader.SetAndSaveLevelDone(levelMeta);
        finishedMessage.SetActive(true);
    }
}

