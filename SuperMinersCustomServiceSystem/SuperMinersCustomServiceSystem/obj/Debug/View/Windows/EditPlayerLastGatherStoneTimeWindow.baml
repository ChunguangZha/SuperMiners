   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5   	   U    OSuperMinersCustomServiceSystem.View.Windows.EditPlayerLastGatherStoneTimeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   J	mycontrol6clr-namespace:SuperMinersCustomServiceSystem.MyControl    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$-  '修改玩家上一次收取矿石时间��5   	   $	��200��6   $	��350��6,    � WindowStyle$ 
ToolWindow=�69    � WindowStartupLocation$ CenterScreen=�6R   .��5      �  � ColumnDefinitions 5   
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
   $��1��6   $��0��6!   $��原时间：��61   $��5q�6=   $��Right=�6H   �� 5   
   -   5        $��txtCurrentTime��5   /   $��1��6   $��1��6    . $��5q�6W   �� 5   
   $��2��6   $��修改后时间：��6!   $��5q�6/   $��Right=�6:   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5   
   -   5          Name$ numChangedTime��5   I   $��2��6,   $��1��69   $��5q�6a   �� 5   
   -   5        $��btnOK��5   2   $��4��6   $��2��6   $��确定��6?   $	��100��6L   $��Right=�6X   $��Bottom=�6t   $��	5,5,120,5q�6�   6�   �� 5   
   -   5        $��	btnCancel��5   2   $��4��6   $��2��6   $��取消��6C   $	��100��6P   $��Right=�6\   $��Bottom=�6x   $��5q�6�   6�   5      5      