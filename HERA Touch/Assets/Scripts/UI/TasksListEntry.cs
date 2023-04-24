using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HERATouch
{
    public class TasksListEntry : MonoBehaviour, IPointerExitHandler
    {
        public int taskIndex;

        public Slider progressBar;
        public Image progressBarFill;

        public Task task;

        public GameObject taskDescription;

        private void Update()
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("[Anim] Tasks List Entry Right"))
            {
                GetComponent<Animator>().SetBool("toLeft", false);
                GetComponent<Animator>().SetBool("toRight", false);
            }
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

            task = a_task;

            taskDescription.GetComponent<LocalizeStringEvent>().StringReference.SetReference("Tasks", task.taskData.tasksListEntryLocalRefKey);
        }


    }

}
