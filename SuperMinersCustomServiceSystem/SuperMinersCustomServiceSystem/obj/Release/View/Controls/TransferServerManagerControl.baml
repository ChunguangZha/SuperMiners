   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemO    ISuperMinersCustomServiceSystem.View.Controls.TransferServerManagerControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -   .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��跨区转移��$��18(�$
��Bold"�$��10q�� $��1��  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�.+��� $��用户名：��$��Right=�$��Center=�$��5q��� -   $��txtUserName��$��5q�$	��100��$��Center=��� $��转移状态��$��5q�$��Center=��� -   $��cmbTransferState�� �� SelectedIndex$ 1��$��5q�$	��100��.@��� $��全部���� $��	未转移���� $��	已完成���� -   $��
btnRefresh��$��重新加载玩家列表��$	��150��$��5q�$
��Left=��� -   $��	btnSearch��$��当前页面中查找��$	��100��$��	170,5,5,5q�$
��Left=��� -   $��panelPlayersManager��$��2��$��5q�.��&   System.Windows.Controls.DataGrid  -   $��datagridPlayerInfos��   SelectionMode$ Single=�   AutoGenerateColumns .     FrozenColumnCount$ 2��   
IsReadOnly . ���� *ListFilteredPlayers+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 记录ID��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *ID+  $ 	用户名��$	 * 
 �� *
UserName+  $ 转移状态��$	 * 
 �� *isTransfered+  $ 支付宝账户��$	 * 
 �� *AlipayAccount+  $ 支付宝实名��$	 * 
 �� *AlipayRealName+  $ 邮箱��$	 * 
 �� *Email+  $ 申请时间��$	 * 
 �� *SubmitTimeText+  $ 处理时间��$	 * 
 �� *HandledTimeText+  $ 	处理人��$	 * 
 �� *HandlerName+�� $��3��$ 
Horizontal=�.+��� -   $��btnEditPlayerInfo��$��Right=�$��查看玩家详细信息��$	��200��$��10q��� -	   $��btnTransfer��$��Right=�$��确认转区��$	��200��$��10q�