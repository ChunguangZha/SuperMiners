   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5   	   U    OSuperMinersCustomServiceSystem.View.Windows.EditPlayerRemoteServiceHandleWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   L	myControl6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5   	     � Title$)  #EditPlayerRemoteServiceHandleWindow��5   	   $	��300��65   $	��400��6B    � WindowStyle$ 
ToolWindow=�6N    � WindowStartupLocation$ CenterScreen=�6g   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� $��玩家用户名：��6   $��10q�6"   $��Center=�6.   $��Right=�6I   �� 5   
   -   5        $��txtPlayerUserName��5   /   $��0��6   $��1��6    �� 
IsReadOnly . $��10q�6Z   $��Center=�6f    �� 	MaxLength$	 150��6�   �� 5   
   $��1��6   $��0��6!   $��服务工程师：��61   $��10q�6?   $��Center=�6K   $��Right=�6f   �� 5   
   -   5        $��txtWorkerName��5   /   $��1��6   $��1��6   $��10q�6D   $��Center=�6P   $	 150��6k   �� 5   
   $��2��6   $��0��6!   $��服务时间：��61   $��10q�6>   $��Center=�6J   $��Right=�6e   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5   
   -   5          Name$ myTimeServiceTime��5   I   $��2��6,   $��1��69   $��10q�6d   $��Center=�6p   �� 5   
   $��3��6   $��0��6!   $��服务内容：��61   $��10q�6>   $	��Top=�6J   $��Right=�6b   �� 5   
   -   5        $��txtServiceContent��5   /   $��3��6   $��1��6   $��10q�6H    �� TextWrapping$
 Wrap=�6T   	 �� MaxLines$	 10000��6h   
 �� AcceptsReturn
 .  �� 
AcceptsTab . 6�   �� 5   
   -   5        $��	btnCancel��5   2   $��4��6   $��2��6   $��取消��6C   $	��100��6P   $��10q�6\   $��Right=�6h   6�   �� 5   
   -   5        $��btnOK��5   2   $��4��6   $��2��6   $��确定��6?   $	��100��6L   $��10,10,130,10q�6X   $��Right=�6n   6�   5      5      