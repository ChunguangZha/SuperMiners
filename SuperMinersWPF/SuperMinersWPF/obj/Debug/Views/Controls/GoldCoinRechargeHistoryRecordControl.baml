   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      H    BSuperMinersWPF.Views.Controls.GoldCoinRechargeHistoryRecordControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��金币充值历史记录��6   $��18(�6$   $
��Bold"�62   $��10q�6D   � 5   
   $��1��6     5      �� 5      �� 5      �� 5      .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   .+�5      �� $��订单号：��6   $��
10,10,5,10q�6(   $��Center=�6<   �� 5      -   5        $��txtOrderNumber��5      $��	0,10,5,10q�60   $	��100��6C    �� 	MaxLength$	 100��6O   $��Center=�6_   �� 5      $��时间：从��6   $��
10,10,5,10q�6(   $��Center=�6<   F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  5      -   5          Name$ dpStartCreateTime��5   .   $��Center=�6I      ShowTime .  6d   �� 5      $	��到��6   $��Center=�6%     5      -   5        $ dpEndCreateTime��5   .   $��Center=�6G    .  6b   5      �� 5      $��2��6   $ 
Horizontal=�6&   .+�5       �� -   5        $��	btnSearch��5       $��查询��6*   $	��100��67   $��10q�6C   $��Center=�6O   6j   �� 5!      $��每页显示：30 行��6   $��50,10,10,10q�6-   $��Center=�6B   �� 5"      -   5        $��btnPreviousPage��5"      $��	上一页��60   $��70��6>   $��10q�6I   $��Center=�6U   6p   �� 5#      $��当前显示第��6   $
��5,10q�6)   $��Center=�67   -   'SuperMinersWPF.MyControl.NumericTextBox  5$      -   5          Name$ numPageIndex��5$   +   $��50��6A    �� 
IsReadOnly . $
��5,10q�6a   $��Center=�6o      Minimum$ 1Q�6�      Maximum$ 10000000Q�6�   �� 5%      $	��页��6   $
��5,10q�6%   $��Center=�63   �� 5&      -   5        $��btnNextPage��5&      $��	下一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   5'      5(      &   System.Windows.Controls.DataGrid  5)   
   -   5        $��	dgRecords��5)       $��2��6   $��5q�61   	   AutoGenerateColumns	 .  $��Center=�6X   
   Columns
 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ 玩家用户名��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 64      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
UserName+ �� Mode$ OneWay=�6h     5,      $ 	订单号��6%   $ * 62    .  6<   �� *OrderNumber+$ OneWay=�6i     5-      $ 花费灵币��6%   $ * 63    .  6=   �� *
SpendRMB+ �� StringFormat$ f2��$ OneWay=�6z     5.      $ 获取金币��6%   $ * 63    .  6=   �� *GainGoldCoin+$ OneWay=�6k     5/      $ 提交时间��6%   $ * 63    .  6=   �� *
CreateTime+$ OneWay=�6i   51      52      53      