   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  (    "SuperMinersWPF.Views.BuyMineWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5my&clr-namespace:SuperMinersWPF.MyControl     -     � Title$  勘探新矿山��$	��300��$	��300�� � 
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
��Auto �.+��� $��账户余额��$��5q�$��Right=�$��Center=�$��18(��� -   $��txtRMB��$��1��
������ 
������� �� 
IsReadOnly . $��5q�$��Center=�$��18(��� $��2��$��Center=�$��5q�$��18(����� *Strings.RMB+ �� Mode$ OneWay=�  Language �� Source# [ �� $��1��$��3��$��3�����	#FFC1C4FD		 �� X1$	 0��	
 �� Y1$
 0��	 �� X2$	 300��	 �� Y2$ 0��$��3���� $��2��$��0��$��需要支付��$��18(�$��5q�$��Right=�$��Center=��� -   $��txtRMB_Mine��$��2��$��1��$��18(�$��5q�$��Center=��� -   $��
txtPayUnit��$��2��$��2��$��18(�$��5q����� *Strings.RMB+$ OneWay=�# [ �� $��5��$��0��$��支付方式��$
��10,5q�$	��Top=�$��Right=��� -   $��
cmbPayType��$��5��$��1��$��Center=�$��Stretch=�$
��10,5q� �� SelectedIndex$ 0��.@��� $��灵币���� $��积分���� -   $��txtError��$��7��$��1��$*��$账户余额不足，请先充值。��$��5q�
���  ��$��Center=�$y�	Collapsed=��� -   $��btnOK��$��8��$��3��$��确定��$��75��$��	5,5,110,5q�$��Right=�$	��Top=��� -   $��	btnCancel��$��8��$��3��$��取消��$��75��$��5q�$��Right=�$	��Top=�