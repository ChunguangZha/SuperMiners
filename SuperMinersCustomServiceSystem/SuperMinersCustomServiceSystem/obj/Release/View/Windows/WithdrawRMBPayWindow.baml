   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemF    @SuperMinersCustomServiceSystem.View.Windows.WithdrawRMBPayWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  灵币提现��$	��350��$	��350�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $	��100 ��� $
��181* �.+��� $��ID��$
��10,5q�$��Right=�$��Center=��� $��1�� �� 
IsReadOnly . $
��10,5q�$��Center=����� *ID+ �� Mode$ OneWay=��� $��1��$��玩家用户名��$
��10,5q�$��Right=�$��Center=��� $��1��$��1�� . $
��10,5q�$��Center=����� *PlayerUserName+$ OneWay=��� $��2��$��支付宝账户��$
��10,5q�$��Right=�$��Center=��� $��2��$��1�� . $
��10,5q�$��Center=����� *AlipayAccount+$ OneWay=� �� StringFormat$ f2���� $��3��$��支付宝实名��$
��10,5q�$��Right=�$��Center=��� $��3��$��1�� . $
��10,5q�$��Center=����� *AlipayRealName+$ OneWay=�$ f2���� $��4��$��提现灵币��$
��10,5q�$��Right=�$��Center=��� $��4��$��1�� . $
��10,5q�$��Center=����� *WidthdrawRMB+$ OneWay=�$ f2���� $��5��$��价值人民币��$
��10,5q�$��Right=�$��Center=��� $��5��$��1�� . $
��10,5q�$��Center=����� *	ValueYuan+$ OneWay=��� $��6��$��	管理员��$
��10,5q�$��Right=�$��Center=��� -   $��txtAdminUserName��$��6��$��1�� . $
��10,5q�$��Center=��� $��7��$��支付宝单号��$
��10,5q�$��Right=�$��Center=��� -   $��txtAlipayOrderNumber��$��7��$��1�� �� 	MaxLength$ 45��$
��10,5q�$��Center=��� -   $��	btnCancel��$��9��$��2��$��取消��$	��100��$��10q�$��Right=��� -   $��btnOK��$��9��$��2��$��确定��$	��100��$��10,10,120,10q�$��Right=�