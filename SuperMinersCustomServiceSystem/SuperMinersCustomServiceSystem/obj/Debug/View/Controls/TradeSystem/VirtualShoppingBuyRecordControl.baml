   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      ^    XSuperMinersCustomServiceSystem.View.Controls.TradeSystem.VirtualShoppingBuyRecordControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��积分商品购买记录��6   $��18(�6$   $
��Bold"�62   $��10q�6D   � 5   
   $��1��6     5      �� 5      �� 5      �� 5      .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   .+�5      �� $��订单号：��6   $��
10,10,5,10q�6(   $��Center=�6<   $y�	Collapsed=�6W   �� 5      -   5        $��txtOrderNumber��5      $��	0,10,5,10q�60   $	��100��6C    �� 	MaxLength$	 100��6O   $��Center=�6_   $y�	Collapsed=�6z   �� 5      $��买家用户名：��6   $��
10,10,5,10q�6*   $��Center=�6>   �� 5      -   5        $��txtBuyUserName��5      $��	0,10,5,10q�60   $	��100��6C   $ 64��6O   $��Center=�6^   �� 5      $��商品名：��6   $��
10,10,5,10q�6(   $��Center=�6<   �� 5      -   5        $��txtShoppingName��5      $��	0,10,5,10q�61   $	��100��6D   $ 64��6P   $��Center=�6_   �� 5       $��购买时间：从��6   $��
10,10,5,10q�6*   $��Center=�6>   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5!      -   5          Name$ dpStartPayTime��5!   .   $��Center=�6F      ShowTime .     TimeTickType$ 
StartOfDay=�6r   �� 5"      $	��到��6   $��Center=�6%     5#      -   5        $ dpEndPayTime��5#   .   $��Center=�6D    .  $ EndOfDay=�6p   5$      �� 5%      $��2��6   $ 
Horizontal=�6&   .+�5&      �� -   5        $��	btnSearch��5&      $��查询��6*   $	��100��67   $��10q�6C   $��Center=�6O   6j   �� 5'      $��每页显示：30 行��6   $��50,10,10,10q�6-   $��Center=�6B   �� 5(      -   5        $��btnPreviousPage��5(      $��	上一页��60   $��70��6>   $��10q�6I   $��Center=�6U   6p   �� 5)      $��当前显示第��6   $
��5,10q�6)   $��Center=�67   -   'SuperMinersWPF.MyControl.NumericTextBox  5*      -   5          Name$ numPageIndex��5*   +   $��50��6A    �� 
IsReadOnly . $
��5,10q�6a   $��Center=�6o      Minimum$ 1Q�6�   	   Maximum$	 10000000Q�6�   �� 5+      $	��页��6   $
��5,10q�6%   $��Center=�63   �� 5,      -	   5        $��btnNextPage��5,      $��	下一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   5-      5.      &   System.Windows.Controls.DataGrid  5/   
   -
   5        $��	dgRecords��5/       $��2��6   $��5q�61   
   AutoGenerateColumns
 .  $��Center=�6X      Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ 	订单号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 62      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *OrderNumber+ �� Mode$ OneWay=�6i     52      $ 买家用户名��6%   $ * 64    .  6>   �� *
UserName+$ OneWay=�6h     53      $ 	商品名��6%   $ * 62    .  6<   �� *VirtualShoppingItemName+$ OneWay=�6u     54      $ 购买时间��6%   $ * 63    .  6=   �� *BuyTimeText+$ OneWay=�6j   56      �� 57   
   $��3��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�58      �� $$��当前页面总计交易矿石��6   $��10q�6*   �� 59      -   5        $��txtSumStone��59      $	��200��6)    . $��10q�6G   �� 5:      $��总计价值灵币��6   $��10q�6&   �� 5;      -   5        $��	txtSumRMB��5;      $	��200��6'    . $��10q�6E   5<      �� 5=   
   $��4��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�5>      �� $��总计手续费��6   $��10q�6%   �� 5?      -   5        $��	txtSumFee��5?      $	��200��6'    . $��10q�6E   �� 5@      $��总计奖励金币��6   $��10q�6&   �� 5A      -   5        $��txtSumGoldCoin��5A      $	��200��6,    . $��10q�6J   5B      5C      5D      