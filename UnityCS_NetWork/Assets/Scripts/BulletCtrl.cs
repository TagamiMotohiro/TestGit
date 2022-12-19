using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletCtrl : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject HitEffect;
    [SerializeField]
    float dulation;//�e�̐�������
    float time=0;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            //�����ꂽ�e�����g�̂��̂łȂ���΃��C���[��G�e�p�̂��̂ɕύX
            this.gameObject.layer = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= dulation)
        {
            //���g�̕��������͈��b���Ńq�b�g���Ȃ��Ă�����
            if (!photonView.IsMine) { return; }
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
	private void OnDestroy()
	{
		Instantiate(HitEffect,transform.position,Quaternion.identity);
        //���Ŏ��G�t�F�N�g
	}
	private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        //�_���[�W����͓I���Ȃ̂Œe���̏����͏�������
    }
}
