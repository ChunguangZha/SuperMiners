   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  S    MSuperMinersCustomServiceSystem.View.Windows.HandleExceptionAlipayRecordWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    ;
MyControl1&clr-namespace:SuperMinersWPF.MyControl    J	MyControl6clr-namespace:SuperMinersCustomServiceSystem.MyControl    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$*  $处理异常的支付宝支付订单��$	��350��$	��500�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��42* �.+��� $��商品订单号：��$��5q�$��Right=��� -   $��txtOrderNumber��$��1��$��5q��� $��1��$��订单类型：��$��5q�$��Right=��� -   $��txtOrderType��$��1��$��1�� �� 
IsReadOnly . $��5q��� $��2��$��支付宝订单号：��$��5q�$��Right=��� -   $��txtAlipayOrderNumber��$��2��$��1��$��5q��� $��3��$��支付账户：��$��5q�$��Right=��� -   $��txtBuyerEmail��$��3��$��1��$��5q��� $��4��$��玩家用户名：��$��5q�$��Right=��� -   $��txtBuyerUserName��$��4��$��1��$��5q��� $��5��$��充值人民币：��$��5q�$��Right=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numTotalFee��-   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$ numTotalFee_ValueChanged��$��5��$��1��   Minimum$ 0Q�	   Maximum$	 100000Q�$��5q�
   DecimalPlaces$
 2���� $��6��$��价值灵币：��$��5q�$��Right=�  -   $ numValueRMB��$��6��$��1�� . $��5q�$	 10000000Q�$ 0Q�$
 2���� $��7��$��付款时间：��$��5q�$��Right=�F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  -     Name$ mydpPayTime��$��7��$��1��$��5q��� -	   $��	btnCancel��$��9��$��2��$��取消处理��$	��100��$��10q�$��Center=�$��Right=��� -
   $��btnOK��$��9��$��2��$��认同支付��$	��100��$��10,10,120,10q�$��Center=�$��Right=�