   M S B A M L   `   `   `  ����   SuperMinersWPF5    /SuperMinersWPF.Views.Windows.StoneFactoryWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � WindowStartupLocation$  CenterScreen=����	#FF478F61
������� � Title$ 	加工厂��$	��600��$	��600�� � 	Resources    UserInfoTextBoxStyle �� 
TargetType �� Property �� Value��%�   &	        �� " ��.���  �@��
IsReadOnly . ��  � ��
 �������  � ��
 ���� ��  � ��$
 Auto����  � ��$ 5q�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�� 
���  �� �� $
��Auto ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� $��昨日单机收益：��$��5q�$��18(�
�������$��Bottom=��� $��5q�$��28(�$
��Bold"�
��� ������� *YesterdayFactoryProfitRate+�� $��灵币��$��5q�$��18(�$��Bottom=�$��Italic$�
��������� $��50,5,5,5q�$��昨日总收益：��$��18(�
�������$��Bottom=��� $��5q�$��28(�$
��Bold"�
��� ������� *YesterdayTotalProfitRMB+�� $��灵币��$��5q�$��18(�$��Bottom=�$��Italic$�
��������� $��1��$ 
Horizontal=�$��Center=�$��Center=�.+��� $��累计收益：��$��5q�$��16(�
�������$��Center=��� $��5q�$��16(�
��� ���$��Center=����� *TotalProfitRMB+�� $��灵币��$��5q�$��12(�$��Center=�$��Italic$�
��������� $��50,5,5,5q�$��可提取：��$��16(�
�������$��Center=��� $��5q�$��16(�
��� ���$��Center=����� *WithdrawableProfitRMB+�� $��灵币��$��5q�$��12(�$��Center=�$��Italic$�
��������� -   $��btnWithdrawProfitRMB��$��提取��  PlayerInfoBackground#��[ 
�������$��16(�$	��100��$��40��$��Center=�$��	30,0,10,0q�{��� *WithdrawProfitRMBEnable+� -   $��panelFactoryOpenning��$��2�� �� $	��53* ��� $
��447* � � ColumnDefinitions �� $��* ��� $��* �.+�� $��2��
��������� $��资产��$��18(�$��Center=�$��Center=��� $��1��$��0��.+�� ���	#FFECECEC.+��� $��矿石加工��
���   �$��Center=�$��Center=�$��5q��  �� $��80 ���  �� �� �� �� .+��� $��凝练中：��$��5q�$��Center=�$��Right=��� -   $��txtValidStoneStack��$��1��#��[  $��5q�	 �� 
IsReadOnly	 . ���� *TotalStoneCount+
 �� Mode$
 OneWay=��� $��1��$��运输中：��$��5q�$��Center=�$��Right=��� -   $��txtFreezingStoneStack��$��1��$��1��#��[  $��5q�	 . ���� *FreezingStoneCount+$
 OneWay=��� $��2��$��可返回：��$��5q�$��Center=�$��Right=��� -   $��txtWithdrawableStoneStack��$��2��$��1��#��[  $��5q�	 . ���� *WithdrawableStoneCount+$
 OneWay=��� -   $��btnWithdrawStoneStack��$��3��$��2��$��取出矿石��#��[ 
�������$	��100��$��30��$��20,5,5,5q�$
��Left=�{��� *WithdrawableStoneEnable+�� -   $��btnJoinInStoneStack��$��3��$��2��$��添加矿石��#��[ 
�������$	��100��$��30��$��5,5,20,5q�$��Right=�� ���	#FFECECEC.+��� $��苦力��
���   �$��Center=�$��Center=�$��5q��  �� $��80 ���  �� �� �� �� .+��� $��劳动中：��$��5q�$��Center=�$��Right=��� -	   $��txtValidSlaves��$��1��#��[  $��5q�	 . ���� *EnableSlavesCount+$
 OneWay=��� $��1��$��剩余寿命：��$��5q�$��Center=�$��Right=��� $��1��$��1��$��5q�	 . #��[  ���� *SlaveLiveDiscountmsText+$
 OneWay=��� $��2��$��休息中：��$��5q�$��Center=�$��Right=��� -
   $��txtFreezingSlaves��$��2��$��1��#��[  $��5q�	 . ���� *FreezingSlavesCount+$
 OneWay=��� -   $��btnJoinInSlave��$��3��$��2��$��增加苦力��#��[ 
�������$	��100��$��30��$��5q�$��Center=�� ���	#FFECECEC.+��� $��氪金��
���   �$��Center=�$��Center=�$��5q��  �� $��80 ���  �� �� �� �� .+��� $��1��$��可用天数：��$��5q�$��Center=�$��Right=��� -   $��txtFoodUsableDays��$��1��$��1��#��[  $��5q�	 . ���� *Food+$
 OneWay=��� -   $��btnBuyFoods��$��3��$��2��$��购买氪金��#��[ 
�������$	��100��$��30��$��20,5,5,5q�$
��Left=��� -   $��btnFeedSlave��$��3��$��2��$��投喂苦力��#��[ 
�������$	��100��$��30��$��5,5,20,5q�$��Right=��� $��1��$��收益记录��$��18(�$��Center=�$��Center=��� -   $��lvProfitList��$��1��$��1��$��5q�$��Stretch=����� .R��� $��0,0,0,1q�#��[ ._�� ���� *
Background+ �� �� �� .+��� $��5q����� *OperTimeText+$
 OneWay=��� $��1��$��5q��� ��� *OperRMBText+$
 OneWay=� �� $�灵币���� $��2��$��5q����� *ProfitTypeText+$
 OneWay=�