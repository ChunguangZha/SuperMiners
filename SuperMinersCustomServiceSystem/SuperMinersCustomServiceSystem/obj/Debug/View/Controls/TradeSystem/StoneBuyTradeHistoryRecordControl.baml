   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      `    ZSuperMinersCustomServiceSystem.View.Controls.TradeSystem.StoneBuyTradeHistoryRecordControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��矿石买入历史记录��6   $��18(�6$   $
��Bold"�62   $��10q�6D   � 5   
   $��1��6     5      �� 5      �� 5      �� 5      .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   .+�5      �� $��订单号：��6   $��
10,10,5,10q�6(   $��Center=�6<   �� 5      -   5        $��txtOrderNumber��5      $��	0,10,5,10q�60   $	��100��6C    �� 	MaxLength$	 100��6O   $��Center=�6_   �� 5      $��卖家用户名：��6   $��
10,10,5,10q�6*   $��Center=�6>   �� 5      -   5        $��txtSellerUserName��5      $��	0,10,5,10q�63   $	��100��6F   $ 64��6R   $��Center=�6a   �� 5      $��买家用户名：��6   $��
10,10,5,10q�6*   $��Center=�6>   �� 5      -   5        $��txtBuyerUserName��5      $��	0,10,5,10q�62   $	��100��6E   $ 64��6Q   $��Center=�6`   �� 5       $��订单状态：��6   $��
10,10,5,10q�6)   $��Center=�6=   �� 5!      -   5        $��cmbOrderState��5!      $��	0,10,5,10q�60   $	��100��6C   $��Center=�6O    �� SelectedIndex$ 0��6j   .@�5"      �� $��全部��6#   �� 5#      $��等待��6#   �� 5$      $��锁定��6#   �� 5%      $��完成��6#   �� 5&      $��异常��6#   5'      5(      �� 5)      $��1��6   $ 
Horizontal=�6&   .+�5*      �� $��出售时间：从��6   $��
10,10,5,10q�6*   $��Center=�6>   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5+      -   5          Name$ dpStartCreateTime��5+   .   $��Center=�6I      ShowTime .     TimeTickType$ 
StartOfDay=�6u   �� 5,      $	��到��6   $��Center=�6%     5-      -   5        $ dpEndCreateTime��5-   .   $��Center=�6G    .  $ EndOfDay=�6s   �� 5.      $��购买时间：从��6   $��
10,10,5,10q�6*   $��Center=�6>     5/      -   5        $ dpStartPayTime��5/   .   $��Center=�6F    .  $ 
StartOfDay=�6r   �� 50      $	��到��6   $��Center=�6%     51      -   5        $ dpEndPayTime��51   .   $��Center=�6D    .  $ EndOfDay=�6p   52      �� 53      $��2��6   $ 
Horizontal=�6&   .+�54      �� -	   5        $��	btnSearch��54      $��查询��6*   $	��100��67   $��10q�6C   $��Center=�6O   6j   �� 55      $��每页显示：30 行��6   $��50,10,10,10q�6-   $��Center=�6B   �� 56      -
   5        $��btnPreviousPage��56      $��	上一页��60   $��70��6>   $��10q�6I   $��Center=�6U   6p   �� 57      $��当前显示第��6   $
��5,10q�6)   $��Center=�67   -   'SuperMinersWPF.MyControl.NumericTextBox  58      -   5          Name$ numPageIndex��58   +   $��50��6A    �� 
IsReadOnly . $
��5,10q�6a   $��Center=�6o   	   Minimum$	 1Q�6�   
   Maximum$
 10000000Q�6�   �� 59      $	��页��6   $
��5,10q�6%   $��Center=�63   �� 5:      -   5        $��btnNextPage��5:      $��	下一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   5;      5<      &   System.Windows.Controls.DataGrid  5=   
   -   5        $��	dgRecords��5=       $��2��6   $��5q�61      AutoGenerateColumns .  $��Center=�6X      Columns 5>      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn   Header$ 	订单号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 62      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *OrderNumber+ �� Mode$ OneWay=�6i     5@      $ 卖家用户名��6%   $ * 64    .  6>   �� *SellerUserName+$ OneWay=�6n     5A      $ 交易矿石数��6%   $ * 64    .  6>   �� *SellStonesCount+$ OneWay=�6o     5B      $ 价值灵币��6%   $ * 63    .  6=   �� *
ValueRMB+$ OneWay=�6g     5C      $ 	手续费��6%   $ * 62    .  6<   �� *	Expense+$ OneWay=�6e     5D      $ 订单状态��6%   $ * 63    .  6=   �� *OrderStateText+$ OneWay=�6m     5E      $ 出售时间��6%   $ * 63    .  6=   �� *
SellTime+$ OneWay=�6g     5F      $ 买家用户名��6%   $ * 64    .  6>   �� *BuyerUserName+$ OneWay=�6m     5G      $ 奖励金币��6%   $ * 63    .  6=   �� *AwardGoldCoin+$ OneWay=�6l     5H      $ 支付时间��6%   $ * 63    .  6=   �� *	BuyTime+$ OneWay=�6f   5J      �� 5K   
   $��3��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�5L      �� $$��当前页面总计交易矿石��6   $��10q�6*   �� 5M      -   5        $��txtSumStone��5M      $	��200��6)    . $��10q�6G   �� 5N      $��总计价值灵币��6   $��10q�6&   �� 5O      -   5        $��	txtSumRMB��5O      $	��200��6'    . $��10q�6E   5P      �� 5Q   
   $��4��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�5R      �� $��总计手续费��6   $��10q�6%   �� 5S      -   5        $��	txtSumFee��5S      $	��200��6'    . $��10q�6E   �� 5T      $��总计奖励金币��6   $��10q�6&   �� 5U      -   5        $��txtSumGoldCoin��5U      $	��200��6,    . $��10q�6J   5V      5W      5X      