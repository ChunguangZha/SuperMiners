   M S B A M L   `   `   `  ����"  SuperMinersCustomServiceSystemO    ISuperMinersCustomServiceSystem.View.Controls.TransferServerManagerControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��跨区转移��6   $��18(�6    $
��Bold"�6.   $��10q�6@   � 5   
   $��1��6     5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��用户名：��6   $��Right=�6(   $��Center=�6D   $��5q�6_   �� 5      -   5        $��txtUserName��5      $��5q�6-   $	��100��68   $��Center=�6D   �� 5      $��转移状态��6   $��5q�6(   $��Center=�63   �� 5      -   5        $��cmbTransferState��5       �� SelectedIndex$ 1��63   $��5q�6E   $	��100��6P   .@�5      �� $��全部��6#   �� 5      $��	未转移��6#   �� 5      $��	已完成��6#   5       �� 5!      -   5        $��
btnRefresh��5!      $��重新加载玩家列表��6+   $	��150��6>   $��5q�6J   $
��Left=�6U   6p   �� 5"      -   5        $��	btnSearch��5"      $��当前页面中查找��6*   $	��100��6<   $��	170,5,5,5q�6H   $
��Left=�6[   6v   5#      5$      �� 5%   
   -   5        $��panelPlayersManager��5%       $��2��6   $��5q�6;   .��5&      &   System.Windows.Controls.DataGrid  -   5        $��datagridPlayerInfos��5&         SelectionMode$ Single=�6^      AutoGenerateColumns .     FrozenColumnCount$ 2��6�      
IsReadOnly . ��62   �� *ListFilteredPlayers+   Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ 记录ID��6@   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6N   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6)   �� *ID+6N     5)      $ 	用户名��6F   $	 * 6S   
 6)   �� *
UserName+6S     5*      $ 转移状态��6J   $	 * 6X   
 6)   �� *isTransfered+6X     5+      $ 支付宝账户��6K   $	 * 6Z   
 6)   �� *AlipayAccount+6Z     5,      $ 支付宝实名��6L   $	 * 6[   
 6)   �� *AlipayRealName+6[     5-      $ 邮箱��6C   $	 * 6O   
 6)   �� *Email+6O     5.      $ 申请时间��6L   $	 * 6Z   
 6)   �� *SubmitTimeText+6Z     5/      $ 处理时间��6M   $	 * 6[   
 6)   �� *HandledTimeText+6[     50      $ 	处理人��6I   $	 * 6V   
 6)   �� *HandlerName+6V   52      53      �� 54   
   $��3��6   $ 
Horizontal=�6"   .+�55      �� -   5        $��btnEditPlayerInfo��55      $��Right=�6.   $��查看玩家详细信息��6J   $	��200��6]   $��10q�6i   6u   �� 56      -	   5        $��btnTransfer��56      $��Right=�6(   $��确认转区��6D   $	��200��6S   $��10q�6_   6k   57      58      59      