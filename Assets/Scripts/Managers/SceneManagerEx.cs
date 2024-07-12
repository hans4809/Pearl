//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private bool _isMultiPlay = false;
    public bool IsMultiPlay { get => _isMultiPlay; set => _isMultiPlay = value; }
    private AsyncOperation _asyncLoadSceneOper;
    public AsyncOperation AsyncLoadSceneOper { get => _asyncLoadSceneOper; private set => _asyncLoadSceneOper = value; }

    private UI_Scene _loadingScene;
    public UI_Scene LoadingScene { get => _loadingScene; private set => _loadingScene = value; }
    public BaseScene CurrentScene
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }
    public void LoadScene(Define.Scene type, bool isMultiplay = false)
    {
        Managers.Clear();
        Time.timeScale = 1;
        if(!isMultiplay)
            SceneManager.LoadScene(GetSceneName(type));
        //else
        //    PhotonNetwork.LoadLevel(GetSceneName(type));
    }
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();

        if(AsyncLoadSceneOper != null)
            AsyncLoadSceneOper = null;

        if (LoadingScene != null)
            LoadingScene = null;
    }

    public IEnumerator LoadSceneAsync<T>(Define.Scene type, bool isMultiplay = false) where T : UI_Scene
    {
        Managers.Clear();

        LoadingScene = Managers.UI.ShowSceneUI<T>();
        AsyncLoadSceneOper = SceneManager.LoadSceneAsync(GetSceneName(type));
        AsyncLoadSceneOper.allowSceneActivation = false; // Scene 로드 끝나도 화면 활성화 안 함

        while(!AsyncLoadSceneOper.isDone)
        {
            if((LoadingScene as UI_LoadingScene).LoadingBarIMG != null)
                (LoadingScene as UI_LoadingScene).LoadingBarIMG.fillAmount = AsyncLoadSceneOper.progress;

            if((LoadingScene) as UI_ExplainScene)
            {

            }

            if (AsyncLoadSceneOper.progress >= 0.9f)
                break;

            yield return null;
        }

        if ((LoadingScene as UI_LoadingScene).LoadingBarIMG != null)
            (LoadingScene as UI_LoadingScene).LoadingBarIMG.fillAmount = 1;
    }
}
