   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  4    .SuperMinersWPF.Views.Windows.WithdrawRMBWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5my&clr-namespace:SuperMinersWPF.MyControl     -     � Title$  灵币提现��$	��250��$	��300�� � 
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
��Auto ��� $��* ��� $
��Auto �.+��� $��账户余额��$��5q�$��Right=�$��Center=�$��18(��� -   $��txtRMB��$��1�� �� 
IsReadOnly . $��5q�
������ 
�������$��Center=�$��18(��� $��2��$��Center=�$��5q�$��18(����� *Strings.RMB+ �� Mode$ OneWay=�  Language �� Source# [ �� $��1��$��3��$��3�����	#FFC1C4FD		 �� X1$	 0��	
 �� Y1$
 0��	 �� X2$	 300��	 �� Y2$ 0��$��3���� $��2��$��0��$��5q�$��Right=�$��Center=��� $�1�� �� $	�元�� �� $�=���� -   $��txtYuan_RMB��$��2��$��1��$��5q�$��Center=��� $��2��$��2��$��5q����� *Strings.RMB+$ OneWay=�# [ �� $��3��$��3��$��3�����	#FFC1C4FD$	 0��$
 0��$	 300��$ 0��$��3���� $��4��$��0��$��提现��$��5q�$��Right=�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numWithdrawRMB��-   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$! numRechargeRMB_ValueChanged��$��4��$��1��$��Center=�$��5q�   Maximum$ 100000Q��� $��4��$��2��$��5q�$��Center=����� *Strings.RMB+$ OneWay=�# [ �� $y�	Collapsed=�$��6��$��0��$��	可获得��$��5q�$��Right=�$��Center=�  -   $ numGainYuan��$y�	Collapsed=�$��6��$��1�� . $��5q�$��Center=�$ 1000000Q�   DecimalPlaces$ 2���� $y�	Collapsed=�$��6��$��2��$��元 人民币��$��5q�$��Center=��� -   $��btnOK��$��8��$��3��$��确定��$��75��$��	5,5,110,5q�$��Right=�$	��Top=��� -   $��	btnCancel��$��8��$��3��$��取消��$��75��$��5q�$��Right=�$	��Top=�