   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   >    8SuperMinersCustomServiceSystem.View.FillAlipayInfoWindow   4	MyControl&clr-namespace:SuperMinersWPF.MyControl  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  填写支付宝支付信息��5   	   $	��350��6   $	��350��6(    � WindowStartupLocation$ CenterScreen=�64    � WindowStyle$ 
ToolWindow=�6Y    � 
ResizeMode$ NoResize=�6r   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��买家用户名��6   $��5q�6!   $��Center=�6,   $��Right=�6G   �� 5   
   -   5        $��txtBuyerUserName��5   "   $��1��6   $��5q�6:    �� 
IsReadOnly . $��Center=�6W   �� 5   
   $��1��6   $��交易订单号��6!   $��5q�6.   $��Center=�69   $��Right=�6T   �� 5   
   -   5        $��txtMyTradeNo��5   /   $��1��6   $��1��6    .  �� 	MaxLength$ 35��6U   $��5q�6d   $��Center=�6o   �� 5   
   $��2��6   $��支付宝订单号��6!   $��5q�6/   $��Center=�6:   $��Right=�6U   �� 5   
   -   5        $��txtAlipayTradeNo��5   /   $��2��6   $��1��6   $ 45��6G   $��5q�6V   $��Center=�6a   �� 5   
   $��3��6   $��支付账户��6!   $��5q�6-   $��Center=�68   $��Right=�6S   �� 5   
   -   5        $��txtAlipayAccount��5   /   $��3��6   $��1��6   $ 35��6G   $��5q�6V   $��Center=�6a   �� 5   
   $��4��6   $ ��支付金额(人民币元)��6!   $��5q�63   $��Center=�6>   $��Right=�6Y   -   'SuperMinersWPF.MyControl.NumericTextBox  5   
   -   5          Name$ numTotalFee��5   @   -   'SuperMinersWPF.MyControl.NumericTextBox	   ValueChanged$	 numTotalFee_ValueChanged��6�   $��4��6#   $��1��60   $��5q�6U   $��Center=�6`   
   DecimalPlaces$
 2��6{      Maximum$ 10000Q�6�   6�   �� 5    
   $��5��6   $��价值灵币��6!   $��5q�6-   $��Center=�68   $��Right=�6S     5!   
   -   5        $ numValueRMB��5!   @   $��5��6#   $��1��60   $��5q�6U   $��Center=�6`   $
 2��6{   $ 1000000Q�6�   �� 5"   
   $��6��6   $��支付时间��6!   $��5q�6-   $��Center=�68   $��Right=�6S   (  "System.Windows.Controls.DatePicker  5#   
   -   5        $��	dpPayTime��5#   2   $��6��6   $��1��6"   $��5q�6C   $��Center=�6N      FirstDayOfWeek$ Monday=�6i      SelectedDateFormat$
 Long=�6�   � 5$   
   $��7��6   $��1��6   $��5q�6,    5%      �� 5&      �� 5'      $
��Auto �6#   �� 5(      �� 5)      $
��Auto �6#   �� 5*      �� 5+      $
��Auto �6#   .+�5-        -   5        $ numHour��5-   '   $��Center=�68   $ 23Q�6S   �� 5.      $��1��6   $	��时��6(   $��3q�61     5/      -	   5        $ 	numMinute��5/   7   $��2��6'   $��Center=�6J   $ 59Q�6e   �� 50      $��3��6   $	��分��6(   $��3q�61     51      -
   5        $ 	numSecond��51   7   $��4��6'   $��Center=�6J   $ 59Q�6e   �� 52      $��5��6   $	��秒��6(   $��3q�61   53      �� 54   
   -   5        $��	btnCancel��54   2   $��9��6   $��2��6   $��取消��6C   $	��100��6P   $��Right=�6\   $��10q�6x   6�   �� 55   
   -   5        $��btnOK��55   2   $��9��6   $��2��6   $��确定��6?   $	��100��6L   $��Right=�6X   $��10,10,120,10q�6t   6�   56      57      