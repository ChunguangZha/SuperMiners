   M S B A M L   `   `   `  ����  SuperMinersWPF2    ,SuperMinersWPF.Views.DigStoneAreaUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        .��5	      � ���	#FFF0F0F0  � ColumnDefinitions  5
   
   �� 5      �� 5      $
��Auto �6   .+�5   
   � -   5        $��gridActionMessage��5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      .+�5      �� ���	#FFAFBCBB �� Orientation$ 
Horizontal=�60   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��20��6   $��20��6'   $��	30,5,10,5q�63   $4��./SuperMinersWPF;component/Resources/player.png��6F   �� 5      $	��5,0q�6    $��Center=�6-   
��������� 5   "   $	�共��6&    �� 5   "   �6&   �� *AllSystemState.AllPlayerCount+ �� Mode$ OneWay=� �� 5   "   $�矿主��6&    �� 5   "   �6&   �� *AllSystemState.AllMinersCount+$ OneWay=� �� 5   "   �6&   �� *Strings.Miner+$ OneWay=�   Language �� Source# [  5      5      �� 5&      $ 
Horizontal=�6   .+�5'      �� $��20��6   $��20��6'   $��	30,5,10,5q�63   $/��)/SuperMinersWPF;component/Resources/m.png��6F   �� 5(      $��Center=�6    $	��5,0q�6;   
��������� 5)   "   $%�距离总矿池溢出剩余： ��6&    �� 5*   "   �6&   �� *)'AllSystemState.SurplusBuyableStoneCount+$ OneWay=�5+      5,      5-      �� 5.      -   5        $��listboxActionMessage��5.      $��1��62   $��5q�6?   
������ $��Disabled=�6�   $��Stretch=�6�   ��6c   �� *ListPlayerActionLog+��5/      �� 50      .R�51      �  52      �� 53   "   $
��Auto �60   �� 54   "     56      �� 57   "   $
��Auto �63   �� 58   "   �� 59   "   $
��Auto �63   .+�5;      �� $��3��6#    �� StrokeDashArray$	 4,2R�67   ���	#FF8D8D8D	 �� X2$	 300��6^   �� 5<      $��1��6(   $��1��65   $
��Wrap=�6n   $��20,5,5,5q�6�   ��6E   �� *
LogMessage+$ OneWay=�6�   �� 5=      $��1��6(   $��2��65   
��� d �$��5q�6   ��6E   �� *Time+$ OneWay=�6   5>      5?      5A      5B      � 5C   
   -   5        $��gridGetMoneyShowImages��5C      $��1��6    5D      �� 5E      $
��Auto �6    �� 5F      .+�5H      � ���	#FFAFBCBB$y�	Collapsed=�6*    5I      �� 5J      $��30 �6$   �� 5K      $��30 �6$   .+�5M      �� $��0��6   $��提现晒图��6)   
�������$
��30,0q�6H   $��Center=�6V   �� 5N      $��1��6   $*��$当前系统用300矿主成功提现��6)   
�������$y�	Collapsed=�6R   $
��30,0q�6i   $��Center=�6w   5O      �� 5P      $��1��6   $��1��6"   
�������$��1q�6E    �� CornerRadius$ 5��6Y   $y�	Collapsed=�6j   ._�5Q      �� -   5         ��Name$ listChat��5Q   '   $��1��6   $��5q�69   .@�5R      �� .��5S      �� $ Vertical=�6%   .+�5T      �� $4��.矿主 小倩 2016/04/04 22:22:22 成功提现��6(   $��Italic$�6N   $��12(�6a   
����  �6o   �� 5U      $��	提现图��6%   $	��200��63   $	��200��6?   $��Center=�6L   5V      5W      �� 5X      .��5Y      �� $ Vertical=�6%   .+�5Z      �� $1��+矿主 abc 2016/04/04 22:22:22 成功提现��6(   $��Italic$�6O   $��12(�6b   
����  �6p   �� 5[      $��	提现图��6%   $	��200��63   $	��200��6?   $��Center=�6L   5\      5]      5^      5_      5`      5a      5b      