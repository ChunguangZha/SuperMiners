   M S B A M L   `   `   `  ����   SuperMinersWPF*    $SuperMinersWPF.Views.BuyStonesWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  购买矿石��$	��300��$	��400�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�   PlayerInfoBackground#���   
������� � WindowStyle$ 
ToolWindow=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $
��Auto �$	��100���� $
��399* �.+��� $��	订单号��$
��10,5q�$��16(�$
��Bold"�$��Right=��� $��1��$��16(�$
��10,5q�$��Center=����� *OrderNumber+�� $��1��$��0��$��卖方��$
��10,5q�$��Center=�$��Right=��� $��1��$��1��$
��10,5q�$��Center=����� *SellerUserName+�� $��2��$��0��$��挂单时间��$
��10,5q�$��Center=�$��Right=��� $��2��$��1��$
��10,5q�$��Center=����� *
SellTime+�� $��3��$��0��$��出售矿石��$
��10,5q�$��Center=�$��Right=��� $��3��$��1��$
��10,5q�$��Center=����� *SellStonesCount+�� $��4��$��0��$��支付灵币��$
��10,5q�$��Center=�$��Right=��� $��4��$��1��$
��10,5q�$��Center=����� *
ValueRMB+�� $��5��$��0��$��奖励金币��$
��10,5q�$��Center=�$��Right=��� -   $��txtAwardGoldCoin��$��5��$��1��$
��10,5q�$��Center=����� *AwardGoldCoin+�� $��6��$��0��$��支付方式��$
��10,5q�$	��Top=�$��Right=��� -   $��
chkPayType��$��6��$��1��$��灵币支付��  SliderCheckBox#��[ $	��Top=�
�������$��5q��� $��7��$��0��$��请在��$
��10,5q�$	��Top=�$��Right=��� $��7��$��1��$	��Top=�$
��Left=�$��5q��� 
���  ��$
��Bold"���� *ValidTimeSecondsString+ �� Mode$ OneWay=� �� $�	内支付���� -   $��btnOK��$��8��$��2��$��
20,5,150,5q�$��确认支付��$	��100��$��Right=��� -   $��	btnCancel��$��8��$��2��$
��20,5q�$��取消��$	��100��$��Right=�