   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  6    0SuperMinersWPF.Views.Controls.GambleStoneControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5my&clr-namespace:SuperMinersWPF.MyControl     -   
����������	#FF513700  � 	Resources     	GridColor �� Color  	imgInGrid �� 
TargetType �� Property �� Value  StoryboardOpenGamble �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%�  &	        &	      &	 S     �� $
 #000���� " ��.���  � ��$ 2q���  � ��$ UniformToFill=��� -   .��U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ imgSplitStone1��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$	 -80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ imgSplitStone1��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$	 -80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ imgSplitStone2��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$ 80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ imgSplitStone2��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$	 -80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ imgSplitStone3��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$ 80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ imgSplitStone3��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$ 80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ imgSplitStone4��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$	 -80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ imgSplitStone4��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$ 80Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ imgFlash��.\�  $ 0��$	 150Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$
 -150Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ imgFlash��.\�  $ 0��$
 -150Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1��$	 150Q�.��� 	 � ColumnDefinitions	 �� $
��Auto ��� $
��197* �.+�� 
 � RowDefinitions
 �� $
��Auto ��� $��50 ��� $
��Auto ��� $
��Auto ��� .+��� $��疯狂猜石��$��26(�$��Center=�$��Center=�$��10q�� $��1��	 �� �� �� �� �� .+�� 
 �� �� .+��� $��上轮统计：��$��Center=�$��Right=��� $��1��$��本轮统计：��$��Center=�$��Right=��� $��1�����	#FFC7A365$��2q� �� CornerRadius$ 5��._�� 	 �� $
��Auto ��� 
 �� �� $
��Auto ��� .+��� $��3��$��2q�$��24��$��24��$��Center=�$��Center=�$7��1/SuperMinersWPF;component/Resources/gamblered.png���� $��1��$��Center=�$��Center=����� *" CurrentRoundInfo.LastWinRedCount+ �� Mode$ OneWay=��� $��1��$��1��$��1��
���  ��$
��10,0q�$��Center=�$��Stretch=��� $��1��$��2��$��Center=�$��Center=����� *%#CurrentRoundInfo.CurrentWinRedCount+$ OneWay=��� $��2�����	#FFC7A365$��2q�$ 5��._�� 	 �� $
��Auto ��� 
 �� �� $
��Auto ��� .+��� $��3��$��2q�$��24��$��24��$��Center=�$��Center=�$9��3/SuperMinersWPF;component/Resources/gamblegreen.png���� $��1��$��Center=�$��Center=����� *$"CurrentRoundInfo.LastWinGreenCount+$ OneWay=��� $��1��$��1��$��1��
���  ��$
��10,0q�$��Center=�$��Stretch=��� $��1��$��2��$��Center=�$��Center=����� *'%CurrentRoundInfo.CurrentWinGreenCount+$ OneWay=��� $��3�����	#FFC7A365$��2q�$ 5��._�� 	 �� $
��Auto ��� 
 �� �� $
��Auto ��� .+��� $��3��$��2q�$��24��$��24��$��Center=�$��Center=�$8��2/SuperMinersWPF;component/Resources/gambleblue.png���� $��1��$��Center=�$��Center=����� *#!CurrentRoundInfo.LastWinBlueCount+$ OneWay=��� $��1��$��1��$��1��
���  ��$
��10,0q�$��Center=�$��Stretch=��� $��1��$��2��$��Center=�$��Center=����� *&$CurrentRoundInfo.CurrentWinBlueCount+$ OneWay=��� $��4�����	#FFC7A365$��2q�$ 5��._�� 	 �� $
��Auto ��� 
 �� �� $
��Auto ��� .+��� $��3��$��2q�$��24��$��24��$��Center=�$��Center=�$:��4/SuperMinersWPF;component/Resources/gambleyellow.png���� $��1��$��Center=�$��Center=����� *%#CurrentRoundInfo.LastWinPurpleCount+$ OneWay=��� $��1��$��1��$��1��
���  ��$
��10,0q�$��Center=�$��Stretch=��� $��1��$��2��$��Center=�$��Center=����� *(&CurrentRoundInfo.CurrentWinPurpleCount+$ OneWay=��� $��2��$��5q�$��1q�#��[  ._�� -   $��gridWinnedColor��$	��420��$	��336��	 �� �� �� �� �� �� �� �� �� �� 
 �� �� �� �� �� �� �� �� .+��� $��0��$��4��$��6��$��1��#��[  $��1q�$��Bottom=��� $��1��$��4��$��6��$��1��#��[  $��1q�$��Bottom=��� $��2��$��4��$��6��$��1��#��[  $��1q�$��Bottom=��� $��3��$��10��$��1��#��[  $��1q�$��Bottom=��� $��4��$��10��$��1��#��[  $��1q�$��Bottom=��� $��5��$��10��$��1��#��[  $��1q�$��Bottom=��� $��6��$��10��$��1��#��[  $��1q�$��Bottom=��� $��0��$��4��$��4��$��1��#��[  $��1q�$��Right=��� $��1��$��4��$��4��$��1��#��[  $��1q�$��Right=��� $��2��$��4��$��4��$��1��#��[  $��1q�$��Right=��� $��3��$��8��$��1��#��[  $��1q�$��Right=��� $��4��$��8��$��1��#��[  $��1q�$��Right=��� $��5��$��8��$��1��#��[  $��1q�$��Right=��� $��6��$��8��$��1��#��[  $��1q�$��Right=��� $��7��$��8��$��1��#��[  $��1q�$��Right=��� $��8��$��8��$��1��#��[  $��1q�$��Right=�� -    �Name$ panelAnimation��$��4��$��4��
���   � J� Opacity$ 1Q� K� ZIndex$ 0��
 �� �� $��16 �.+�� $	��8,0q��� $��1��$��Center=��� ��� *CurrentInningInfo.InningIndex+$ OneWay=� �� $	�局�� �� ��� *(&CurrentInningInfo.CountDownSecondsText+$ OneWay=��� -   $��imgSrcStone��$��4��$��4��$��18,10,18,26q�$ 1Q�$	 100��$6��0/SuperMinersWPF;component/Resources/srcstone.png��y��� *#!CurrentInningInfo.SrcStoneVisible+�� -   $��imgSplitStone1��$��4��$��4��$��18,10,18,26q�$0��*/SuperMinersWPF;component/Resources/s1.png�� J� RenderTransformOrigin$ 0.5,0.5&�$	 100��z�V� .~��� �� �� T�  T� X$ 0Q� T� Y$ 0Q��� -   $��imgSplitStone2��$��4��$��4��$��18,10,18,26q�$0��*/SuperMinersWPF;component/Resources/s2.png��$ 0.5,0.5&�$	 100��z�V� .~��� �� �� T� $ 0Q�$ 0Q��� -   $��imgSplitStone3��$��4��$��4��$��18,10,18,26q�$0��*/SuperMinersWPF;component/Resources/s3.png��$ 0.5,0.5&�$	 100��z�V� .~��� �� �� T� $ 0Q�$ 0Q��� -	   $��imgSplitStone4��$��4��$��4��$��18,10,18,26q�$0��*/SuperMinersWPF;component/Resources/s4.png��$ 0.5,0.5&�$	 100��z�V� .~��� �� �� T� $ 0Q�$ 0Q��� -
   $��imgFlash��$��4��$��4��$��18,10,18,26q�$3��-/SuperMinersWPF;component/Resources/flash.png��$ 0.5,0.5&�$	 100��z�V� .~��� �� �� T� $
 -150Q�$	 150Q��� $��5��$��4��$��5q�$y�	Collapsed=�$:��4/SuperMinersWPF;component/Resources/gambleyellow.png��$ 0.5,0.5&�$ 0��z�V� .~���  �� ScaleX$ 3Q� �� ScaleY$ 3Q��� �� T� $
 -105Q�$
 -155Q�� $��3��	 �� �� �� �� .+���  �� Orientation$ 
Horizontal=�$��5q�.+��� $��28��$��28��$ 5��$��0q�._��� $7��1/SuperMinersWPF;component/Resources/gamblered.png���� $��5q�$��Center=�$��14(����� *#!CurrentInningInfo.BetRedStoneText+$ OneWay=��� $��1��$ 
Horizontal=�$��5q�.+��� $��28��$��28��$ 5��$��0q�._��� $9��3/SuperMinersWPF;component/Resources/gamblegreen.png���� $��5q�$��Center=�$��14(����� *%#CurrentInningInfo.BetGreenStoneText+$ OneWay=��� $��2��$ 
Horizontal=�$��5q�.+��� $��28��$��28��$ 5��$��0q�._��� $8��2/SuperMinersWPF;component/Resources/gambleblue.png���� $��5q�$��Center=�$��14(����� *$"CurrentInningInfo.BetBlueStoneText+$ OneWay=��� $��3��$ 
Horizontal=�$��5q�.+��� $��28��$��28��$ 5��$��0q�._��� $:��4/SuperMinersWPF;component/Resources/gambleyellow.png���� $��5q�$��Center=�$��14(����� *&$CurrentInningInfo.BetPurpleStoneText+$ OneWay=�� $��1��
 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��254* �	 �� �� �� �� .+��� -    ��Name$ txtWinnerNotice��$��4��$�� ��$��5q�$��22(�$��Center=�$��Center=��� $��1��$��0��$	��5,0q�$��Center=�$��Center=����� *.,CurrentInningPlayerBetRecord.BetRedStoneText+$ OneWay=��� $��1��$��1��$	��5,0q�$��Center=�$��Center=����� *0.CurrentInningPlayerBetRecord.BetGreenStoneText+$ OneWay=��� $��1��$��2��$	��5,0q�$��Center=�$��Center=����� */-CurrentInningPlayerBetRecord.BetBlueStoneText+$ OneWay=��� $��1��$��3��$	��5,0q�$��Center=�$��Center=����� *1/CurrentInningPlayerBetRecord.BetPurpleStoneText+$ OneWay=��� -   $��	btnBetRed��$��2��$��0��$��15q�
������ $��Center=�$��Center=�{��� *%#CurrentInningInfo.BetInButtonEnable+���� $7��1/SuperMinersWPF;component/Resources/gamblered.png���� -   $��btnBetGreen��$��2��$��1��$��15q�
������ $��Center=�$��Center=�{��� *%#CurrentInningInfo.BetInButtonEnable+���� $9��3/SuperMinersWPF;component/Resources/gamblegreen.png���� -   $��
btnBetBlue��$��2��$��2��$��15q�
������ $��Center=�$��Center=�{��� *%#CurrentInningInfo.BetInButtonEnable+���� $8��2/SuperMinersWPF;component/Resources/gambleblue.png���� -   $��btnBetPurple��$��2��$��3��$��15q�
������ $��Center=�$��Center=�{��� *%#CurrentInningInfo.BetInButtonEnable+���� $:��4/SuperMinersWPF;component/Resources/gambleyellow.png���� $��3��$��0��$	��5,0q�$��Center=�$��Center=��� $�	猜中赚�� �� $
�2倍�����	#FFFF4F6E�� $��3��$��1��$	��5,0q�$��Center=�$��Center=��� $�	猜中赚�� �� $
�3倍�����	#FFFF4F6E�� $��3��$��2��$	��5,0q�$��Center=�$��Center=��� $�	猜中赚�� �� $
�6倍�����	#FFFF4F6E�� $��3��$��3��$	��5,0q�$��Center=�$��Center=��� $�	猜中赚�� �� $�50倍�����	#FFFF4F6E�� $��4��$��4��$ 
Horizontal=�$��3q�
���**��.+��� $��每次下注矿石：��$
��10,5q�
�������	� -   $��chk10Stones��$��10矿石�� d� 	IsChecked$
 TrueY� 	� 	GroupName$ Times��$��5q�
�������	� -   $��chk100Stones��$��	100矿石��$ Times��$��5q�
�������	� -   $��chk1000Stones��$��
1000矿石��$ Times��$��5q�
��������� $��5��$��4��$��玩法说明��$��10q�$��18(�$��Center=�$��Center=��� $��6��$��4��$��10q����	#FFC7A365$ Vertical=�.+��� $4��.1. 每个原石含有4种宝石中的一种。��$��5q����#ffdc15$
��Wrap=�$��14(��� $?��92. 点击每个宝石按钮，即选择猜宝石种类。��$��5q����#ffdc15$
��Wrap=�$��14(��� $o��i3. 每次猜宝石都要花费一定数量的矿石，同时猜中将获得相应倍数的矿石奖励。��$��5q����#ffdc15$
��Wrap=�$��14(��� $Q��K4. 每次切开原石后，宝石商人（系统）会自动发放奖励。��$��5q����#ffdc15$
��Wrap=�$��14(��� $����小提示：游戏过程中，可能会由于网络原因造成极少用户奖励没有到账。请不要担心，系统会在24小时内自动返还至您的账户。��$��5q����#ffdc15$
��Wrap=�$��14(�