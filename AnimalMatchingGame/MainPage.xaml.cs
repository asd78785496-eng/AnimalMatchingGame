namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;   /* 讓包含emoji的FlexLayout顯示出來 */
            PlayAgainButton.IsVisible = false; /* 讓Play Again按鈕隱藏起來 */

            List<string> animalEmoji = [
                "🐶", "🐶",
                "🐱", "🐱",
                "🐭", "🐭",
                "🐹", "🐹",
                "🐰", "🐰",
                "🦊", "🦊",
                "🐻", "🐻",
                "🐼", "🐼",
             ]; /* 這裡定義了一個包含16個元素的列表，每個動物表情符號都出現兩次，形成一對。這些表情符號將用於遊戲中的配對。 */

            foreach (var button in AnimalButtons.Children.OfType<Button>()) /*在FlexLayout中找到每一個按鈕，並為它們執行大括號{}的陳述式*/
            {
                int index = Random.Shared.Next(animalEmoji.Count); /*選擇一個介於0和串列剩餘的emoji數量之間的隨機數，並將它命名為index。這個隨機數將用來從animalEmoji列表中選擇一個表情符號。*/

                string nextEmoji = animalEmoji[index]; /*從animalEmoji列表中使用剛才生成的隨機索引index來獲取一個表情符號，並將它命名為nextEmoji。這個表情符號將被分配給當前的按鈕。*/

                button.Text = nextEmoji; /*將剛才獲取的表情符號nextEmoji設置為當前按鈕的文本，這樣按鈕就會顯示出這個表情符號。*/

                animalEmoji.RemoveAt(index); /*從animalEmoji列表中移除剛才使用的表情符號，這樣它就不會被再次選中，確保每個表情符號只會被分配給兩個按鈕。*/

            }
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick); /*啟動一個計時器，每0.1秒觸發一次TimerTick方法。這個計時器將用來更新遊戲中的時間顯示，讓玩家知道他們已經花了多少時間在遊戲中。*/
        }

        int tenthsOfSecondsElapse = 0; /* 這個變數用來追蹤遊戲中已經過了多少十分之一秒。每當計時器觸發一次，這個變數就會增加1，表示又過了0.1秒。當玩家完成遊戲後，這個變數會被重置為0，以便在下一次遊戲開始時重新計算時間。 */

        private bool TimerTick() /* 這個方法是計時器每次觸發時執行的回調函數。它的主要功能是更新遊戲中的時間顯示，並檢查遊戲是否已經完成。當玩家完成遊戲後，這個方法會停止計時器，並重置時間計數器，以便在下一次遊戲開始時重新計算時間。 */
        {
            if (!this.IsLoaded) return false; /* 這行代碼檢查當前頁面是否已經加載完成。如果頁面尚未加載完成，則返回false，這將停止計時器的運行。這樣做是為了確保計時器不會在頁面尚未準備好時開始運行，避免可能的錯誤或異常情況。 */

            tenthsOfSecondsElapse++; /* 每當計時器觸發一次，這行代碼就會將tenthsOfSecondsElapse變數增加1，表示又過了0.1秒。這樣可以用來追蹤遊戲中已經過了多少時間，並在遊戲完成後顯示給玩家。 */

            TimeElapsed.Text = "Time elapsed: " + (tenthsOfSecondsElapse / 10F).ToString("0.0s");/* 這行代碼更新遊戲中的時間顯示。它將tenthsOfSecondsElapse變數除以10，將十分之一秒轉換為秒，然後使用ToString方法將其格式化為一個帶有一位小數的字符串，最後將這個字符串設置為TimeElapsed文本的內容，讓玩家看到他們已經花了多少時間在遊戲中。 */

            if (PlayAgainButton.IsVisible) /* 這行代碼檢查Play Again按鈕是否可見。如果Play Again按鈕可見，這意味著玩家已經完成了遊戲，並且計時器應該停止運行。當玩家完成遊戲後，這個方法會返回false，這將停止計時器的運行，並重置時間計數器，以便在下一次遊戲開始時重新計算時間。 */
            {
                tenthsOfSecondsElapse = 0;
                return false;
            }
            return true; /* 如果Play Again按鈕不可見，這意味著遊戲仍在進行中，計時器應該繼續運行。當玩家完成遊戲後，這個方法會返回false，這將停止計時器的運行，並重置時間計數器，以便在下一次遊戲開始時重新計算時間。 */
        }

        Button lastClicked;
        bool findMatch = false;
        int matchesFound;
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findMatch == false)) 
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findMatch = true;
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text)
                        && (!String.IsNullOrWhiteSpace(buttonClicked.Text)))
                    {
                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findMatch = false;
                }
            }
            if (matchesFound == 8) /* 當玩家找到所有的配對後，matchesFound的值將達到8，這表示玩家已經成功地找到了所有的配對。 */
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false; /* 當玩家找到所有的配對後，將包含emoji的FlexLayout隱藏起來，這樣玩家就看不到任何按鈕了。 */
                PlayAgainButton.IsVisible = true; /* 同時，將Play Again按鈕顯示出來，讓玩家有機會重新開始遊戲。 */
            }
        }


    }
}
