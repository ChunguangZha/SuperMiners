   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  >    8SuperMinersCustomServiceSystem.View.FillAlipayInfoWindow   4	MyControl&clr-namespace:SuperMinersWPF.MyControl  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$!  填写支付宝支付信息��$	��350��$	��350�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ 
ToolWindow=� � 
ResizeMode$ NoResize=�.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��买家用户名��$��5q�$��Center=�$��Right=��� -   $��txtBuyerUserName��$��1��$��5q� �� 
IsReadOnly . $��Center=��� $��1��$��交易订单号��$��5q�$��Center=�$��Right=��� -   $��txtMyTradeNo��$��1��$��1�� .  �� 	MaxLength$ 35��$��5q�$��Center=��� $��2��$��支付宝订单号��$��5q�$��Center=�$��Right=��� -   $��txtAlipayTradeNo��$��2��$��1��$ 45��$��5q�$��Center=��� $��3��$��支付账户��$��5q�$��Center=�$��Right=��� -   $��txtAlipayAccount��$��3��$��1��$ 35��$��5q�$��Center=��� $��4��$ ��支付金额(人民币元)��$��5q�$��Center=�$��Right=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numTotalFee��-   'SuperMinersWPF.MyControl.NumericTextBox	   ValueChanged$	 numTotalFee_ValueChanged��$��4��$��1��$��5q�$��Center=�
   DecimalPlaces$
 2��   Maximum$ 10000Q��� $��5��$��价值灵币��$��5q�$��Center=�$��Right=�  -   $ numValueRMB��$��5��$��1��$��5q�$��Center=�$
 2��$ 1000000Q��� $��6��$��支付时间��$��5q�$��Center=�$��Right=�(  "System.Windows.Controls.DatePicker  -   $��	dpPayTime��$��6��$��1��$��5q�$��Center=�   FirstDayOfWeek$ Monday=�   SelectedDateFormat$
 Long=�� $��7��$��1��$��5q� �� �� $
��Auto ��� �� $
��Auto ��� �� $
��Auto �.+�  -   $ numHour��$��Center=�$ 23Q��� $��1��$	��时��$��3q�  -	   $ 	numMinute��$��2��$��Center=�$ 59Q��� $��3��$	��分��$��3q�  -
   $ 	numSecond��$��4��$��Center=�$ 59Q��� $��5��$	��秒��$��3q��� -   $��	btnCancel��$��9��$��2��$��取消��$	��100��$��Right=�$��10q��� -   $��btnOK��$��9��$��2��$��确定��$	��100��$��Right=�$��10,10,120,10q�