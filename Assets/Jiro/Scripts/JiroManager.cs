using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Jiro
{
    public class JiroManager : MonoBehaviour
    {
        #region �v���p�e�B
        public static JiroManager Instance;

        void Awake()
        {
            if(Instance)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }
        #endregion

        /// <summary>�I�[�f�B�I�\�[�X�R���|�[�l���g</summary>
        [SerializeField] AudioSource _audioSource;

        /// <summary>�f�[�^�̃X�N���v�^�u���I�u�W�F�N�g����</summary>
        [SerializeField] OrderDeta[] _orderDetas;

        /// <summary>���q����̔ԍ��S�l��</summary>
        int[] _customers = new int[4];

        /// <summary>���݂̂��q����̔ԍ�</summary>
        int _currentCustomer = 0;

        /// <summary>�Q�[���̃t�F�C�Y</summary>
        GamePhase _gamePhase = GamePhase.GameReady;

        /// <summary>�R�[���̃N���b�v���A���؁A��</summary>
        [SerializeField] AudioClip[] _callClips;

        JiroValue _currentNinniku = JiroValue.Default;
        JiroValue _currentYasai = JiroValue.Default;
        JiroValue _currentAbura = JiroValue.Default;
        JiroValue _currentKarame = JiroValue.Default;

        bool _currentSelect;

        /// <summary>
        /// �Q�[���X�^�[�g�{�^��
        /// </summary>
        public void GameStart()
        {
            Debug.Log("GameStart");
            RandomSelectCustmers();
            //�A�j���[�V��������
            //���A�j���[�V�����C�x���g������
            Call();
        }

        /// <summary>
        /// ���q����������_���Ɍ��߂�
        /// </summary>
        void RandomSelectCustmers()
        {
            Debug.Log("RandomSelect");
            for (int i = 0; i < _customers.Length; i++)
            {
                _customers[i] = UnityEngine.Random.Range(0, _orderDetas.Length);
            }
        }

        public void Call()
        {
            Debug.Log("Call");
            Debug.Log($"{_orderDetas[_customers[_currentCustomer]].Ninniku},{_orderDetas[_customers[_currentCustomer]].Yasai}," +
                $"{_orderDetas[_customers[_currentCustomer]].Abura},{_orderDetas[_customers[_currentCustomer]].Karame}");
            StartCoroutine(CallAsync());
        }

        IEnumerator CallAsync()
        {
            Debug.Log("StartAsync");
            switch (_orderDetas[_customers[_currentCustomer]].JiroType)
            {
                case JiroType.Syou:
                    _audioSource.PlayOneShot(_callClips[0]);
                    break;
                case JiroType.SyouButa:
                    _audioSource.PlayOneShot(_callClips[1]);
                    break;
                case JiroType.Dai:
                    _audioSource.PlayOneShot(_callClips[2]);
                    break;
            }
            yield return new WaitForSeconds(1.5f);
            _audioSource.PlayOneShot(_orderDetas[_customers[_currentCustomer]].Call);
            _gamePhase = GamePhase.InGame;
        }

        public void LastSelect()
        {
            Debug.Log("LastSelect");
            if (Judge())
            {
                Debug.Log("�����ł�");
            }
            else
            {
                Debug.Log("���s�ł�");
            }
            _gamePhase = GamePhase.GameReady;
            //�A�j���[�^�[����
            //���A�j���[�V�����C�x���g������
            NextCustmor();
            Call();
        }

        bool Judge()
        {
            Debug.Log("Judge");
            if (_orderDetas[_customers[_currentCustomer]].Ninniku != _currentNinniku)
                return false;
            if (_orderDetas[_customers[_currentCustomer]].Yasai != _currentYasai)
                return false;
            if (_orderDetas[_customers[_currentCustomer]].Abura != _currentAbura)
                return false;
            if (_orderDetas[_customers[_currentCustomer]].Karame != _currentKarame)
                return false;
            return true;
        }

        public void Open(GameObject go)
        {
            Debug.Log("Open");
            if (_currentSelect || _gamePhase == GamePhase.GameReady)
                return;
            go.SetActive(true);
            _currentSelect = true;
        }

        public void Close(GameObject go)
        {
            Debug.Log("Close");
            go.SetActive(false);
            _currentSelect = false;
        }

        public void NinnikuChanege(int jiroValue)
        {
            Debug.Log("NinnikuChange");
            _currentNinniku = (JiroValue)jiroValue;
        }

        public void YasaiChanege(int jiroValue)
        {
            Debug.Log("YasaiChange");
            _currentYasai = (JiroValue)jiroValue;
        }

        public void AburaChange(int jiroValue)
        {
            Debug.Log("AburaChange");
            _currentAbura = (JiroValue)jiroValue;
        }

        public void KarameChange(int jiroValue)
        {
            Debug.Log("KarameChange");
            _currentKarame = (JiroValue)jiroValue;
        }

        void NextCustmor()
        {
            Debug.Log("NextCustmor");
            if (_currentCustomer == 3)
            {
                GameFinish();
            }
            _currentCustomer++;
        }

        void GameFinish()
        {
            Debug.Log("��Y�I��");
        }

        enum GamePhase
        {
            GameReady,
            InGame,
        }

        /// <summary>
        /// �g�b�s���O�̗�
        /// </summary>
        public enum JiroValue
        {
            Small,
            Default,
            Mashi,
        }

        /// <summary>
        /// ��Y�̎��
        /// </summary>
        public enum JiroType
        {
            Syou,
            SyouButa,
            Dai
        }
    }
}
