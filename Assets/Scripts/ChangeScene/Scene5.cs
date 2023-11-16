using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene5 : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ToScene1());
    }
    private IEnumerator ToScene1()
    {
        yield return new WaitForSeconds(44f);
        MemoryItemManage.Candy1Mission = false;
        MemoryItemManage.Candy3Mission = false;
        MemoryItemManage.HamburgerCandyMission = false;
        MemoryItemManage.HariMission = false;
        MemoryItemManage.InvitationMission = false;
        MemoryItemManage.OldLetterMission = false;
        MemoryItemManage.PeaCandyMission = false;
        MemoryItemManage.TeddyMission = false;
        MemoryItemManage.TakeItemState = 0;
        CharacterAbility.SP = 1;
        Character.IsDialoged = 0;
        GameCompleteTrigger.AllClear = false;
        SceneManager.LoadScene(0);
    }
}
