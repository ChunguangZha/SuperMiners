   M S B A M L   `   `   `  ����"  SuperMinersCustomServiceSystemb    \SuperMinersCustomServiceSystem.View.Controls.GameFunny.RouletteActiveWinRealAwardListControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��5q�6     � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $-��'幸运大转盘摇中实物奖项处理��6   $��18(�6)   $
��Bold"�67   $��10q�6I   �� 5   
   -   5        $��
btnRefresh��5      $��1��6   $��刷新��60   $	��100��6=   $��10q�6I   $
��Left=�6U   6p   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dgRecords��5      $��2��6$      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 61      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
RecordID+ �� Mode$ OneWay=�6e     5      $ 玩家用户名��6%   $ * 64    .  6>   �� *
UserName+$ OneWay=�6h     5      $ 中奖信息��6%   $ * 63    .  6=   �� *AwardItemName+ �� StringFormat$ f2��$ OneWay=�6     5      $ 中奖时间��6%   $ * 63    .  6=   �� *	WinTime+$ OneWay=�6f     5      $ 是否领取��6%   $ * 63    .  6=   �� *IsGot+$ OneWay=�6d     5      $ 领取QQ号��6%   $ * 64    .  6>   �� *
GotInfo1+$ OneWay=�6h     5      $ 领取手机号��6%   $ * 64    .  6>   �� *
GotInfo2+$ OneWay=�6h   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ * 6)   	   CellTemplate	 5      �� 5      .R�5      �� -   5        $��支付��5   %   $��70��6]   $��Center=�6h   $��Center=�6�   y�62   �� *PayButtonVisibility+6�   5      5      5!      5#      5$      