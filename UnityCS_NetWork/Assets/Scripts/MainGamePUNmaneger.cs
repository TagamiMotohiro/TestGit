using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class MainGamePUNmaneger : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float instanceDistance=2;
    public static float Player1score = 0;
    public static float Player2score = 0;
    [SerializeField]
    TextMeshProUGUI scoreText;
    GameObject player;
    GameObject OVRcamera;
    GameObject RightHand;
	// Start is called before the first frame update
	private void Awake()
	{
        //スコアが静的なので初期化
	}
	void Start()
    {
        Player1score = 0;
        Player2score = 0;
		PhotonNetwork.IsMessageQueueRunning = true;
		player = PhotonNetwork.Instantiate("VRPlayer", Vector3.zero, Quaternion.identity);
		OVRcamera = GameObject.Find("OVRCameraRig");
		RightHand = GameObject.Find("RighthandPrefub");
		//scriptにプレイヤーの移動スクリプトを取得
		var script = OVRcamera.gameObject.GetComponent<VRPlayerWork>();
		var gun = RightHand.gameObject.GetComponent<PlayerGun>();
		//プレイヤーが動けるようになるフラグをonにする
		script.SetStert(true);
		gun.SetStart(true);
		//プレイヤーにカメラをセット
		OVRcamera.transform.position = player.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            scoreText.text = Player1score.ToString();
        }
        else
        {
            scoreText.text = Player2score.ToString();
        }
    }
    public void PlusScore(int plusScore)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Player1score += plusScore;
        }
        else
        {
            Player2score += plusScore;
        }
    }
}
