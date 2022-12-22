using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

        JiroValue _currentNinniku = JiroValue.�ӂ�;
        JiroValue _currentYasai = JiroValue.�ӂ�;
        JiroValue _currentAbura = JiroValue.�ӂ�;
        JiroValue _currentKarame = JiroValue.�ӂ�;

        bool _currentSelect;

        [SerializeField] GameObject _startPanel;

        [SerializeField] Text _ninnikuText;
        [SerializeField] Text _yasaiText;
        [SerializeField] Text _aburaText;
        [SerializeField] Text _karameText;
        [SerializeField] Animator _animator;
        [SerializeField] GameObject[] _hudas;
        [SerializeField] GameObject[] _hudaPos;

        [SerializeField] Image[] _customerImages;

        [SerializeField] AudioClip _finishClip;

        [SerializeField] AudioClip _collectClip;
        [SerializeField] AudioClip _failedClip;
        int _currentScore;

        void Update()
        {
            _ninnikuText.text = "�ɂ�ɂ�:"+_currentNinniku.ToString();
            _yasaiText.text = "�₳��:" + _currentYasai.ToString();
            _aburaText.text = "���Ԃ�:" + _currentAbura.ToString();
            _karameText.text = "�����:" + _currentKarame.ToString();
        }

        /// <summary>
        /// �Q�[���X�^�[�g�{�^��
        /// </summary>
        public void GameStart()
        {
            ResetValue();
            _startPanel.SetActive(false);
            RandomSelectCustmers();
            _animator.Play("Coming");
            //���A�j���[�V�����C�x���g�ł��
            //Call();
        }

        /// <summary>
        /// ���q����������_���Ɍ��߂�
        /// </summary>
        void RandomSelectCustmers()
        {
            for (int i = 0; i < _customers.Length; i++)
            {
                _customers[i] = UnityEngine.Random.Range(0, _orderDetas.Length);
                Instantiate(_hudas[(int)_orderDetas[_customers[i]].JiroType], _hudaPos[i].transform);
            }
        }

        public void Call()
        {
            Debug.Log($"{_orderDetas[_customers[_currentCustomer]].Ninniku},{_orderDetas[_customers[_currentCustomer]].Yasai}," +
                $"{_orderDetas[_customers[_currentCustomer]].Abura},{_orderDetas[_customers[_currentCustomer]].Karame}");
            StartCoroutine(CallAsync());
        }

        IEnumerator CallAsync()
        {
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
            yield return new WaitForSeconds(3f);
            _audioSource.pitch = UnityEngine.Random.Range(0.8f, 2.0f);
            _audioSource.PlayOneShot(_orderDetas[_customers[_currentCustomer]].Call);
            _gamePhase = GamePhase.InGame;
        }

        public void LastSelect()
        {
            _audioSource.pitch = 1;
            if (Judge())
            {
                //�����ڕς���
                Debug.Log("�����ł�");
                _customerImages[_currentCustomer].color = Color.red;
                _audioSource.PlayOneShot(_collectClip);
                _currentScore += 150;
            }
            else
            {
                _customerImages[_currentCustomer].color = Color.blue;
                _audioSource.PlayOneShot(_failedClip);
                Debug.Log("���s�ł�");
            }
            _gamePhase = GamePhase.GameReady;
            StartCoroutine(Stay());
        }

        IEnumerator Stay()
        {
            yield return new WaitForSeconds(1);
            NextCustmor();
            if (_currentCustomer == 4)
            {
                GameFinish();
            }
            if(_gamePhase != GamePhase.GameEnd)
            {
                Call();
            }
        }

        bool Judge()
        {
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
            if (_currentSelect || _gamePhase == GamePhase.GameReady)
                return;
            go.SetActive(true);
            _currentSelect = true;
        }

        public void Close(GameObject go)
        {
            go.SetActive(false);
            _currentSelect = false;
        }

        public void NinnikuChanege(int jiroValue)
        {
            _currentNinniku = (JiroValue)jiroValue;
        }

        public void YasaiChanege(int jiroValue)
        {
            _currentYasai = (JiroValue)jiroValue;
        }

        public void AburaChange(int jiroValue)
        {
            _currentAbura = (JiroValue)jiroValue;
        }

        public void KarameChange(int jiroValue)
        {
            _currentKarame = (JiroValue)jiroValue;
        }

        void NextCustmor()
        {
            ResetValue();
            _currentCustomer++;
        }

        void GameFinish()
        {
            Instance = null;
            _audioSource.PlayOneShot(_finishClip);
            _gamePhase = GamePhase.GameEnd;
            ApplicationManager.Instance.AddScore(_currentScore);
            ApplicationManager.Instance.RandomSceneChange();
            Debug.Log("��Y�I��");
        }

        void ResetValue()
        {
            _currentNinniku = JiroValue.�ӂ�;
            _currentYasai = JiroValue.�ӂ�;
            _currentAbura = JiroValue.�ӂ�;
            _currentKarame = JiroValue.�ӂ�;
        }

        enum GamePhase
        {
            GameReady,
            InGame,
            GameEnd
        }

        /// <summary>
        /// �g�b�s���O�̗�
        /// </summary>
        public enum JiroValue
        {
            �����Ȃ�,
            �ӂ�,
            �܂�,
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
