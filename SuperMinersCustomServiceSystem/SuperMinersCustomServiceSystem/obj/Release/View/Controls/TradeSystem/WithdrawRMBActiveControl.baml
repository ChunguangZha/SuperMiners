   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemW    QSuperMinersCustomServiceSystem.View.Controls.TradeSystem.WithdrawRMBActiveControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� $��5q�  � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��提现交易实时管理��$��18(�$
��Bold"�$��10q��� -   $��
btnRefresh��$��1��$��刷新��$	��100��$��10q�$
��Left=��� -   $��btnCSV��$��1��$��	保存CSV��$	��100��$��10q�$��Right=�&   System.Windows.Controls.DataGrid  -   $��	dgRecords��$��2��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+ �� Mode$ OneWay=�  $ 玩家用户名��$ *  .  �� *PlayerUserName+$ OneWay=�  $ 提现灵币��$ *  .  �� *WidthdrawRMB+ �� StringFormat$ f2��$ OneWay=�  $ 价值人民币��$ *  .  �� *	ValueYuan+$ f2��$ OneWay=�  $ 提交时间��$ *  .  �� *
CreateTime+$ OneWay=�  $ 支付状态��$ *  .  �� *	StateText+$ OneWay=�4  .System.Windows.Controls.DataGridTemplateColumn  $ * 	   CellTemplate	 �� .R��� -   $��支付��$��70��$��Center=�$��Center=�y��� *PayButtonVisibility+  $ * 	 �� .R��� -   $��拒绝��$��70��$��Center=�$��Center=�y��� *PayButtonVisibility+�� $��3��
 �� Orientation$
 
Horizontal=�.+��� $��总计��$
��10,5q��� $��提现数：��$
��10,5q��� -   $��txtCount��$	��100��$
��10,5q� �� 
IsReadOnly . �� $��提现灵币：��$
��10,5q��� -   $��	txtSumRMB��$	��100��$
��10,5q� . �� $��人民币：��$
��10,5q��� -   $��
txtSumYuan��$	��100��$
��10,5q� . 