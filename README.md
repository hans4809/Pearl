# 2024 넥슨 하계 게임잼 재밌넥 팀 프로젝트 👥
<img width="700" src="https://github.com/user-attachments/assets/011a1e6d-f814-4586-a700-9cf226ff86f5">

# PealRush
![시작화면](https://github.com/user-attachments/assets/763f880f-50f7-441f-a4b4-46745e078c2e)



### 게임개요 🔎
<details>
<summary>접기/펼치기 버튼</summary> 
<div markdown="1">

#### 시스템 테마
#### 게임 설명 및 아이템 설명
![게임설명](https://github.com/user-attachments/assets/9c84453c-ce95-4f0c-8546-e80733d357b4)
</div>
</details>

## 개발 기간 📅
#### 2024.07.12~ 2024.07.14

## 역할 분담 🧑‍💻
### 개발 인원 : 5명 [재밌넥 게임잼 팀프로젝트]
| 이름 | 개인 역할 | 담당 역할 및 기능 |
| ------ | ---------- | ------ |
| 박민지 | 기획 | 기획자 |
| 박예진 | 아트 | 디자이너 |
| 현다헌 | 아트 | 디자이너 |
| 김종엽 | Developer | 개발 |
| 한효빈 | Developer | 개발 |


## 시연영상 
#### ⬇ Link Here ⬇
https://youtu.be/8ucHX0Cfe6w
 
## 기술 스택 💻
<img src="https://img.shields.io/badge/Unity-FFFFFF?style=for-the-badge&logo=Unity&logoColor=black">
<img src="https://img.shields.io/badge/csharp-512BD4?style=for-the-badge&logo=csharp&logoColor=white">

## 구현 내용
### 물리 계산식을 활용한 포물선 운동(에어본) 구현
```{cpp}
    public void JumpForce()
    {
        if(ReturnToIdleCoroutine != null)
        {
            StopCoroutine(ReturnToIdleCoroutine);
            ReturnToIdleCoroutine = null;
        }

        Rb2D.gravityScale = 1.0f;
        // m*k*g*h = m*v^2/2 (단, k == gravityScale) <= 역학적 에너지 보존 법칙 적용
        float v_y = Mathf.Sqrt(2 * Rb2D.gravityScale * -Physics2D.gravity.y * MaxHeightDisplacement.y);
        // 포물선 운동 법칙 적용
        float v_x = MaxHeightDisplacement.x * v_y / (2 * MaxHeightDisplacement.y)/*Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length*/;

        Vector2 force = Rb2D.mass * (new Vector2(v_x, v_y) - Rb2D.velocity);
        Rb2D.AddForce(force, ForceMode2D.Impulse);

        float timer = (4 * MaxHeightDisplacement.y) / v_y;
        ReturnToIdleCoroutine = StartCoroutine(RetrunToIdleCor(timer));
    }
```
### AsyncOperation을 활용한 비동기 씬 로드
```{cpp}
 public IEnumerator LoadSceneAsync<T>(Define.Scene type, bool isMultiplay = false) where T : UI_Scene
 {
     Managers.Clear();
 
     LoadingScene = Managers.UI.ShowSceneUI<T>();
     Managers.Scene.CurrentScene.SceneUI = LoadingScene;
     AsyncLoadSceneOper = SceneManager.LoadSceneAsync(GetSceneName(type));
     AsyncLoadSceneOper.allowSceneActivation = false; // Scene 로드 끝나도 화면 활성화 안 함
 
     while(!AsyncLoadSceneOper.isDone)
     {
         if(LoadingScene is UI_ExplainScene)
         {
             if ((LoadingScene as UI_ExplainScene).StartButton != null)
             {
                 (LoadingScene as UI_ExplainScene).StartButton.interactable = AsyncLoadSceneOper.progress >= 0.9f;
                 (LoadingScene as UI_ExplainScene).StartButton.onClick.AddListener(() =>
                 {
                     Managers.Sound.Play("Sounds/SFX/UI_Button");
                     if (AsyncLoadSceneOper.isDone || AsyncLoadSceneOper.progress >= 0.9f)
                     {
                         AsyncLoadSceneOper.allowSceneActivation = true;
                     }
                 });
             }
 
         }
 
         if(LoadingScene is UI_GameOverScene)
         {
             Sprite winnerSprite = Managers.Resource.Load<Sprite>($"Sprites/UI/Player{Managers.Game.BestPlayerIndex}Winner");
             (LoadingScene as UI_GameOverScene).WinnerIMG.sprite = winnerSprite;
         }
 
         if (AsyncLoadSceneOper.progress >= 0.9f)
             break;
 
         yield return null;
     }
     
     if (LoadingScene is UI_ExplainScene)
     {
         if((LoadingScene as UI_ExplainScene).StartButton != null)
             (LoadingScene as UI_ExplainScene).StartButton.interactable = AsyncLoadSceneOper.progress >= 0.9f;
     }
 }
```
### Get/Set Property와 Delegate를 활용한 UI 효과 구현
```{cpp}
   public int StartTimer 
   { 
       get => _startTimer;
       set
       {
           if(value > 0)
           {
               _startTimer = value;
               (SceneUI as UI_GameScene).StartTimerText.text = $"{_startTimer}";
               StartTextDelegate.Invoke((SceneUI as UI_GameScene).StartTimerText);
               //StartCoroutine((SceneUI as UI_GameScene).StartTimerEffect_FadeIn((SceneUI as UI_GameScene).StartTimerText));
           }
           else
           {
               _startTimer = 0;
               (SceneUI as UI_GameScene).StartTimerText.gameObject.SetActive(false);
               Managers.Game.GameStart();
           }
  
       }
   }
```
