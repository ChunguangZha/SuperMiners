   M S B A M L   `   `   `  ����   SuperMinersWPF8    2SuperMinersWPF.Views.GatherStonesForBuyMinerWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  购买矿工��$	��230��$	��300��   PlayerInfoBackground#���   
������� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$ 
ToolWindow=�.��� $��5q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��1��$0��*购买矿工之前，需先收取矿石。��
���  ��$��5q��� $��1��$��当前生产��$��5q�$��Right=�$��Center=��� -   $��txtAllOutputStones��$��1��$��1�� �� 
IsReadOnly . $��5q�$��Center=��� $��1��$��2��$��Center=�$��5q����� *Strings.Stone+ �� Mode$ OneWay=�  Language �� Source# [ �� $��2��$��	可收取��$��5q�$��Right=�$��Center=��� -   $��txtGatherableStones��$��2��$��1�� . $��5q�$��Center=��� $��2��$��2��$��Center=�$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��3��$��提示��$��5q�$��Right=��� $��3��$��1��$]��W系统只按整数进行收取，小数部分将自动舍去，请确认是否收取？��$��5q�$
��Wrap=��� -   $��btnOK��$��4��$��3��$��收取矿石��$��75��$��	5,5,200,5q�$��Right=�$	��Top=��� -   $��
btnDiscard��$��4��$��3��$��丢弃矿石��$��75��$��	5,5,100,5q�$��Right=�$	��Top=��� -   $��	btnCancel��$��4��$��3��$��取消��$��75��$��5q�$��Right=�$	��Top=�