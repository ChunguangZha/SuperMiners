   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   4    .SuperMinersWPF.Views.Windows.WithdrawRMBWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5my&clr-namespace:SuperMinersWPF.MyControl     5   	   -   5          � Title$  灵币提现��5   	   $	��250��6   $	��300��6#    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6      PlayerInfoBackground#���   6D   
������� � WindowStyle$ 
ToolWindow=�6�   .��5	      � $��5q�6    � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   $��70��6,   �� 5      $
��174* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��账户余额��6   $��5q�6    $��Right=�6+   $��Center=�6G   $��18(�6b   �� 5   
   -   5        $��txtRMB��5   "   $��1��6    �� 
IsReadOnly . $��5q�6B   
������ 
�������$��Center=�6y   $��18(�6�   �� 5   
   $��2��6   $��Center=�6p   $��5q�6�   $��18(�6�   ��6$   �� *Strings.RMB+ �� Mode$ OneWay=�  Language �� Source# [ 6�   �� 5   
   $��1��6   $��3��6   $��3��60   ���	#FFC1C4FD		 �� X1$	 0��6N   	
 �� Y1$
 0��6U   	 �� X2$	 300��6\   	 �� Y2$ 0��6e   $��3��6l   �� 5   
   $��2��6   $��0��6!   $��5q�61   $��Right=�6<   $��Center=�6X   �� 5      $�1��6    �� 5       $	�元��6    �� 5!      $�=��6   5"      �� 5#   
   -   5        $��txtYuan_RMB��5#   1   $��2��6   $��1��6!   $��5q�6D   $��Center=�6O   �� 5$   
   $��2��6   $��2��6!   $��5q�6}   ��61   �� *Strings.RMB+$ OneWay=�# [ 6}   �� 5%   
   $��3��6   $��3��6   $��3��60   ���	#FFC1C4FD$	 0��6N   $
 0��6U   $	 300��6\   $ 0��6e   $��3��6l   �� 5&   
   $��4��6   $��0��6!   $��提现��61   $��5q�6;   $��Right=�6F   $��Center=�6b   -   'SuperMinersWPF.MyControl.NumericTextBox  5'   
   -   5          Name$ numWithdrawRMB��5'   9   -   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$! numRechargeRMB_ValueChanged��6w   $��4��6   $��1��6)   $��Center=�6Q   $��5q�6l      Maximum$ 100000Q�6�   �� 5(   
   $��4��6   $��2��6!   $��5q�6}   $��Center=�6�   ��61   �� *Strings.RMB+$ OneWay=�# [ 6�   �� 5)   
   $y�	Collapsed=�6   $��6��6+   $��0��68   $��	可获得��6H   $��5q�6S   $��Right=�6^   $��Center=�6z     5*   
   -   5        $ numGainYuan��5*   P   $y�	Collapsed=�6   $��6��63   $��1��6@    . $��5q�6w   $��Center=�6�   $ 1000000Q�6�      DecimalPlaces$ 2��6�   �� 5+   
   $y�	Collapsed=�6   $��6��6+   $��2��68   $��元 人民币��6H   $��5q�6U   $��Center=�6`   �� 5-   
   -   5        $��btnOK��5-   2   $��8��6   $��3��6   $��确定��6?   $��75��6L   $��	5,5,110,5q�6W   $��Right=�6j   $	��Top=�6�   6�   �� 5.   
   -   5        $��	btnCancel��5.   2   $��8��6   $��3��6   $��取消��6C   $��75��6P   $��5q�6[   $��Right=�6f   $	��Top=�6�   6�   5/      50      