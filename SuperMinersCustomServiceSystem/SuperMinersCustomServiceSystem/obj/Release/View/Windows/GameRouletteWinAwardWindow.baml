   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemL    FSuperMinersCustomServiceSystem.View.Windows.GameRouletteWinAwardWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$'  !幸运大转盘实物中奖处理��$	��300��$	��300�� � WindowStyle$ 
ToolWindow=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $
��113* ��� $
��181* �.+��� $��ID��$
��10,5q�$��Right=�$��Center=��� $��1�� �� 
IsReadOnly . $
��10,5q�$��Center=����� *
RecordID+ �� Mode$ OneWay=��� $��1��$��玩家用户名��$
��10,5q�$��Right=�$��Center=��� $��1��$��1�� . $
��10,5q�$��Center=����� *
UserName+$ OneWay=��� $��2��$��中奖信息��$
��10,5q�$��Right=�$��Center=��� $��2��$��1�� . $
��10,5q�$��Center=����� *AwardItemName+$ OneWay=��� $��3��$��中奖时间��$
��10,5q�$��Right=�$��Center=��� $��3��$��1�� . $
��10,5q�$��Center=����� *	WinTime+$ OneWay=��� $��4��$��领奖信息1��$
��10,5q�$��Right=�$��Center=��� $��4��$��1�� . $
��10,5q�$��Center=����� *
GotInfo1+$ OneWay=� �� StringFormat$ f2���� $��5��$��领奖信息2��$
��10,5q�$��Right=�$��Center=��� $��5��$��1�� . $
��10,5q�$��Center=����� *
GotInfo2+$ OneWay=��� -   $��	btnCancel��$��9��$��2��$��取消��$	��100��$��10q�$��Right=��� -   $��btnOK��$��9��$��2��$��确认支付��$	��100��$��10,10,120,10q�$��Right=�