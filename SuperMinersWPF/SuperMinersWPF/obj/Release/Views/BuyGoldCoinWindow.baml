   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  ,    &SuperMinersWPF.Views.BuyGoldCoinWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5my&clr-namespace:SuperMinersWPF.MyControl     -     � Title$  金币充值��$	��330��$	��300�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�   PlayerInfoBackground#���   
������� � WindowStyle$ 
ToolWindow=�.��� $��5q� � ColumnDefinitions �� $
��Auto �$��70���� $
��174* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $��* ��� $
��Auto �.+��� $��账户余额��$��5q�$��Right=�$��Center=�$��18(��� -   $��txtRMB��$��1�� �� 
IsReadOnly . $��5q�
������ 
�������$��Center=�$��18(��� $��2��$��Center=�$��5q�$��18(����� *Strings.RMB+ �� Mode$ OneWay=�  Language �� Source# [ �� $��1��$��3��$��3�����	#FFC1C4FD		 �� X1$	 0��	
 �� Y1$
 0��	 �� X2$	 300��	 �� Y2$ 0��$��3���� $��2��$��0��$��5q�$��Right=�$��Center=��� $�1�� �� ��� *Strings.RMB+$ OneWay=�# [  �� $�	可兑换���� -   $��txtRMB_GoldCoin��$��2��$��1��$��5q�$��Center=��� $��2��$��2��$��5q����� *Strings.GoldCoin+$ OneWay=�# [ �� $��3��$��3��$��3�����	#FFC1C4FD$	 0��$
 0��$	 300��$ 0��$��3���� $��4��$��0��$��支付��$��5q�$��Right=�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numRechargeRMB��-   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$! numRechargeRMB_ValueChanged��$��4��$��1��$��Center=�$��5q�   Maximum$ 100000Q��� -   $��
txtPayUnit��$��4��$��2��$�� ��$��5q�$��Center=��� $��5��$��0��$��支付方式��$��5q�$	��Top=�$��Right=��� -   $��
cmbPayType��$��5��$��1�� �� SelectedValuePath$	 Key�� �� DisplayMemberPath$ Value��$��Center=�$��Stretch=�$��5q� �� SelectedIndex$ 0���� $��6��$��0��$��	可获取��$��5q�$��Right=�$��Center=��� -   $��txtGainGoldCoin��$��6��$��1�� . $��5q�$��Center=��� $��6��$��2��$��5q�$��Center=����� *Strings.GoldCoin+$ OneWay=�# [ �� -   $��txtError��$��8��$��1��$*��$账户余额不足，请先充值。��$��5q�
���  ��$��Center=�$
��Wrap=�$y�	Collapsed=��� -	   $��btnOK��$��9��$��3��$��确定��$��75��$��	5,5,110,5q�$��Right=�$	��Top=��� -
   $��	btnCancel��$��9��$��3��$��取消��$��75��$��5q�$��Right=�$	��Top=�