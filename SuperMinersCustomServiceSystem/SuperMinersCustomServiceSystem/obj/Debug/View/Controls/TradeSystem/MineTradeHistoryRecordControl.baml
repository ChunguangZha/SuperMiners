   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      \    VSuperMinersCustomServiceSystem.View.Controls.TradeSystem.MineTradeHistoryRecordControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��矿山交易历史记录��6   $��18(�6$   $
��Bold"�62   $��10q�6D   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��玩家用户名：��6   $��
10,10,5,10q�6&   $��Center=�6:   �� 5      -   5        $��txtPlayerUserName��5      $��	0,10,5,10q�6/   $	��100��6B    �� 	MaxLength$ 64��6N   $��Center=�6]   �� 5      $��创建时间：从��6   $��
10,10,5,10q�6&   $��Center=�6:   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5      -   5          Name$ dpStartCreateTime��5   *   $��Center=�6E      ShowTime .     TimeTickType$ 
StartOfDay=�6q   �� 5      $	��到��6   $��Center=�6!     5      -   5        $ dpEndCreateTime��5   *   $��Center=�6C    .  $ EndOfDay=�6o   5      �� 5   
   $��2��6   $ 
Horizontal=�6"   .+�5      �� -   5        $��	btnSearch��5      $��查询��6&   $	��100��63   $��10q�6?   $��Center=�6K   6f   �� 5      $��每页显示：30 行��6   $��50,10,10,10q�6)   $��Center=�6>   �� 5      -   5        $��btnPreviousPage��5      $��	上一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   �� 5      $��当前显示第��6   $
��5,10q�6%   $��Center=�63   -   'SuperMinersWPF.MyControl.NumericTextBox  5      -   5          Name$ numPageIndex��5   '   $��50��6=    �� 
IsReadOnly . $
��5,10q�6]   $��Center=�6k      Minimum$ 1Q�6�   	   Maximum$	 10000000Q�6�   �� 5       $	��页��6   $
��5,10q�6!   $��Center=�6/   �� 5!      -   5        $��btnNextPage��5!      $��	下一页��6(   $��70��66   $��10q�6A   $��Center=�6M   6h   5"      &   System.Windows.Controls.DataGrid  5$   
   -   5        $��datagrid��5$       $��3��6   
   AutoGenerateColumns
 .     
IsReadOnly .    SelectionMode$ Single=�6^      Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ 	用户名��60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *
UserName+  5'      $ * 6%   $ 	订单号��60    6=   �� *OrderNumber+  5(      $ * 6%   $ 花费灵币��60    6>   �� *
SpendRMB+  5)      $ * 6%   $ 购买矿山数��60    6?   �� *GainMinesCount+  5*      $ * 6%   $ 获取矿石储量��60    6@   �� *GainStonesReserves+  5+      $ * 6%   $ 创建时间��60    6>   �� *
CreateTime+5-      5.      5/      