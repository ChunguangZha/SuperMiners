   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  ^    XSuperMinersCustomServiceSystem.View.Controls.TradeSystem.WithdrawRMBHistoryRecordControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <	MyControl&clr-namespace:SuperMinersWPF.MyControl     Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��提现交易历史记录��$��18(�$
��Bold"�$��10q�� $��1��  �� �� �� .+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $��支付状态：��$��
10,10,5,10q�$��Center=��� -   $��cmbIsPay��$��	0,10,5,10q�$��Center=� �� SelectedIndex$ 0��.@��� $��全部���� $��等待���� $��完成���� $��拒绝���� $��玩家用户名：��$��
10,10,5,10q�$��Center=��� -   $��txtPlayerUserName��$��	0,10,5,10q�$	��100�� �� 	MaxLength$ 64��$��Center=��� $��创建时间：从��$��
10,10,5,10q�$��Center=�F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  -     Name$ dpStartCreateTime��$��Center=�   ShowTime .     TimeTickType$ 
StartOfDay=��� $	��到��$��Center=�  -   $ dpEndCreateTime��$��Center=� .  $ EndOfDay=��� -   $��panelAdminPayed��$��1��$ 
Horizontal=�.+��� $��管理员用户名：��$��
10,10,5,10q�$��Center=��� -   $��txtAdminUserName��$��	0,10,5,10q�$	��100��$ 64��$��Center=��� $��支付时间：从��$��
10,10,5,10q�$��Center=�  -   $ dpStartPayTime��$��Center=� .  $ 
StartOfDay=��� $	��到��$��Center=�  -   $ dpEndPayTime��$��Center=� .  $ EndOfDay=��� $��2��$ 
Horizontal=�.+��� -	   $��	btnSearch��$��查询��$	��100��$��10q�$��Center=��� $��每页显示：30 行��$��50,10,10,10q�$��Center=��� -
   $��btnPreviousPage��$��	上一页��$��70��$��10q�$��Center=��� $��当前显示第��$
��5,10q�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numPageIndex��$��50�� �� 
IsReadOnly . $
��5,10q�$��Center=�	   Minimum$	 1Q�
   Maximum$
 10000000Q��� $	��页��$
��5,10q�$��Center=��� -   $��btnNextPage��$��	下一页��$��70��$��10q�$��Center=�&   System.Windows.Controls.DataGrid  -   $��	dgRecords��$��2��$��5q�   AutoGenerateColumns .  $��Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+ �� Mode$ OneWay=�  $ 玩家用户名��$ *  .  �� *PlayerUserName+$ OneWay=�  $ 提现灵币��$ *  .  �� *WidthdrawRMB+ �� StringFormat$ f2��$ OneWay=�  $ 价值人民币��$ *  .  �� *	ValueYuan+$ f2��$ OneWay=�  $ 提交时间��$ *  .  �� *
CreateTime+$ OneWay=�  $ 支付状态��$ *  .  �� *	StateText+$ OneWay=�  $ 	处理人��$ *  .  �� *AdminUserName+$ OneWay=�  $ 处理时间��$ *  .  �� *PayTimeString+$ OneWay=�  $ 支付宝订单号��$ *  .  �� *AlipayOrderNumber+$ OneWay=�  $ 原因��$ *  .  �� *	Message+$ OneWay=�