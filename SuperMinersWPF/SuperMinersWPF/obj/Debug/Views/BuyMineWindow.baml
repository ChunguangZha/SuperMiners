   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   (    "SuperMinersWPF.Views.BuyMineWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5my&clr-namespace:SuperMinersWPF.MyControl     5   	   -   5          � Title$  勘探新矿山��5   	   $	��300��6   $	��300��6$    � 
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
   -   5        $��txtRMB��5   "   $��1��6   
������ 
������� �� 
IsReadOnly . $��5q�6n   $��Center=�6y   $��18(�6�   �� 5   
   $��2��6   $��Center=�6p   $��5q�6�   $��18(�6�   ��6$   �� *Strings.RMB+ �� Mode$ OneWay=�  Language �� Source# [ 6�   �� 5   
   $��1��6   $��3��6   $��3��60   ���	#FFC1C4FD		 �� X1$	 0��6N   	
 �� Y1$
 0��6U   	 �� X2$	 300��6\   	 �� Y2$ 0��6e   $��3��6l   �� 5   
   $��2��6   $��0��6!   $��需要支付��61   $��18(�6=   $��5q�6K   $��Right=�6V   $��Center=�6r   �� 5   
   -   5        $��txtRMB_Mine��5   1   $��2��6   $��1��6!   $��18(�6D   $��5q�6R   $��Center=�6]   �� 5    
   -   5        $��
txtPayUnit��5    1   $��2��6   $��2��6!   $��18(�6�   $��5q�6�   ��6C   �� *Strings.RMB+$ OneWay=�# [ 6�   �� 5!   
   $��5��6   $��0��6!   $��支付方式��61   $
��10,5q�6=   $	��Top=�6K   $��Right=�6c   �� 5"   
   -   5        $��
cmbPayType��5"   0   $��5��6   $��1��6    $��Center=�6B   $��Stretch=�6]   $
��10,5q�6{    �� SelectedIndex$ 0��6�   .@�5#      �� $��灵币��6   �� 5$      $��积分��6   5%      �� 5'   
   -   5        $��txtError��5'   2   $��7��6   $��1��6!   $*��$账户余额不足，请先充值。��6B   $��5q�6V   
���  ��$��Center=�6r   $y�	Collapsed=�6�   �� 5(   
   -   5        $��btnOK��5(   2   $��8��6   $��3��6   $��确定��6?   $��75��6L   $��	5,5,110,5q�6W   $��Right=�6j   $	��Top=�6�   6�   �� 5)   
   -   5        $��	btnCancel��5)   2   $��8��6   $��3��6   $��取消��6C   $��75��6P   $��5q�6[   $��Right=�6f   $	��Top=�6�   6�   5*      5+      