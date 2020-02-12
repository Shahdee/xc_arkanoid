using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// controller 

public class GUIMan : UIObject
{
    public Canvas m_Canvas;
    public CanvasScaler m_Scaler;
    public List<WinViewBase> m_Windows;

    // static float m_Scale;

    // public static float GetScale(){
    //     return m_Scale;
    // }

    public void Init(){

        EventMan.AddGameStateChangeListener(GameStateChange);

        InitWindows();

        // Debug.Log("Screen " + Screen.width + " . " + Screen.height);
        // Debug.Log("RTrs " + m_RectTransform.sizeDelta.x + " / " + m_RectTransform.sizeDelta.y);
        // Debug.Log("scale " + m_RectTransform.localScale);

        // m_Scale = m_RectTransform.localScale.x;

        OpenWindow(WinViewBase.WinType.Menu);
    }

    void InitWindows(){
        for (int i=0; i<m_Windows.Count; i++){
            m_Windows[i].Init();
        }
    }

    public override void UpdateMe(float deltaTime){
        for (int i=0; i<m_Windows.Count; i++){
            m_Windows[i].UpdateMe(deltaTime);
        }
    }

    void OpenWindow(WinViewBase.WinType wType){

        // Debug.Log("OpenWindow " + wType);

        for (int i=0; i<m_Windows.Count; i++){
            if (m_Windows[i].m_WindowType == wType)
                m_Windows[i].Open();
            else
                m_Windows[i].Close();
        }
    }

    void GameStateChange(GameMan.GameStates state){

        Debug.Log("GameStateChange " + state);

        switch(state){
            case GameMan.GameStates.Menu:
                OpenWindow(WinViewBase.WinType.Menu);
            break;

            case GameMan.GameStates.Play:
                OpenWindow(WinViewBase.WinType.Gameplay);
            break;

            case GameMan.GameStates.Over:
                OpenWindow(WinViewBase.WinType.Over);
            break;
        }
    }
}
