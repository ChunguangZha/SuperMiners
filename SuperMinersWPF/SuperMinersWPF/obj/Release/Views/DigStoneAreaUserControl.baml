   M S B A M L   `   `   `  ����   SuperMinersWPF2    ,SuperMinersWPF.Views.DigStoneAreaUserControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -   .��� ���	#FFF0F0F0  � ColumnDefinitions  �� �� $
��Auto �.+�� -   $��gridActionMessage�� � RowDefinitions �� $
��Auto ��� .+��� ���	#FFAFBCBB �� Orientation$ 
Horizontal=�.+��� $ 
Horizontal=�.+��� $��20��$��20��$��	30,5,10,5q�$4��./SuperMinersWPF;component/Resources/player.png���� $	��5,0q�$��Center=�
��������� $	�共�� �� ��� *AllSystemState.AllPlayerCount+ �� Mode$ OneWay=� �� $�矿主�� �� ��� *AllSystemState.AllMinersCount+$ OneWay=� �� ��� *Strings.Miner+$ OneWay=�   Language �� Source# [  �� $ 
Horizontal=�.+��� $��20��$��20��$��	30,5,10,5q�$/��)/SuperMinersWPF;component/Resources/m.png���� $��Center=�$	��5,0q�
��������� $%�距离总矿池溢出剩余： �� �� ��� *)'AllSystemState.SurplusBuyableStoneCount+$ OneWay=��� -   $��listboxActionMessage��$��1��$��5q�
������ $��Disabled=�$��Stretch=����� *ListPlayerActionLog+���� .R��  �� $
��Auto ���   �� $
��Auto ��� �� $
��Auto �.+��� $��3�� �� StrokeDashArray$	 4,2R����	#FF8D8D8D	 �� X2$	 300���� $��1��$��1��$
��Wrap=�$��20,5,5,5q����� *
LogMessage+$ OneWay=��� $��1��$��2��
��� d �$��5q����� *Time+$ OneWay=�� -   $��gridGetMoneyShowImages��$��1�� �� $
��Auto ��� .+�� ���	#FFAFBCBB$y�	Collapsed=� �� $��30 ��� $��30 �.+��� $��0��$��提现晒图��
�������$
��30,0q�$��Center=��� $��1��$*��$当前系统用300矿主成功提现��
�������$y�	Collapsed=�$
��30,0q�$��Center=��� $��1��$��1��
�������$��1q� �� CornerRadius$ 5��$y�	Collapsed=�._��� -    ��Name$ listChat��$��1��$��5q�.@��� .���� $ Vertical=�.+��� $4��.矿主 小倩 2016/04/04 22:22:22 成功提现��$��Italic$�$��12(�
����  ��� $��	提现图��$	��200��$	��200��$��Center=��� .���� $ Vertical=�.+��� $1��+矿主 abc 2016/04/04 22:22:22 成功提现��$��Italic$�$��12(�
����  ��� $��	提现图��$	��200��$	��200��$��Center=�