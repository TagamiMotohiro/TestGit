using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.LowLevel;
using static UnityEngine.GraphicsBuffer;
using UnityChan;

public class ShotTarget : Target
{
    [SerializeField]
    bool isStart = false;
    [SerializeField]
    float late = 10f;
    float coolTime;
    GameObject firePos;
    [SerializeField]
    List<GameObject> Player_List;
    GameObject LookPlayer;
    LineRenderer myLR;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            isStart = true;
        }
        myLR = GetComponent<LineRenderer>();
        Player_List = GameObject.FindGameObjectsWithTag("Player").ToList();
        //全プレイヤーを取得
        base.Start();
        firePos = transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStart) { return; }
        LookPlayer = closestPlayer();
        //関数で一番近いプレイヤーを算出
        LookAtTransformUp();
        //一番近いプレイヤーのほうを向く(砲身が)
        if (coolTime > late)
        {
            coolTime = 0;
            if (PhotonNetwork.LocalPlayer.IsMasterClient == false) { return; }
            GameObject g = PhotonNetwork.Instantiate("ChaseSphere", firePos.transform.position, Quaternion.identity);
            //クールタイムが終わったら弾を生成
            g.GetComponent<MoveTarget>().SetTarget(LookPlayer);
            //その際弾に今向いているプレイヤーの情報を代入
        }
        coolTime += Time.deltaTime;
    }
	GameObject closestPlayer()//Player_Listの中から自身に一番近いプレイヤーを算出
    {
        GameObject clossest = null;
        float minDistance = float.MaxValue;
        foreach (GameObject g in Player_List)
        {
            float gPos = Mathf.Abs(transform.position.magnitude - g.transform.position.magnitude);
            if (gPos < minDistance)
            {
                clossest = g;
                minDistance = gPos;
            }
        }
        return clossest;
    }
    void LookAtTransformUp()
    {
        this.transform.LookAt(LookPlayer.transform.position);
        this.transform.rotation = transform.rotation*Quaternion.AngleAxis(90,Vector3.right);
        myLR.SetPosition(0,firePos.transform.position);
        myLR.SetPosition(1, LookPlayer.transform.position + Vector3.down * 0.5f);
    }
}
