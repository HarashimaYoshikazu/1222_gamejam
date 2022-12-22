using UnityEngine;

namespace Jiro
{
    [CreateAssetMenu(fileName = "OrdersData", menuName = "ScriptableObjects/OrdersData", order = 1)]
    public class OrderDeta : ScriptableObject
    {
        [SerializeField] JiroManager.JiroType _jiroType;
        [SerializeField] JiroManager.JiroValue _ninniku;
        [SerializeField] JiroManager.JiroValue _yasai;
        [SerializeField] JiroManager.JiroValue _abura;
        [SerializeField] JiroManager.JiroValue _karame;
        [SerializeField] AudioClip _call;

        public JiroManager.JiroType JiroType => _jiroType;
        public JiroManager.JiroValue Ninniku => _ninniku;
        public JiroManager.JiroValue Yasai => _yasai;
        public JiroManager.JiroValue Abura => _abura;

        public JiroManager.JiroValue Karame => _karame;
        public AudioClip Call => _call;
    }
}
