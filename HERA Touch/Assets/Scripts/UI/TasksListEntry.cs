using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HERATouch
{
    public class TasksListEntry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int taskIndex;

        public TaskData taskData;
        public int patientId;

        public Slider progressBar;
        public Image progressBarFill;

        public Color startColor = new Color(138 / 255, 236 / 255, 204 / 255);
        public Color pauseColor = new Color(248 / 255, 160 / 255, 27 / 255);

        public Task task;

        private Vector2 _touchStartPosition;

        void Start()
        {
            // EventManager.instance.taskButtonEvents.clicked += ResetExpandable;
            InputManager.instance.swipeLeft += OnSwipeLeft;
            InputManager.instance.swipeRight += OnSwipeRight;
        }

        void OnDestroy()
        {
            InputManager.instance.swipeLeft -= OnSwipeLeft;
            InputManager.instance.swipeRight -= OnSwipeRight;
            // EventManager.instance.taskButtonEvents.clicked -= ResetExpandable;
        }

        private void Update()
        {
            // if (task.taskStatus == TaskStatus.OnGoing)
            // {
            //     progressBar.value = 1;
            //     progressBarFill.color = Color.green;
            // }
            // 
            // else
            // {
            //     progressBarFill.color = Color.yellow;
            // }
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("[Anim] Tasks List Entry Right"))
            {
                GetComponent<Animator>().SetBool("toLeft", false);
                GetComponent<Animator>().SetBool("toRight", false);
            }
        }

        void OnSwipeLeft(Vector2 startPostion)
        {
            // if (Vector2.Distance(startPostion, _touchStartPosition) <= 0.01f)
            // {
            //     GetComponent<Animator>().SetBool("toLeft", true);
            // }
        }

        void OnSwipeRight(Vector2 startPostion)
        {
            // GetComponent<Animator>().SetBool("toRight", true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _touchStartPosition = eventData.position;
        }

        public void TurnOnDeleteButton()
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Empty"))
            {
                GetComponent<Animator>().SetBool("toLeft", true);
                GetComponent<Animator>().SetBool("toRight", false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("[Anim] Tasks List Entry Left"))
            {
                GetComponent<Animator>().SetBool("toRight", true);
                GetComponent<Animator>().SetBool("toLeft", false);
            }
            
        }


        public void Initialize(int a_taskIndex, Task a_task)
        {
            taskIndex = a_taskIndex;
            taskData = a_task.taskData; // _taskData;
            patientId = a_task.patientId; //  _patientId;

            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", taskData.bubbleLocalRefKey);
        }


    }

}
