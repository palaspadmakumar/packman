using UnityEngine;
using UnityEngine.UI;

namespace com.hellion.packman
{
    public abstract class AbstractCharecter : MonoBehaviour
    {
        [SerializeField] protected Sprite look_right;
        [SerializeField] protected Sprite look_left;
        [SerializeField] protected Sprite look_up;
        [SerializeField] protected Sprite look_down;

        [SerializeField] protected Image _eye;

        protected void MakeCharecterLookRight()
        {
            _eye.sprite = look_right;
        }
        protected void MakeCharecterLookLeft()
        {
            _eye.sprite = look_left;
        }
        protected void MakeCharecterLookUp()
        {
            _eye.sprite = look_up;
        }
        protected void MakeCharecterLookDown()
        {
            _eye.sprite = look_down;
        }
    }
}
