   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      [    USuperMinersCustomServiceSystem.View.Controls.GameFunny.RouletteRoundRecordListControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5my&clr-namespace:SuperMinersWPF.MyControl     5      -   5        .��5
      � $��5q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $!��幸运大转盘开奖信息��6   $��18(�6%   $
��Bold"�63   $��10q�6E   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� -   5        $��
btnRefresh��5      $��刷新��6'   $	��100��64   $��10q�6@   $
��Left=�6L   6g   �� 5      $!��当前大奖中奖倍数：��6   $��200,10,10,10q�6)   -   'SuperMinersWPF.MyControl.NumericTextBox  5      -   5          Name$ numMultiple��5       $��50��65      DecimalPlaces$ 2��6@      Minimum$ 1Q�6R      Maximum$ 10Q�6^      Value$ 2Q�6k   $��Center=�6u   �� 5      -   5        $��btnSaveMultiple��5      $��修改��6,   $��50��69   $��10q�6D   $��Center=�6P   6k   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dgRecords��5      $��2��6$      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 61      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *ID+ �� Mode$ OneWay=�6b     5      $	 消耗矿石��6%   $
 * 63    .  6=   �� *AwardPoolSumStone+$ OneWay=�6p     5      $	 开出人民币��6%   $
 * 64    .  6>   �� *WinAwardSumYuan+$ OneWay=�6o     5      $	 盈利（人民币）��6%   $
 * 66    .  6@   �� *CurrentRoundProfitYuan+$ OneWay=�6x     5      $	 开始时间��6%   $
 * 63    .  6=   �� *	StartTime+$ OneWay=�6h     5      $	 结束奖项��6%   $
 * 63    .  6=   �� *MustWinAwardItemID+$ OneWay=�6q     5       $	 是否结束��6%   $
 * 63    .  6=   �� *
Finished+$ OneWay=�6g   5"      5#      5$      