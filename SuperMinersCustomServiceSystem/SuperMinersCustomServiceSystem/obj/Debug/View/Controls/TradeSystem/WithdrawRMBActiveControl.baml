   M S B A M L   `   `   `  ����"  SuperMinersCustomServiceSystemW    QSuperMinersCustomServiceSystem.View.Controls.TradeSystem.WithdrawRMBActiveControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��5q�6     � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��提现交易实时管理��6   $��18(�6$   $
��Bold"�62   $��10q�6D   �� 5   
   -   5        $��
btnRefresh��5      $��1��6   $��刷新��60   $	��100��6=   $��10q�6I   $
��Left=�6U   6p   �� 5   
   -   5        $��btnCSV��5      $��1��6   $��	保存CSV��6,   $	��100��6<   $��10q�6H   $��Right=�6T   6p   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dgRecords��5      $��2��6$      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 61      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *ID+ �� Mode$ OneWay=�6_     5      $ 玩家用户名��6%   $ * 64    .  6>   �� *PlayerUserName+$ OneWay=�6n     5      $ 提现灵币��6%   $ * 63    .  6=   �� *WidthdrawRMB+ �� StringFormat$ f2��$ OneWay=�6~     5      $ 价值人民币��6%   $ * 64    .  6>   �� *	ValueYuan+$ f2��$ OneWay=�6|     5      $ 提交时间��6%   $ * 63    .  6=   �� *
CreateTime+$ OneWay=�6i     5      $ 支付状态��6%   $ * 63    .  6=   �� *	StateText+$ OneWay=�6h   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ * 6)   	   CellTemplate	 5      �� 5      .R�5      �� -   5        $��支付��5   %   $��70��6]   $��Center=�6h   $��Center=�6�   y�62   �� *PayButtonVisibility+6�   5      5         5!      $ * 6)   	 5"      �� 5#      .R�5$      �� -   5        $��拒绝��5$   %   $��70��6]   $��Center=�6h   $��Center=�6�   y�62   �� *PayButtonVisibility+6�   5%      5'      5)      �� 5*   
   $��3��6   
 �� Orientation$
 
Horizontal=�6"   .+�5+      �� $��总计��6   $
��10,5q�6"   �� 5,      $��提现数：��6   $
��10,5q�6$   �� 5-      -   5        $��txtCount��5-      $	��100��6&   $
��10,5q�62    �� 
IsReadOnly . 6@   �� 5.      $��提现灵币：��6   $
��10,5q�6%   �� 5/      -   5        $��	txtSumRMB��5/      $	��100��6'   $
��10,5q�63    . 6A   �� 50      $��人民币：��6   $
��10,5q�6$   �� 51      -   5        $��
txtSumYuan��51      $	��100��6(   $
��10,5q�64    . 6B   52      53      54      