   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      a    [SuperMinersCustomServiceSystem.View.Controls.TradeSystem.StoneSellTradeHistoryRecordControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��矿石出售历史记录��6   $��18(�6$   $
��Bold"�62   $��10q�6D   � 5   
   $��1��6     5      �� 5      �� 5      �� 5      .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   .+�5      �� $��订单号：��6   $��
10,10,5,10q�6(   $��Center=�6<   �� 5      -   5        $��txtOrderNumber��5      $��	0,10,5,10q�60   $	��100��6C    �� 	MaxLength$	 100��6O   $��Center=�6_   �� 5      $��卖家用户名：��6   $��
10,10,5,10q�6*   $��Center=�6>   �� 5      -   5        $��txtSellerUserName��5      $��	0,10,5,10q�63   $	��100��6F   $ 64��6R   $��Center=�6a   �� 5      $��订单状态：��6   $��
10,10,5,10q�6)   $��Center=�6=   �� 5      -   5        $��cmbOrderState��5      $��	0,10,5,10q�60   $	��100��6C   $��Center=�6O    �� SelectedIndex$ 0��6j   .@�5       �� $��全部��6#   �� 5!      $��等待��6#   �� 5"      $��锁定��6#   �� 5#      $��完成��6#   �� 5$      $��异常��6#   5%      5&      �� 5'      $��1��6   $ 
Horizontal=�6&   .+�5(      �� $��出售时间：从��6   $��
10,10,5,10q�6*   $��Center=�6>   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5)      -   5          Name$ dpStartCreateTime��5)   .   $��Center=�6I      ShowTime .     TimeTickType$ 
StartOfDay=�6u   �� 5*      $	��到��6   $��Center=�6%     5+      -   5        $ dpEndCreateTime��5+   .   $��Center=�6G    .  $ EndOfDay=�6s   5,      �� 5-      $��2��6   $ 
Horizontal=�6&   .+�5.      �� -   5        $��	btnSearch��5.      $��查询��6*   $	��100��67   $��10q�6C   $��Center=�6O   6j   �� 5/      $��每页显示：30 行��6   $��50,10,10,10q�6-   $��Center=�6B   �� 50      -   5        $��btnPreviousPage��50      $��	上一页��60   $��70��6>   $��10q�6I   $��Center=�6U   6p   �� 51      $��当前显示第��6   $
��5,10q�6)   $��Center=�67   -   'SuperMinersWPF.MyControl.NumericTextBox  52      -   5          Name$ numPageIndex��52   +   $��50��6A    �� 
IsReadOnly . $
��5,10q�6a   $��Center=�6o   	   Minimum$	 1Q�6�   
   Maximum$
 10000000Q�6�   �� 53      $	��页��6   $
��5,10q�6%   $��Center=�63   �� 54      -	   5        $��btnNextPage��54      $��	下一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   55      56      &   System.Windows.Controls.DataGrid  57   
   -
   5        $��	dgRecords��57       $��2��6   $��5q�61      AutoGenerateColumns .  $��Center=�6X      Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$ 	订单号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 62      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *OrderNumber+ �� Mode$ OneWay=�6i     5:      $ 卖家用户名��6%   $ * 64    .  6>   �� *SellerUserName+$ OneWay=�6n     5;      $ 交易矿石数��6%   $ * 64    .  6>   �� *SellStonesCount+$ OneWay=�6o     5<      $ 价值灵币��6%   $ * 63    .  6=   �� *
ValueRMB+$ OneWay=�6g     5=      $ 	手续费��6%   $ * 62    .  6<   �� *	Expense+$ OneWay=�6e     5>      $ 订单状态��6%   $ * 63    .  6=   �� *OrderStateText+$ OneWay=�6m     5?      $ 出售时间��6%   $ * 63    .  6=   �� *
SellTime+$ OneWay=�6g   5A      �� 5B   
   $��3��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�5C      �� $$��当前页面总计交易矿石��6   $��10q�6*   �� 5D      -   5        $��txtSumStone��5D      $	��200��6)    . $��10q�6G   �� 5E      $��总计价值灵币��6   $��10q�6&   �� 5F      -   5        $��	txtSumRMB��5F      $	��200��6'    . $��10q�6E   5G      �� 5H   
   $��4��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�5I      �� $��总计手续费��6   $��10q�6%   �� 5J      -   5        $��	txtSumFee��5J      $	��200��6'    . $��10q�6E   5K      5L      5M      