   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  E    ?SuperMinersWPF.Views.Controls.StoneBuyTradeHistoryRecordControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     <	MyControl&clr-namespace:SuperMinersWPF.MyControl     Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��矿石买入历史记录��$��18(�$
��Bold"�$��10q�� $��1��  �� �� �� .+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $��订单号：��$��
10,10,5,10q�$��Center=��� -   $��txtOrderNumber��$��	0,10,5,10q�$	��100�� �� 	MaxLength$	 100��$��Center=��� $��卖家用户名：��$��
10,10,5,10q�$��Center=��� -   $��txtSellerUserName��$��	0,10,5,10q�$	��100��$ 64��$��Center=��� $��1��$ 
Horizontal=�.+��� $��出售时间：从��$��
10,10,5,10q�$��Center=�F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  -     Name$ dpStartCreateTime��$��Center=�   ShowTime .  �� $	��到��$��Center=�  -   $ dpEndCreateTime��$��Center=� .  �� $��购买时间：从��$��
10,10,5,10q�$��Center=�  -   $ dpStartPayTime��$��Center=� .  �� $	��到��$��Center=�  -   $ dpEndPayTime��$��Center=� .  �� $��2��$ 
Horizontal=�.+��� -   $��	btnSearch��$��查询��$	��100��$��10q�$��Center=��� $��每页显示：30 行��$��50,10,10,10q�$��Center=��� -   $��btnPreviousPage��$��	上一页��$��70��$��10q�$��Center=��� $��当前显示第��$
��5,10q�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -	     Name$ numPageIndex��$��50�� �� 
IsReadOnly . $
��5,10q�$��Center=�   Minimum$ 1Q�   Maximum$ 10000000Q��� $	��页��$
��5,10q�$��Center=��� -
   $��btnNextPage��$��	下一页��$��70��$��10q�$��Center=�&   System.Windows.Controls.DataGrid  -   $��	dgRecords��$��2��$��5q�	   AutoGenerateColumns	 .  $��Center=�
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	订单号��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *OrderNumber+ �� Mode$ OneWay=�  $ 卖家用户名��$ *  .  �� *SellerUserName+$ OneWay=�  $ 交易矿石数��$ *  .  �� *SellStonesCount+$ OneWay=�  $ 价值灵币��$ *  .  �� *
ValueRMB+$ OneWay=�  $ 	手续费��$ *  .  �� *	Expense+$ OneWay=�  $ 订单状态��$ *  .  �� *OrderStateText+$ OneWay=�  $ 出售时间��$ *  .  �� *
SellTime+$ OneWay=�  $ 买家用户名��$ *  .  �� *BuyerUserName+$ OneWay=�  $ 奖励金币��$ *  .  �� *AwardGoldCoin+$ OneWay=�  $ 支付时间��$ *  .  �� *	BuyTime+$ OneWay=�