   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   =    7SuperMinersCustomServiceSystem.View.EditPlayerRMBWindow   4	MyControl&clr-namespace:SuperMinersWPF.MyControl  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  修改玩家灵币��5   	   $	��300��6   $	��300��6%    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�6G   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5       � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��	玩家：��6   $��5q�6   $��Right=�6*   �� 5   
   -   5        $��txtUserName��5   "   $��1��6    �� 
IsReadOnly . $��5q�6G   �� 5   
   $��1��6   $��0��6!   $��现有灵币：��61   $��5q�6>   $��Right=�6I   �� 5   
   -   5        $��txtCurrentRMB��5   /   $��1��6   $��1��6    . $��5q�6V   �� 5   
   -   5        $��chkChangeType��5       $��2��6   $��5q�65   $��	充值：��6@    d� 	IsChecked$
 TrueY�6N   $��Right=�6_   $��Center=�6{   6�   �� 5   
   -   5        $��panelInCharge��5   2   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�6G   .+�5      -   'SuperMinersWPF.MyControl.NumericTextBox  -   5          Name$ numYuanValue��5   '   -   'SuperMinersWPF.MyControl.NumericTextBox	   ValueChanged$!	 NumericTextBox_ValueChanged��6�   $��5q�6=   $	��100��6H   
   Minimum$
 0Q�6T      Maximum$ 10000Q�6`    � ToolTip$B <充值时输入充入的人民币值，自动换算成灵币��6p   6�   �� 5      $��	人民币��6   $��5q�6#   5      �� 5   
   $��3��6   $��修改后灵币：��6!   $��5q�6/   $��Right=�6:     5   
   -   5        $ txtRMBChanged��5   @   $��3��6#   $��1��60   $
 0Q�6W   $ 200000Q�6c    . $��5q�6�      DecimalPlaces$ 2��6�   �� 5   
   -   5        $��btnOK��5   2   $��4��6   $��2��6   $��确定��6?   $	��100��6L   $��Right=�6X   $��Bottom=�6t   $��	5,5,120,5q�6�   6�   �� 5   
   -   5        $��	btnCancel��5   2   $��4��6   $��2��6   $��取消��6C   $	��100��6P   $��Right=�6\   $��Bottom=�6x   $��5q�6�   6�   5      5      