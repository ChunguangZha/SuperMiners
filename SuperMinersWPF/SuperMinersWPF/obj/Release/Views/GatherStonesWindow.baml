   M S B A M L   `   `   `  ����   SuperMinersWPF-    'SuperMinersWPF.Views.GatherStonesWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  收取矿石��$	��230��$	��300��   PlayerInfoBackground#���   
������� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$ 
ToolWindow=�.��� $��5q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��当前生产��$��5q�$��Right=�$��Center=��� -   $��txtAllOutputStones��$��1�� �� 
IsReadOnly . $��5q�$��Center=��� $��2��$��Center=�$��5q����� *Strings.Stone+ �� Mode$ OneWay=�  Language �� Source# [ �� $��1��$��	可收取��$��5q�$��Right=�$��Center=��� -   $��txtGatherableStones��$��1��$��1�� . $��5q�$��Center=��� $��1��$��2��$��Center=�$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��2��$��提示��$��5q�$��Right=��� $��2��$��1��$]��W系统只按整数进行收取，小数部分将自动舍去，请确认是否收取？��$��5q�$
��Wrap=��� -   $��btnOK��$��3��$��3��$��收取��$��75��$��	5,5,110,5q�$��Right=�$	��Top=��� -   $��	btnCancel��$��3��$��3��$��取消��$��75��$��5q�$��Right=�$	��Top=�