   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemK    ESuperMinersCustomServiceSystem.View.Controls.DeletedPlayerInfoControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -   .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��已删除的玩家管理��$��18(�$
��Bold"�$��10q�� $��1��  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�.+��� $��用户名：��$��Right=�$��5q��� -   $��txtUserName��$��5q�$	��100��$��Center=��� $��	用户组��$��5q��� -   $��cmbUserGroup�� �� SelectedIndex$ 0��$��5q�$	��100��.@��� $��全部���� $��普通玩家���� $��测试玩家���� $��代理玩家���� $��支付宝账户：��$��Right=�$��5q��� -   $��txtAlipayAccount��$��5q�$	��150��$��Center=��� $��锁定状态：��$��Center=�$��Right=�$��5q��� -   $��	cmbLocked��$	��100��$��5q�$ 0��$
��Left=�$��Center=�.@��� $��全部���� $��	已锁定���� $��	未锁定���� $��1��$ 
Horizontal=�.+��� $��推荐人：��$��Right=�$��Center=�$��5q��� -   $��txtReferrerUserName��$��Center=�$��5q�$	��100���� $��邀请码：��$��Right=�$��Center=�$��5q��� -   $��txtInvitationCode��$��Center=�$��5q�$	��100���� $��登录IP：��$��Center=�$��5q��� -   $��
txtLoginIP��$��Center=�$��5q�$	��150���� $��登录MAC：��$��Center=�$��5q��� -	   $��txtLoginMac��$��Center=�$��5q�$	��150���� $��2��$ 
Horizontal=�.+��� $��在线状态：��$��Center=�$��Right=�$��5q��� -
   $��	cmbOnline��$	��100��$��5q�$
��Left=�$ 0��$��Center=�.@��� $��全部���� $��在线���� $��离线���� -   $��
btnRefresh��$��重新加载玩家列表��$	��150��$��5q�$
��Left=��� -   $��	btnSearch��$��当前页面中查找��$	��100��$��	170,5,5,5q�$
��Left=��� -   $��panelPlayersManager��$��2��$��5q����� $ 
Horizontal=�.+��� $��玩家列表��$��5q��� $	��共��$��20,5,5,5q��� -   $��txtCount��$��5q����� *PlayersCount+�� $	��人��$��5q�.��&   System.Windows.Controls.DataGrid  -   $��datagridPlayerInfos��   SelectionMode$ Single=�   AutoGenerateColumns .     FrozenColumnCount$ 2��   
IsReadOnly . ���� *ListFilteredPlayers+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 用户ID��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *UserID+  $ 	用户名��	 �� *
UserName+
   ClipboardContentBinding
 Z�   $ 昵称��	 �� *
NickName+
 Z�   $ 	用户组��	 �� *GroupTypeText+  $ 是否为代理推荐��	 �� *IsAgentReferred+  $ 代理推荐级别��	 �� *AgentReferredLevel+  $ 代理用户ID��	 �� *AgentUserID+  $ 支付宝账户��	 �� *Alipay+
 Z�   $ 支付宝真实姓名��	 �� *AlipayRealName+
 Z�   $ 身份证号��	 �� *
IDCardNo+  $ 注册时间��	 �� *RegisterTime+
 Z�   $ 注册IP��	 �� *
RegisterIP+
 Z�   $ 	推荐人��	 �� *ReferrerUserName+
 Z�   $ 	邀请码��	 �� *InvitationCode+
 Z�   $ 上一次登录时间��	 �� *LastLoginTime+
 Z�   $! 上一次收取矿石时间��	 �� *LastGatherStoneTime+  $ 	被锁定��	 �� *IsLockedString+
 Z�   $ 锁定时间��	 �� *LockedTimeString+
 Z�   $ 锁定天数��	 �� *LockedExpireDays+  $ 是否在线��	 �� *Online+
 Z�   $ 最近一次登录IP��	 �� *LastLoginIP+
 Z�   $ 最近一次登录MAC��	 �� *LastLoginMac+
 Z�   $ 	贡献值��	 �� *Exp+
 Z�   $ 	信誉值��	 �� *CreditValue+  $ 灵币��	 �� *RMB+
 Z�   $ 冻结灵币��	 �� *FreezingRMB+
 Z�   $ 金币��	 �� *
GoldCoin+
 Z�   $ 矿石储量��	 �� *StonesReserves+
 Z�   $ 累计总产出��	 �� *TotalProducedStonesCount+
 Z�   $ 	矿石量��	 �� *StockOfStones+
 Z�   $ 冻结矿石��	 �� *FreezingStones+  $ 	矿工数��	 �� *MinersCount+
 Z�   $ 	钻石量��	 �� *StockOfDiamonds+
 Z�   $ 矿石出售券��	 �� *StoneSellQuan+