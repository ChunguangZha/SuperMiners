   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   J    DSuperMinersCustomServiceSystem.View.Windows.EditPlayerGoldCoinWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   :	MyControl&clr-namespace:SuperMinersWPF.MyControl    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  修改玩家金币��5   	   $	��300��6   $	��300��6%    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�6G   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��	玩家：��6   $��5q�6   $��Right=�6*   �� 5   
   -   5        $��txtUserName��5   "   $��1��6    �� 
IsReadOnly . $��5q�6G   �� 5   
   $��1��6   $��0��6!   $��现有金币：��61   $��5q�6>   $��Right=�6I   �� 5   
   -   5        $��txtCurrentGoldCoin��5   /   $��1��6   $��1��6    . $��5q�6[   �� 5   
   -   5        $��cmbOper��5       $��2��6   $
��10,5q�6/   $��Center=�6=    �� SelectedIndex$ 0��6X   $��Center=�6j   .@�5      �� $��增加��6   �� 5      $��减少��6   5      �� 5   
   -   5        $��panelInCharge��5   2   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�6G   .+�5      -   'SuperMinersWPF.MyControl.NumericTextBox  -   5          Name$ numGoldCoinValue��5   '   -   'SuperMinersWPF.MyControl.NumericTextBox	   ValueChanged$#	 numGoldCoinValue_ValueChanged��6x   $��5q�6A   $	��100��6L   
   Minimum$
 0Q�6X      Maximum$ 	100000000Q�6d   6x   �� 5      $��金币��6   $��5q�6"   5      �� 5   
   $��3��6   $��修改后金币：��6!   $��5q�6/   $��Right=�6:     5   
   -   5        $ numGoldCoinChanged��5   @   $��3��6#   $��1��60   $
 0Q�6\   $ 
1000000000Q�6h    . $��5q�6�   �� 5    
   -   5        $��btnOK��5    2   $��4��6   $��2��6   $��确定��6?   $	��100��6L   $��Right=�6X   $��Bottom=�6t   $��	5,5,120,5q�6�   6�   �� 5!   
   -   5        $��	btnCancel��5!   2   $��4��6   $��2��6   $��取消��6C   $	��100��6P   $��Right=�6\   $��Bottom=�6x   $��5q�6�   6�   5"      5#      