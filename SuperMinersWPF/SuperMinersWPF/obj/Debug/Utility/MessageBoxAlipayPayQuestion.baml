   M S B A M L   `   `   `  ����  SuperMinersWPF8    2SuperMinersWPF.Utility.MessageBoxAlipayPayQuestion   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	请确认��5   	   $	��100��6   $	��500��6"    � WindowStyle$ 
ToolWindow=�6.    � 
ResizeMode$ NoResize=�6G    � WindowStartupLocation$ CenterScreen=�6]   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��
btnSucceed��5      $��1��6   $��支付成功��60   $��5q�6?   $��10q�6K   $��Center=�6W   6r   �� 5   
   -   5        $��	btnFailed��5   .   $��1��6   $��1��6   $!��支付失败，重新支付��6?   $��5q�6S   $��10q�6_   $��Center=�6k   6�   �� 5   
   -   5        $��	btnCancel��5   .   $��1��6   $��2��6   $��取消支付��6?   $��5q�6N   $��10q�6Z   $��Center=�6f   6�   5      5      