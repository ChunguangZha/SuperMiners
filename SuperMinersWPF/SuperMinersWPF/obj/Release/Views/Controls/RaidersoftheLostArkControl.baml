   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  >    8SuperMinersWPF.Views.Controls.RaidersoftheLostArkControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5my&clr-namespace:SuperMinersWPF.MyControl     ���	#FF005793
�������  � 	Resources   �� 
TargetType �� Property   LogoPanelBackground �� Value��%B   '��       7   �� " ��.���  � ��8   ��  � ��$ 1q�.���  � ColumnDefinitions �� $	��250 ��� $��* �.+��  � RowDefinitions �� $	��39* ��� $
��Auto ��� $
��211* ��� $
��Auto �.+��� $��
夺宝show��$��48(�$��Center=�$��Center=�$��STCaiyun)�$
��Bold"�� $��1�����	#FF1D1864 �� �� �� .+��� $��奖期��$��	20,5,10,5q�$��Center=�$
��Left=��� $��1��$��得主��$
��10,5q�$��Center=�$
��Left=��� $��2��$��奖额��$
��10,5q�$��Center=�$
��Left=��� $��2��$��10q�._��� -   $��listRaiderHistoryRecords��
������ 
�������$��Center=�$��Disabled=����� .R��  �� $��70 ��� $��70 ��� $��70 �.+��� $��5q�$
��Left=��� $	�第�� �� ��� *ID+ �� Mode$ OneWay=� �� $	�期���� $��1��$��5q�$
��Left=�$��NoWrap=��� ��� *WinnerUserName+$ OneWay=��� $��2��$��5q�$
��Left=��� ��� *	WinStones+$ OneWay=�� $��3�����	#FF1D1864.+��� -   $��btnViewMyRaiderRecord��$��查看我的夺宝记录>�����#ffdc15$��14(�$��5q�$��Center=�$��Stretch=�$��0q�$��Italic$���Z� � $��1�� �� $
��Auto ��� $
��228* �.+�� ���	#FF1D1864 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��26(�$��Center=�$��Center=�$
��10,5q��� $	�第�� �� ��� *ID+$ OneWay=� �� $	�期���� $��1�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��剩余夺宝时间：��$��20(�$
��10,5q�$��Center=��� $��38(�$
��10,5q����#ffdc15�� ��� *CountDownMinutes+$ OneWay=� �� $	�分�� �� ��� *CountDownSeconds+$ OneWay=� �� $	�秒���� $��2��$ 
Horizontal=�$��Center=�.+��� ���#ffdc15$��24(�$��Italic$�$
��10,5q�$��Center=����� *JoinedPlayerCount+�� $��位矿主已经参与��$��20(�$
��10,5q�$��Center=��� $��宝池累计：��$��20(�$
��10,5q�$��Center=��� $��24(�$��Italic$�$
��10,5q����#ffdc15$��Center=��� ��� *AwardPoolSumStones+$ OneWay=� �� $�矿石���� $��3��$ 
Horizontal=�$��Center=�y��� *BetJoinButtonVisible+.+��� -   $��btnSubtract��$��-��$y�	Collapsed=�$
��10,5q�$��20��$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numBetStoneCount��$	��100��	   Minimum$	 1Q�
   Value$

 1000Q�   Maximum$ 1000000Q�$��24��$
��10,5q�$��Center=�$��Center=��� $��矿石��$��Center=��� -   $��btnAdd��$��+��$y�	Collapsed=�$��10q�$��20��$��Center=��� -   $��btnJoin��$��开始夺宝��$	��150��$��40��$
��10,5q�$��24(�  UserInfoButtonStyle#��[ $��Center=��� $��3��$��Center=�$��Center=�$ 
Horizontal=�y��� *WinnerUserNameVisible+.+��� $��本轮中奖者为：��$��10q�$��Center=�$��Center=��� 
���  ��$��10q�$��20(�$��Center=�$��Center=����� *WinnerUserName+$ OneWay=��� $��中奖额为：��$��10q�$��Center=�$��Center=��� 
���  ��$��20(�$��10q�$��Center=�$��Center=����� *	WinStones+$ OneWay=��� $��矿石��$��10q�$��Center=�$��Center=�� $��4��$��2�����	#FF1D1864 �� �� $	��160 � �� $
��Auto ��� .+�� $��0�����	#FF005793.+��� $��0��$��玩法说明��$��10q�$��26(�$��Center=�$��Center=�� $��1�����	#FF005793.+��� $��本期夺宝��$��10q�$��26(�$��Center=�$��Center=��� $��1��$��0��$��10q�._��� $��10q�.+��� $����       当投注人数超过1人时开始倒计时，5分钟一期，所有投注人根据总投注数计算中奖，多人投注一人中奖，奖金为总投注额，服务器扣除10%服务费； 当赢取奖金不足以支付服务费时，只返还投注额。��$
��Wrap=�$��14(��� $��       例：��$
��Wrap=�$��12(��� $&��        玩家一、投注额5000��$
��Wrap=�$��12(��� $%��       玩家二、投注额300��$
��Wrap=�$��12(��� $$��       玩家三、投注额50��$
��Wrap=�$��12(��� $����       如：中奖人为玩家一时，则获得总投注额：5350减去10%服务费：535，获得投注低于玩家一的投注额，则返还5000；��$
��Wrap=�$��12(��� $~��x       如：中奖人为玩家二或玩家三时，则获得总投注额：5350减去10%服务费：535，返还4815；��$
��Wrap=�$��12(��� $��1��$��1��$��10q�._��� -   $��listSelfCurrentBetRecords��
������ 
�������$��Stretch=����� .R��  �� �� $
��Auto �.+��� $��5q����� *ShortTimeText+$ OneWay=��� $��1��$ 
Horizontal=�.+��� $��5q����� *	BetStones+$ OneWay=��� $��矿石��$��5q�