namespace UnityEngine.UI.Extensions.Examples
{
    public class Example01ScrollViewCell : FancyScrollViewCell<Example01CellDto>
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        Text message;
        [SerializeField] private GameObject locker;
        private GAME_CONTROLLER GAME_CONTROLLER;
        private int index_buffer;

        readonly int scrollTriggerHash = Animator.StringToHash("scroll");

        void Start()
        {
            GAME_CONTROLLER = GameObject.FindGameObjectWithTag("GAME_CONTROLLER").GetComponent<GAME_CONTROLLER>();

            if (SAVE_GAME_HOLDER.index_of_opened_level >= index_buffer || index_buffer == 3)
            {
                locker.SetActive(false);
            }
            else
            {
                locker.SetActive(true);
            }

            var rectTransform = transform as RectTransform;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchoredPosition3D = Vector3.zero;
            UpdatePosition(0);
        }

        public void LOAD()
        {
            if (SAVE_GAME_HOLDER.index_of_opened_level >= index_buffer || index_buffer == 3)
                GAME_CONTROLLER.LOAD_LEVEL(index_buffer);
            else
            {
                GAME_CONTROLLER.LOAD_FIRST_SCENE();
            }
        }

        /// <summary>
        /// セルの内容を更新します
        /// </summary>
        /// <param name="itemData"></param>
        public override void UpdateContent(Example01CellDto itemData)
        {
            index_buffer = itemData.index;
            message.text = itemData.Message;
        }

        /// <summary>
        /// セルの位置を更新します
        /// </summary>
        /// <param name="position"></param>
        public override void UpdatePosition(float position)
        {
            animator.Play(scrollTriggerHash, -1, position);
            animator.speed = 0;
        }
    }
}
