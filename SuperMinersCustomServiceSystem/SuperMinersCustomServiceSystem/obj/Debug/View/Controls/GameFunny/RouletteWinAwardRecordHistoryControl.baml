   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      a    [SuperMinersCustomServiceSystem.View.Controls.GameFunny.RouletteWinAwardRecordHistoryControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $'��!幸运大转盘中奖历史记录��6   $��18(�6'   $
��Bold"�65   $��10q�6G   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��玩家用户名：��6   $��
10,10,5,10q�6&   $��Center=�6:   �� 5      -   5        $��txtUserName��5      $��	0,10,5,10q�6)   $	��100��6<    �� 	MaxLength$ 64��6H   $��Center=�6W   �� 5      $��中奖信息��6   $��	0,10,5,10q�6$   $��Center=�67   �� 5      -   5        $��cmbAwardItems��5      $	��200��6,   $��
10,10,5,10q�68   $��Center=�6L    �� DisplayMemberPath$ 	AwardName��6g    �� SelectedValuePath$ ID��6�   �� 5      $��已领取：��6   $��
10,10,5,10q�6$   $��Center=�68   �� 5      -   5        $��cmbIsGot��5       �� SelectedIndex$ 0��6'   $��	0,10,5,10q�69   $��70��6L   $��Center=�6W   .@�5      �� $��全部��6   �� 5      $��	未支付��6   �� 5      $��	已支付��6   5      �� 5      $��已支付：��6   $��
10,10,5,10q�6$   $��Center=�68   �� 5      -   5        $��cmbIsPay��5      $ 0��6'   $��	0,10,5,10q�69   $��70��6L   $��Center=�6W   .@�5       �� $��全部��6   �� 5!      $��	未支付��6   �� 5"      $��	已支付��6   5#      5$      �� 5%   
   $��2��6   $ 
Horizontal=�6"   .+�5&      �� $��中奖时间：从��6   $��
10,10,5,10q�6&   $��Center=�6:   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5'      -   5          Name$ dpStartWinTime��5'   *   $��Center=�6B      ShowTime .     TimeTickType$ 
StartOfDay=�6n   �� 5(      $	��到��6   $��Center=�6!     5)      -   5        $ dpEndWinTime��5)   *   $��Center=�6@    .  $ EndOfDay=�6l   5*      �� 5+   
   $��3��6   $ 
Horizontal=�6"   .+�5,      �� -   5        $��	btnSearch��5,      $��查询��6&   $	��100��63   $��10q�6?   $��Center=�6K   6f   �� 5-      $��每页显示：30 行��6   $��50,10,10,10q�6)   $��Center=�6>   �� 5.      -   5        $��btnPreviousPage��5.      $��	上一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   �� 5/      $��当前显示第��6   $
��5,10q�6%   $��Center=�63   -   'SuperMinersWPF.MyControl.NumericTextBox  50      -	   5        	  Name$	 numPageIndex��50   '   $��50��6=   
 �� 
IsReadOnly
 . $
��5,10q�6]   $��Center=�6k      Minimum$ 1Q�6�      Maximum$ 10000000Q�6�   �� 51      $	��页��6   $
��5,10q�6!   $��Center=�6/   �� 52      -
   5        $��btnNextPage��52      $��	下一页��6(   $��70��66   $��10q�6A   $��Center=�6M   6h   53      &   System.Windows.Controls.DataGrid  55   
   -   5        $��datagrid��55       $��4��6      AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�6^      Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ 记录ID��60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
RecordID+  58      $ * 6%   $ 	用户名��60    6=   �� *
UserName+  59      $ * 6%   $ 所中奖项��60    6>   �� *AwardItemName+  5:      $ * 6%   $ 中奖时间��60    6>   �� *	WinTime+  5;      $ * 6%   $ 领奖QQ��60    6>   �� *
GotInfo1+  5<      $ * 6%   $ 领奖电话��60    6>   �� *
GotInfo2+  5=      $ * 6%   $ 是否领取��60    6>   �� *IsGot+  5>      $ * 6%   $ 领取时间��60    6>   �� *	GotTime+  5?      $ * 6%   $ 是否支付��60    6>   �� *IsPay+  5@      $ * 6%   $ 支付时间��60    6>   �� *	PayTime+5B      5C      5D      