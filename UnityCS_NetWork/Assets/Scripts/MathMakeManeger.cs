using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class MathMakeManeger : MonoBehaviourPunCallbacks
{
	[SerializeField] TextMeshProUGUI MatchMakeText;
	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}
	public override void OnJoinedLobby()
	{
		MatchMakeText.text = "���r�[�ւ̎Q���ɐ������܂���";
		Debug.Log("���r�[�̎Q���ɐ���");
	}
	public override void OnConnectedToMaster()
	{
		Debug.Log("�N���C�A���g�̐ڑ��ɐ���");
		PhotonNetwork.JoinOrCreateRoom("Match",new RoomOptions(),TypedLobby.Default);
	}
	public override void OnJoinedRoom()
	{
		//PhotonNetwork.IsMessageQueueRunning = false;
		//�����֎Q�����邪�I�u�W�F�N�g�����͕ʂ̃V�[���ōs��
		MatchMakeText.text = "�����ւ̎Q���ɐ������܂���";
		Debug.Log("�����ւ̎Q���ɐ���");
	}
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if (PhotonNetwork.LocalPlayer.IsMasterClient)
		{
			MatchMakeText.text=("�}�b�`���O���������܂����I�I");
			Debug.Log("�}�b�`���C�L���O����");
			PhotonNetwork.LoadLevel("MainGame");
		}
	}
	public void StartConect()
	{
		MatchMakeText.text = "�}�b�`���O���Ă��܂�";
		Debug.Log("�ڑ����J�n");
		PhotonNetwork.ConnectUsingSettings();
	}
	// Update is called once per frame
}
