   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  )    #SuperMinersWPF.Views.BuyMinerWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5my&clr-namespace:SuperMinersWPF.MyControl     -     � Title$  购买矿工��$	��350��$	��300�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�   PlayerInfoBackground#���   
������� � WindowStyle$ 
ToolWindow=�.��� $��5q� � ColumnDefinitions �� $
��Auto �$��70���� $��* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��2��$��账户余额��$��5q�$��Right=�$��Center=�$��18(��� -   $��txtRMB��$��1�� �� 
IsReadOnly . $��5q�$��Center=�$��18(��� $��2��$��Center=�$��5q�$��18(����� *Strings.RMB+ �� Mode$ OneWay=�  Language �� Source# [ �� -   $��txtGoldCoin��$��1��$��1�� . $��5q�$��Center=�$��18(��� $��1��$��2��$��Center=�$��5q�$��18(����� *Strings.GoldCoin+$ OneWay=�# [ �� $��2��$��3��$��3�����	#FFC1C4FD		 �� X1$	 0��	
 �� Y1$
 0��	 �� X2$	 300��	 �� Y2$ 0��$��3���� $��3��$��5q�$��Right=��� ��� *Strings.Miner+$ OneWay=�# [  �� $�价值���� -   $��txtGoldCoin_Miner��$��3��$��1��$��5q�$��Center=��� $��3��$��2��$��5q����� *Strings.GoldCoin+$ OneWay=�# [ �� $��4��$��3��$��3�����	#FFC1C4FD$	 0��$
 0��$	 300��$ 0��$��3���� $��5��$��购买��$��5q�$��Right=�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numMinersCount��-   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$! numMinersCount_ValueChanged��$��5��$��1��$��5q�$��Center=�   Maximum$
 5000Q��� $��5��$��2��$��5q�$��Center=����� *Strings.Miner+$ OneWay=�# [ �� $��6��$��需要��$��5q�$��Right=�$��Center=��� -   $��txtNeedMoney��$��6��$��1�� . $��5q�$��Center=��� $��6��$��2��$��5q�$��Center=����� *Strings.GoldCoin+$ OneWay=�# [ �� $��7��$��提示��$��5q�$��Right=�$y�	Collapsed=��� $��7��$��1��$��5q�$y�	Collapsed=�$
��Wrap=��� $�当账户中没有足够�� �� ��� *Strings.GoldCoin+$ OneWay=�# [  �� $�时，系统自动将�� �� ��� *Strings.RMB+$ OneWay=�# [  �� $�	转换成�� �� ��� *Strings.GoldCoin+$ OneWay=�# [ �� -   $��txtError��$��8��$��1��$*��$账户余额不足，请先充值。��$��Center=�$��5q�
���  ��$y�	Collapsed=��� -   $��btnOK��$��9��$��3��$��确定��$��75��$��	5,5,110,5q�$��Right=�$	��Top=��� -	   $��	btnCancel��$��9��$��3��$��取消��$��75��$��5q�$��Right=�$	��Top=�