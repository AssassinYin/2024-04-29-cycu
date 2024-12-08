

###### 專題組員

資訊四乙 10924148 陳黃揚 資訊四甲 11027116 蘇新祐 資訊四甲 11027132 曾兆翌

###### 指導教授

莊啟宏 副教授

# 壹、 緒論

一、   摘要

針對2D平台戰鬥遊戲的設計與開發進行探討，運用Unity引擎實現遊戲中的角色控制、戰鬥機制及物理互動，並完成核心玩法的設計與實作。

二、   研究背景

2D平台戰鬥遊戲因其操作簡單且具有挑戰性的特點，深受玩家喜愛。隨著遊戲市場的發展，Unity成為遊戲開發的主要工具之一，特別適合小型和中型遊戲的開發。Unity提供的2D物理系統使得開發者能快速設計並實現平台遊戲所需的跳躍、碰撞、攻擊等功能。

三、   研究目的

本研究旨在運用Unity引擎開發一款具備基礎戰鬥系統的2D平台遊戲，藉此熟悉Unity的物件導向設計、2D物理系統和遊戲鏡頭控制。我們期望通過實作，深入學習遊戲開發過程中的角色控制、戰鬥邏輯、場景設計等技術，並嘗試實現具有沉浸感的鏡頭語言和細緻的物理細節，為玩家提供良好的操作體驗。

# 貳、 相關研究

一、   Unity套件介紹

1.  **Cinemachine**：專注於自動相機控制。允許輕鬆地設定跟隨、切換和控制相機，無需撰寫複雜的代碼。

2.  **Physics 2D**：Unity中的2D物理系統，提供各種2D物理效果如碰撞、重力和物理運動。支持碰撞偵測、剛體（Rigidbody 2D）、各類碰撞器（如Box Collider 2D、Circle Collider 2D等），並可以應用關節來製作物理連接。

3.  **2D Animation**：支持骨架綁定、網格分割和權重控制，使角色或物件可以通過骨骼控制動畫，也支持動畫混合和動畫狀態機以管理動畫狀態。

4.  **Input System**：提供一種現代化的輸入處理方法，支持多種如鍵盤、滑鼠、手柄和觸摸的控制方式。可以方便地設置複合輸入控制，並自定義按鍵配置，同時支持多人遊戲的多控制器設置。

5.  **Unity UI**：Unity的UI系統，用於創建遊戲介面，如按鈕、文本、滑動條和其他互動元素，並可通過Event System進行UI事件處理。

二、   外部套件介紹

1.  **LeanTween**：用於協助製作補間動畫(Tweening Animation)。

三、   遊戲設計模式[1][2]

1.  **遊戲迴圈模式 (Game Loop Pattern)**：將遊戲時間的進程與使用者輸入和處理器速度解耦(Decoupling)。

2.  **組件模式 (Component Pattern)**：一個實體跨越多個領域。為了讓這些領域保持隔離，將每個領域的程式碼放入其專屬的組件類別中。實體則簡化為一個包含組件的容器。

3.  **狀態模式 (State Pattern)**：允許物件在其內部狀態改變時改變其行為。該物件的表現將看起來像改變了它的類別。

4.  **觀察者模式 (Observer Pattern)**：定義物件之間的一對多依賴關係，使得當一個物件的狀態改變時，所有依賴於它的物件都會自動收到通知並更新。

5.  **單例模式 (Singleton Pattern)**：確保一個類別只有一個實例，並提供一個全域的存取點來使用該實例。

四、   相關遊戲案例

**《Hollow Knight》**：結合探索、平台跳躍和戰鬥元素的2D橫向卷軸遊戲，讓玩家在非線性關卡中探索並挑戰不同難度的敵人。

**《九日》**：同《Hollow Knight》結合探索、平台跳躍和戰鬥三項元素的2D橫向卷軸遊戲，但是關卡偏向線性。

**《Dead Cells》**：一款隨機生成關卡的Roguelike風格平台戰鬥遊戲，具備快速且流暢的動作戰鬥體驗。

**《Celeste》**：無戰鬥系統的平台跳躍遊戲，其出色的物理系統和精確的角色控制深受好評。遊戲中對跳躍、爬牆等動作有極高的精確度要求。

# 參、 研究方法

本系統以MVP架構方向進行思考[3]，將遊戲邏輯和視覺呈現分離，使各功能模組更易理解且可重複使用。系統核心分成輸入處理（Presenter）、遊戲邏輯（Model）和視覺呈現（View）三大部分。以下是各模組的詳細說明：

一、      玩家

**Presenter**：玩家輸入控制

Presenter中使用Unity Input System處理玩家的按鍵輸入，並將其以委派的方式傳遞到Model進行處理，以實現動作行為的觸發。

1.    **移動**：玩家按下左右方向鍵時，會將輸入值傳遞至Model的移動方法中。

2.    **跳躍**：玩家按下跳躍鍵時，通知Model進行跳躍行為，並根據按鍵時長決定跳躍高度。

3.    **攻擊**：攻擊按鍵被觸發時，通知Model進行攻擊動畫和傷害判定。

4.    **衝刺**：衝刺按鍵被觸發時，通知Model進行衝刺動畫和透過移動輸入值判定方向移動。

**Model**：遊戲邏輯和物理行為

Model負責處理玩家輸入後的遊戲邏輯，包括狀態機更新、物理行為和碰撞處理。

1.    **狀態機設計**：Model使用狀態機處理角色的各種狀態（如移動、跳躍、攻擊等）。每次輸入行為都會先檢查狀態機的當前狀態，決定該行為是否可執行。

2.    **狀態切換**：例如，當衝刺狀態結束時自動返回到閒置狀態。

3.    **2D物理行為**：使用Unity的2D物理引擎實現移動、跳躍和碰撞等行為

而我們具體實作了下列行為：

1.    **行走**：根據加速度、終端速度等參數控制左右移動的自然感，並在達到終端速度時保持穩定。

2.    **衝刺**：使用Coroutine，讓角色在衝刺時持續性地給予短暫的力來實現向前衝刺效果。

3.    **跳躍/二段跳**：給予角色向上的瞬間衝擊力，根據玩家按鍵時間調整跳躍高度。當放開按鍵時增加重力來縮短跳躍距離。[4]

4.    **攻擊**：角色在前方生成短暫的觸發器，觸發碰撞後對敵人造成傷害和擊退效果，同時為玩家施加反作用力。

5.    **受傷**：在角色與敵人或子彈碰撞後，進入短暫的無敵狀態，避免在同一楨重複受傷，並更新生命值或UI狀態。

**View**：視覺呈現

View負責呈現畫面效果，包括UI、動畫和鏡頭運動，確保玩家在操作時得到即時視覺反饋。

1.    **動畫系統 (Animator)**：Animator控制角色的移動、攻擊、跳躍和受傷等動畫，並通過Model傳遞的狀態來觸發轉場。根據Model中的角色狀態，利用Animator的CrossFade()方法播放對應動畫。

2.    **鏡頭控制 (Cinemachine & LeanTween)**：

**Cinemachine**：用於平滑的鏡頭移動和跟隨效果，保持玩家角色始終位於畫面視角內，並使鏡頭能提前顯示玩家移動方向的視野，這種平滑過渡有助於提升遊戲操作的流暢感並給予更多資訊。[5][8]

**LeanTween**：簡化鏡頭平滑移動的效果實作。

3.    **UI系統**：顯示玩家生命值、得分、技能冷卻等資訊，當角色受傷或狀態變化時，UI會更新顯示生命或無敵時間，給予即時性反饋。

二、      星星樹敵人

**Presenter**：使用隨機數來選擇行為。

Presenter基於玩家位置的距離決定敵人是否應轉變為走路或攻擊狀態。若距離超出範圍，則進入閒置狀態。而在攻擊狀態中，利用隨機亂數決定三種不同的攻擊方式。

1.    **遠程攻擊**：生成子彈物件朝玩家方向發射。

2.    **旋轉攻擊**：在敵人周圍生成短暫的攻擊觸發器，判定是否碰撞到玩家。

3.    **槌地攻擊**：槌地並生成三個子彈物件以拋物線方式拋出。

**Model**：管理狀態機和物理碰撞。

1.    閒置：當玩家距離過遠時，敵人保持不動。

2.    行走：玩家進入範圍內後，敵人朝向玩家移動，並根據朝向進行翻轉（調整localScale），施加水平力進行追擊。

3.    衝刺：通過壓縮localScale來模擬敵人準備衝刺的動作。隨後在指定時間內給予敵人向玩家方向的強大衝刺力。使用Coroutine來實現衝刺時間和冷卻。

4.    攻擊：使用隨機數決定一種攻擊模式，並通過Coroutine控制攻擊的冷卻。攻擊過程中生成臨時的攻擊觸發器來判定玩家是否受傷。

**View**：負責敵人動作的動畫呈現。

根據Model的狀態機切換對應播放不同的動畫，閒置、走路、衝刺和攻擊的動畫會分別設置在Animator內，當狀態轉換時透過Parameter自動播放相應的動畫。

三、      CAL敵人

**Presenter**：使用隨機數來選擇攻擊行為。

利用亂數隨機決定攻擊模式，分為Pointer炸彈、無視地形的彈幕、撞到牆體會返回的彈幕攻擊。被玩家攻擊五次後，隨機選擇五個平台中的一個進行移動。

**Model**：管理狀態機和物理碰撞。

1.    Pointer炸彈：CAL生成兩個炸彈，分別在左右兩側。炸彈朝向玩家移動，若撞擊玩家則爆炸造成傷害；若被玩家擊中則消失。

2.    無視地形的彈幕：CAL生成環形分布的子彈，向外擴散。子彈不受地形影響，直接穿透。

3.    會返回的彈幕攻擊：彈幕以輻射狀向外移動。若攻擊命中玩家，會顯示True並沿原路返回；未命中則顯示False返回移動。

4.    移動機制：被玩家攻擊五次後，CAL隨機傳送至五個平台之一。

**View**：負責敵人的渲染呈現。

四、      販賣機敵人

**Presenter**：

根據飲料的類型決定其行為：

1.    一般飲料：保持現有軌跡，無法被角色攻擊或擊回。

2.    毒藥飲料：檢測玩家攻擊，若被攻擊則施加反方向力；與角色碰撞則造成傷害；若反彈擊中怪物則對怪物造成傷害。

3.    治癒飲料：檢測玩家攻擊，若被攻擊則施加反方向力；與角色碰撞則對角色造成傷害；若反彈擊中怪物則為怪物回復生命值。

使用亂數決定攻擊模式（丟出不同組合的飲料），左右兩側隨機決定丟飲料的位置。

1.    全一般飲料：隨機位置、方向。

2.    治癒飲料為主：混合少量毒藥飲料和一般飲料。

3.    毒藥飲料為主：混合少量一般飲料。

飲料機只能被反彈的毒藥飲料攻擊，否則無法直接受傷。

**Model**：管理狀態機和物理碰撞。

1.    一般飲料：僅移動，不檢測玩家攻擊，與玩家碰撞直接造成傷害。

2.    毒藥飲料：檢測是否被玩家攻擊，被擊回時計算反方向力，更新飲料軌跡。碰撞飲料機時對飲料機造成傷害。與玩家碰撞時造成傷害。

3.    治癒飲料：檢測是否被玩家攻擊，被擊回時計算反方向力，更新飲料軌跡。碰撞飲料機時對飲料機回復生命值。與玩家碰撞時造成傷害。

4.    飲料機：根據亂數生成飲料組合，設定飲料出現的初始位置和方向。僅在毒藥飲料反彈後受到傷害，玩家無法透過攻擊直接改變飲料機狀態，需透過飲料進行間接攻擊。

**View**：負責敵人的渲染呈現。

# 肆、 結果與討論

參見 [/DEMO VIDEO](DEMO VIDEO.mp4)

# 伍、 結論

我們在物件導向和軟體工程的實作中遇到了許多挑戰。首先，最大的問題是我們難以掌握設計介面的適當時機。例如，在處理物件傷害時，使用介面會更方便，因為我們需要區分場景實體和敵人實體。這種情況下，它們被迫繼承相同的物件。我們得到的經驗是：「當一個功能出現在兩個毫不相關的地方時，就應該使用介面。」而我們也常遇到原本可以通過物件繼承解決的問題，卻最終需要額外撰寫程式碼來應對特殊情況。開發進度不理想，團隊經常出現資訊落差，導致我們花費過多時間在溝通和重新開發上。我們的許多腳本功能也過於複雜，缺乏單一性，導致後續維護和偵錯變得困難。在實作方面，我們發現對於敵人 AI，使用行為樹比狀態機更優秀，能提高程式的可讀性、維護性和效率。對某些 Unity
API 不夠熟悉，如物理引擎的 Layer Matrix等。未來我們希望能夠檢討並重構程式，以達到更好的效能和更優雅、簡潔的設計。

# 陸、 參考文獻

[1] **Nystrom, Robert.** *Game Programming Patterns.* 2 Nov. 2014, [https://gameprogrammingpatterns.com/](https://gameprogrammingpatterns.com/).

[2] **Mo, Qian.** *Game Programming Patterns' Examples.* 11 Oct. 2016. GitHub, [GitHub - QianMo/Unity-Design-Pattern: :tea: All Gang of Four Design Patterns written in Unity C# with many examples. And some Game Programming Patterns written in Unity C#. | 各种设计模式的Unity3D C#版本实现](https://github.com/QianMo/Unity-Design-Pattern).

[3] **Lafritz, James.** *"Model View Controller (MVC) Pattern in Unity."* Dev Genius, 5 May 2022, https://blog.devgenius.io/model-view-controller-mvc-pattern-in-unity-4ec9061dd0c.

[4] **Pittman, Kyle.** *"Math for Game Programmers: Building a Better Jump."* YouTube, 2016, [Math for Game Programmers: Building a Better Jump - YouTube](https://www.youtube.com/watch?v=hG9SzQxaCm8).

[5] **Eiserloh, Squirrel.** *"Math for Game Programmers: Juicing Your Cameras With Math."* YouTube, 2016, [Math for Game Programmers: Juicing Your Cameras With Math - YouTube](https://www.youtube.com/watch?v=tu-Qe66AvtY).

[6] **DawnosaurDev.** *Platformer Movement.* GitHub, 6 Aug. 2021, [GitHub - DawnosaurDev/platformer-movement](https://github.com/DawnosaurDev/platformer-movement).

[7] **SmallHedge.** *SoundManager.* GitHub, 12 Apr. 2024, [GitHub - SmallHedge/SoundManager: Trigger Unity Sound from anywhere!](https://github.com/SmallHedge/SoundManager).

[8] **Sasquatch B
Studios.** *"How to Make a Camera System (Like Hollow Knight's) in Unity using Cinemachine | 2D Tutorial."* YouTube, 9 Mar. 2023, [How to Make a Camera System (Like Hollow Knight&#39;s) in Unity using Cinemachine | 2D Tutorial - YouTube](https://www.youtube.com/watch?v=9dzBrLUIF8g).

# 柒、 素材

1.  Tallbeard Studios. Music Loop Bundle. [Itch.io](http://Itch.io), 31 Oct. 2024, [[Music Assets] FREE Music Loop Bundle by Tallbeard Studios](https://tallbeard.itch.io/music-loop-bundle).

2.  Rotting Pixels. Castle Platformer Tileset [16x16][FREE]. [Itch.io](http://Itch.io), 15 July 2019, [Castle Platformer Tileset [16x16][FREE] by RottingPixels](https://rottingpixels.itch.io/castle-platformer-tileset-16x16free).

3.  Pixel Frog. Treasure Hunters. [Itch.io](http://Itch.io), 18 Dec. 2019, [Treasure Hunters by Pixel Frog](https://pixelfrog-assets.itch.io/treasure-hunters).

4.  Pixel Frog. Kings and Pigs. [Itch.io](http://Itch.io), 15 Oct. 2019, [Kings and Pigs by Pixel Frog](https://pixelfrog-assets.itch.io/kings-and-pigs).

5.  Prinbles. UNDER GUI. [Itch.io](http://Itch.io), 3 Sept. 2023, [UNDER GUI by Prinbles](https://prinbles.itch.io/under).

6.  Anokolisa. Legacy Fantacy. [Itch.io](http://Itch.io), 27 Mar. 2019, [Free - Pixel Art Asset Pack - Sidescroller Fantasy - 16x16 Forest Sprites by Anokolisa](https://anokolisa.itch.io/sidescroller-pixelart-sprites-asset-pack-forest-16x16).

7.  Tennessippi. Alphabet & Numbers. [Itch.io](http://Itch.io), 19 Dec. 2021, [Alphabet &amp; Numbers by Tennessippi](https://tennessippistudios.itch.io/alphabet-numbers).

8.  Omniclause. Spikes. [Itch.io](http://Itch.io), 12 July 2021, [Spikes by Omniclause](https://omniclause.itch.io/spikes).

9.  Karsiori. FREE Pixel Art Padlock Pack -
Animated. [Itch.io](http://Itch.io), 3 Dec. 2023, [FREE Pixel Art Padlock Pack - Animated by karsiori](https://karsiori.itch.io/pixel-art-padlock-pack-animated).

10.小森平. 免費音效. 小森平免費音效素材, n.d., https://taira-komori.jpn.org/human01tw.html.

11.pedroricciotti. SMOKE-PRACTICE. [Itch.io](http://Itch.io), 8 Sept. 2020, [SMOKE-PARTICLE by pedroricciotti](https://pedroricciotti.itch.io/particle-smoke).

12.Sillas Frota. Plasma Explosion. [Itch.io](http://Itch.io), 17 Sept. 2020, [Plasma Explosion by Sillas Frota](https://sillas-frota.itch.io/plasma-explosion).

13.Frostwindz. Pixel Art Skill Animations - Warrior. [Itch.io](http://Itch.io), 22 Oct. 2024, [Pixel Art Skill Animations - Warrior by Frostwindz](https://frostwindz.itch.io/pixel-art-skill-animations-warrior).
