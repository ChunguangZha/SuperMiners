   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5   	   S    MSuperMinersCustomServiceSystem.View.Windows.HandleExceptionAlipayRecordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   ;
MyControl1&clr-namespace:SuperMinersWPF.MyControl    5   	   J	MyControl6clr-namespace:SuperMinersCustomServiceSystem.MyControl    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$*  $处理异常的支付宝支付订单��5   	   $	��350��6   $	��500��6+    � 
ResizeMode$ NoResize=�67    � WindowStartupLocation$ CenterScreen=�6M   .��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��42* �6   .+�5   
   �� $��商品订单号：��6   $��5q�6"   $��Right=�6-   �� 5   
   -   5        $��txtOrderNumber��5   "   $��1��6   $��5q�68   6C   �� 5   
   $��1��6   $��订单类型：��6!   $��5q�6.   $��Right=�69   �� 5   
   -   5        $��txtOrderType��5   /   $��1��6   $��1��6    �� 
IsReadOnly . $��5q�6U   �� 5   
   $��2��6   $��支付宝订单号：��6!   $��5q�60   $��Right=�6;   �� 5   
   -   5        $��txtAlipayOrderNumber��5   /   $��2��6   $��1��6   $��5q�6K   �� 5   
   $��3��6   $��支付账户：��6!   $��5q�6.   $��Right=�69   �� 5   
   -   5        $��txtBuyerEmail��5   /   $��3��6   $��1��6   $��5q�6D   �� 5    
   $��4��6   $��玩家用户名：��6!   $��5q�6/   $��Right=�6:   �� 5!   
   -   5        $��txtBuyerUserName��5!   /   $��4��6   $��1��6   $��5q�6G   �� 5"   
   $��5��6   $��充值人民币：��6!   $��5q�6/   $��Right=�6:   -   'SuperMinersWPF.MyControl.NumericTextBox  5#   
   -   5          Name$ numTotalFee��5#   A   -   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$ numTotalFee_ValueChanged��6~   $��5��6$   $��1��61      Minimum$ 0Q�6V   	   Maximum$	 100000Q�6b   $��5q�6s   
   DecimalPlaces$
 2��6�   �� 5$   
   $��6��6   $��价值灵币：��6!   $��5q�6.   $��Right=�69     5%   
   -   5        $ numValueRMB��5%   A   $��6��6$   $��1��61    . $��5q�6h   $	 10000000Q�6s   $ 0Q�6�   $
 2��6�   �� 5&   
   $��7��6   $��付款时间：��6!   $��5q�6.   $��Right=�69   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5'   
   -   5          Name$ mydpPayTime��5'   I   $��7��6,   $��1��69   $��5q�6^   �� 5(   
   -	   5        $��	btnCancel��5(   2   $��9��6   $��2��6   $��取消处理��6C   $	��100��6R   $��10q�6^   $��Center=�6j   $��Right=�6�   6�   �� 5)   
   -
   5        $��btnOK��5)   2   $��9��6   $��2��6   $��认同支付��6?   $	��100��6N   $��10,10,120,10q�6Z   $��Center=�6p   $��Right=�6�   6�   5*      5+      