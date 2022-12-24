using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PUN_Maneger : MonoBehaviourPunCallbacks
{
	GameObject human;
	GameObject OVRcamera;
	GameObject RightHand;
	float score = 0;
	[SerializeField]
	TextMeshProUGUI scoreText;
	public override void OnConnectedToMaster()//PUN�ɐڑ����ꂽ�ۂ̏����i�R�[���o�b�N�j
	{
		// �������Ŏw�肵�����O�̃��[���ɓ����A�Ȃ������ꍇ�͍쐬���ē����B
		PhotonNetwork.JoinOrCreateRoom("Game Room", new RoomOptions(), TypedLobby.Default);	
	}
	public override void OnJoinedRoom()//���r�[���烋�[���ɐڑ����ꂽ���̏����i�R�[���o�b�N�j
	{
		//�v���C���[�ƂȂ�Q�[���I�u�W�F�N�g���쐬���A�ϐ����Ɏ擾�B
		human = PhotonNetwork.Instantiate("VRPlayer", Vector3.zero, Quaternion.identity);
		OVRcamera = GameObject.Find("OVRCameraRig");
		RightHand = GameObject.Find("RighthandPrefub");
		//script�Ƀv���C���[�̈ړ��X�N���v�g���擾
		var script = OVRcamera.gameObject.GetComponent<VRPlayerWork>();
		var gun = RightHand.gameObject.GetComponent<PlayerGun>();
		//�v���C���[��������悤�ɂȂ�t���O��on�ɂ���
		script.SetStert(true);
		gun.SetStart(true);
		//script.SetMessage(PhotonNetwork.NetworkClientState.ToString());
		//�v���C���[�ɃJ�������Z�b�g
		//Camera.main.GetComponent<HumanCameraScript>().target=human;
		OVRcamera.transform.position = human.transform.position;

	}
	// Start is called before the first frame update
	void Start()
    {
		//PUN�T�[�o�[�ɐڑ�����
		PhotonNetwork.ConnectUsingSettings();
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
