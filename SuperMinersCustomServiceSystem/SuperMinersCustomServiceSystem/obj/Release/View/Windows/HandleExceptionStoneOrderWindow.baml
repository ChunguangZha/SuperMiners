   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemQ    KSuperMinersCustomServiceSystem.View.Windows.HandleExceptionStoneOrderWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$!  处理异常的矿石订单��$	��400��$	��400�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��42* �.+��� $��订单号：��$��5q�$��Right=��� -   $��txtOrderNumber��$��1�� �� 
IsReadOnly . $��5q����� *OrderNumber+ �� Mode$ OneWay=��� $��1��$��卖家用户名：��$��5q�$��Right=��� -   $��txtSellUserName��$��1��$��1�� . $��5q����� *SellerUserName+$ OneWay=��� $��2��$��交易矿石数：��$��5q�$��Right=��� -   $��txtSellStoneCount��$��2��$��1�� . $��5q����� *SellStonesCount+$ OneWay=��� $��3��$��价值灵币：��$��5q�$��Right=��� -   $��txtValueRMB��$��3��$��1�� . $��5q����� *
ValueRMB+$ OneWay=��� $��4��$��手续费：��$��5q�$��Right=��� -   $��
txtExpense��$��4��$��1�� . $��5q����� *	Expense+$ OneWay=��� $��5��$��出售时间：��$��5q�$��Right=��� -   $��txtSellTime��$��5��$��1�� . $��5q����� *
SellTime+$ OneWay=��� $��6��$��订单状态：��$��5q�$��Right=��� -   $��txtOrderStateText��$��6��$��1�� . $��5q����� *OrderStateText+$ OneWay=��� $��7��$��买家用户名：��$��5q�$��Right=��� -	   $��txtBuyerUserName��$��7��$��1�� . $��5q����� *LockedByUserName+$ OneWay=��� $��8��$��锁定时间：��$��5q�$��Right=��� -
   $��txtLockTime��$��8��$��1�� . $��5q����� *
LockedTime+$ OneWay=��� -   $��	btnReject��$��10��$��2��$	��100��$��10q�$��Center=�$��Right=����� $��没有付款驳回订单��$
��Wrap=�$��Center=� �� Padding$
 20,0q� �� TextAlignment$ Center=��� -   $��btnCreateAlipay��$��10��$��2��$	��100��$��10,10,120,10q�$��Center=�$��Right=����� $!��新建支付宝付款记录��$
��Wrap=�$��Center=�$
 15,0q�$ Center=��� -   $��btnSearchAlipay��$��10��$��2��$	��100��$��10,10,230,10q�$��Center=�$��Right=����� $!��搜索支付宝付款记录��$
��Wrap=�$��Center=�$
 15,0q�$ Center=�