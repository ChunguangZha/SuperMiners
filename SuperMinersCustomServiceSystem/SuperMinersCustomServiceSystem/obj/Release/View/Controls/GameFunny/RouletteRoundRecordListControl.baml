   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  [    USuperMinersCustomServiceSystem.View.Controls.GameFunny.RouletteRoundRecordListControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5my&clr-namespace:SuperMinersWPF.MyControl     -   .��� $��5q�  � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $!��幸运大转盘开奖信息��$��18(�$
��Bold"�$��10q��� $��1�� �� Orientation$ 
Horizontal=�.+��� -   $��
btnRefresh��$��刷新��$	��100��$��10q�$
��Left=��� $!��当前大奖中奖倍数：��$��200,10,10,10q�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numMultiple��$��50��   DecimalPlaces$ 2��   Minimum$ 1Q�   Maximum$ 10Q�   Value$ 2Q�$��Center=��� -   $��btnSaveMultiple��$��修改��$��50��$��10q�$��Center=�&   System.Windows.Controls.DataGrid  -   $��	dgRecords��$��2��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+ �� Mode$ OneWay=�  $	 消耗矿石��$
 *  .  �� *AwardPoolSumStone+$ OneWay=�  $	 开出人民币��$
 *  .  �� *WinAwardSumYuan+$ OneWay=�  $	 盈利（人民币）��$
 *  .  �� *CurrentRoundProfitYuan+$ OneWay=�  $	 开始时间��$
 *  .  �� *	StartTime+$ OneWay=�  $	 结束奖项��$
 *  .  �� *MustWinAwardItemID+$ OneWay=�  $	 是否结束��$
 *  .  �� *
Finished+$ OneWay=�