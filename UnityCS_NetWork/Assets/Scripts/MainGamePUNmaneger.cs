using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class MainGamePUNmaneger : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float instanceDistance=2;
    float score = 0;
    [SerializeField]
    TextMeshProUGUI scoreText;
    GameObject player;
    GameObject OVRcamera;
    GameObject RightHand;
    // Start is called before the first frame update

    void Start()
    {
		PhotonNetwork.IsMessageQueueRunning = true;
		player = PhotonNetwork.Instantiate("VRPlayer", Vector3.zero, Quaternion.identity);
		OVRcamera = GameObject.Find("OVRCameraRig");
		RightHand = GameObject.Find("RighthandPrefub");
		//script�Ƀv���C���[�̈ړ��X�N���v�g���擾
		var script = OVRcamera.gameObject.GetComponent<VRPlayerWork>();
		var gun = RightHand.gameObject.GetComponent<PlayerGun>();
		//�v���C���[��������悤�ɂȂ�t���O��on�ɂ���
		script.SetStert(true);
		gun.SetStart(true);
		//�v���C���[�ɃJ�������Z�b�g
		OVRcamera.transform.position = player.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
    public void PlusScore(int plusScore)
    {
        score += plusScore;
    }
}
