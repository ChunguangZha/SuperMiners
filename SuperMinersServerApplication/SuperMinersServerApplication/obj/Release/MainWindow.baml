   M S B A M L   `   `   `  ���� W  SSuperMinersServerApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:SuperMinersServerApplication.MyControl&SuperMinersServerApplication.MyControl  -    'SuperMinersServerApplication.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Cmy4clr-namespace:SuperMinersServerApplication.MyControl     -     � Title$  服务器配置��$	��500��$	��800�� � Icon$ Images/black_server.ico��.��� .+��� .@��� $��服务控制��.���  � RowDefinitions �� $	��87* ��� $
��355* �.+��� $��服务状态��.���  � ColumnDefinitions �� $��40 ��� $	��430 ��� $	��120 ��� $
��100* �.+��� -   $��
imgStarted��$��0��$��32��$��Center=�$��0q�$
��Fill=�$��Center=�$��32��$��Images/ServerUp.png���� -   $��
imgStopped��$��0��$��32��$��Center=�$��0q�$
��Fill=�$��Center=�$��32��$��Images/ServerDown.png���� -   $��label2��$��1��$��服务状态:��$��23��$��Stretch=�$	��5,0q�$	��Top=�$��12(�$��2q�$��Center=��� -   $��lblServiceWasStarted��$��1��$��服务正在运行...��$��23��$��Stretch=�$��5,25,5,0q�$	��Top=�$��12(�$��2q�$��Center=�$
��Bold"��� -   $��lblServiceWasStopped��$��1��$��服务已经停止��$��23��$��Stretch=�$��5,25,5,0q�$	��Top=�$��12(�$��2q�$��Center=�$
��Bold"�� -   $��progressbar��$��1��$��23��$��Stretch=�$��5,25,5,0q�$	��Top=�$y�Hidden=� � IsIndeterminate . �� -   $��btnStartService��$��2��$��23��$��Stretch=�$��5,25,5,0q�$	��Top=�.����  �� Orientation$ 
Horizontal=�.+��� $��Images/Start.png��$��16��$��16����  �� TextAlignment$ Center=�$��15��启动服务�� -	   $��btnStopService��$��2��$��23��$��Stretch=�$��5,25,5,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��Images/Stop.png��$��16��$��16���� $ Center=�$��15��停止服务�� -
   $��btnLogsOutput��$��3��$��导出日志��$	��100��$��5,25,5,0q�$	��Top=��� $��1��$��日志��.���� -   $��listboxLogs���� -   $��tabItemConfig��$��配  置��.���  �� �� $
��Auto �.+��� -   $��tabcontrolConfig��.@��� -   $��tabitemRegisterUser��$��注册用户��.��� .+�� $��Center=�$��Center=� �� ��  �� �� �� �� �� �� �� .+��� $#��同一IP可奖励玩家数：��$��Right=�$��Center=�$��10q�;   5SuperMinersServerApplication.MyControl.NumericTextBox  $��1��$	��100��$
��Left=�$��Center=�$��10q�   Value �� *UserCountCreateByOneIP+ �� Mode$ TwoWay=��� $��1��$��0��$��Right=�$��Center=�$��10q��� $�赠送新注册玩家�� �� ��� *Strings.Exp+$ OneWay=�   Language	 �� Source#	 [   �� $	�：��  $��1��$��1��$	��100��$
��Left=�$��Center=�$��10q�
   DecimalPlaces$
 2�� �� *GiveToNewUserExp+$ TwoWay=��� $��2��$��0��$��Right=�$��Center=�$��10q��� $�赠送新注册玩家�� �� ��� *Strings.GoldCoin+$ OneWay=�#	 [   �� $	�：��  $��2��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 2�� �� *GiveToNewUserGoldCoin+$ TwoWay=��� $��3��$��0��$��Right=�$��Center=�$��10q��� $�赠送新注册玩家�� �� ��� *Strings.Mine+$ OneWay=�#	 [   �� $	�：��  $��3��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 2�� �� *GiveToNewUserMines+$ TwoWay=��� $��4��$��0��$��Right=�$��Center=�$��10q��� $�赠送新注册玩家�� �� ��� *Strings.Miner+$ OneWay=�#	 [   �� $	�：��  $��4��$��1��$	��100��$
��Left=�$��Center=�$��10q� �� *GiveToNewUserMiners+$ TwoWay=��� $��5��$��0��$��Right=�$��Center=�$��10q��� $�赠送新注册玩家�� �� ��� *Strings.Stone+$ OneWay=�#	 [   �� $	�：��  $��5��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 3�� �� *GiveToNewUserStones+$ TwoWay=��� $��6��$��0��$��Right=�$��Center=�$��10q��� $�第一次支付宝充值�� �� ��� *Strings.GoldCoin+$ OneWay=�#	 [   �� $�奖励倍数：��  $��6��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 3�� �� **(FirstAlipayRechargeGoldCoinAwardMultiple+$ TwoWay=��� -   $��tabitemAwardReferrer��$��推荐人奖励��.��� .+�� $��Center=�$��Center=�$	��500�� �� $
��Auto ��� �� $
��Auto � �� �� �� �� �� �� �� .+��� 
�������$��1,1,1,0q�._��� $��推荐级别��$��Center=�$��Center=��� $��1��
�������$��0,1,1,0q�._��� $��Center=�$��Center=��� $�奖励�� �� ��� *Strings.Exp+$ OneWay=�#	 [  �� $��2��
�������$��0,1,1,0q�._��� $��Center=�$��Center=��� $�奖励�� �� ��� *Strings.GoldCoin+$ OneWay=�#	 [  �� $��3��
�������$��0,1,1,0q�._��� $��Center=�$��Center=��� $�奖励�� �� ��� *Strings.Mine+$ OneWay=�#	 [  �� $��4��
�������$��0,1,1,0q�._��� $��Center=�$��Center=��� $�奖励�� �� ��� *Strings.Miner+$ OneWay=�#	 [  �� $��5��
�������$��0,1,1,0q�._��� $��Center=�$��Center=��� $�奖励�� �� ��� *Strings.Stone+$ OneWay=�#	 [  �� $��6��
�������$��0,1,1,0q�._��� $��Center=�$��Center=��� $�奖励�� �� ��� *Strings.Diamond+$ OneWay=�#	 [  �� $��1��$��7��$��Stretch=�$��Stretch=����� *InnerListAwardReferrerConfig+���� .R��  �� �� �� �� �� �� �� .+��� 
�������$��1,1,1,0q�._��� $��Center=�$��Stretch=�$ Center=����� *
ReferLevel+�� $��1��
�������$��0,1,1,0q�._�  $
 2��$��Center=�$��Stretch=� �� TextAlignment$ Center=� �� *AwardReferrerExp+$ TwoWay=��� $��2��
�������$��0,1,1,0q�._�  $
 2��$��Center=�$��Stretch=�   Maximum$ 10000Q� �� *AwardReferrerGoldCoin+$ TwoWay=��� $��3��
�������$��0,1,1,0q�._�  $
 2��$��Center=�$��Stretch=� �� *AwardReferrerMines+$ TwoWay=��� $��4��
�������$��0,1,1,0q�._�  $
 2��$��Center=�$��Stretch=� �� *AwardReferrerMiners+$ TwoWay=��� $��5��
�������$��0,1,1,0q�._�  $
 2��$��Center=�$��Stretch=� �� *AwardReferrerStones+$ TwoWay=��� $��6��
�������$��0,1,1,0q�._�  $
 2��$��Center=�$��Stretch=� �� *AwardReferrerDiamond+$ TwoWay=��� $��2��$��7��$ 
Horizontal=�.+��� $$��设置推荐人奖励层级：��$
��10,5q��� -   $��cmdAwardLevel��$��50�� �� SelectedIndex �� *AwardLevelFrom0+$ TwoWay=�.@��� $��1���� $��2���� $��3���� $��4���� $��5���� $��6���� -   $��tabitemGameConfig��$��矿场规则��.���� .��� $��Center=�$��Center=� �� $	��200 ��� $	��200 ��� $	��100 � �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��1 人民币 =��$��Right=�$��Center=�$��10q�  $��1��$	��100��$
��Left=�$��Center=�$��10q�$
 2�� �� *
Yuan_RMB+$ TwoWay=��� $��2��$
��Left=�$��Center=�$��10q����� *Strings.RMB+$ OneWay=�#	 [  �� $��1��$��0��$��Right=�$��Center=�$��10q��� $�1 �� �� ��� *Strings.RMB+$ OneWay=�#	 [   �� $�=��  $��1��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *RMB_GoldCoin+$ TwoWay=��� $��1��$��2��$
��Left=�$��Center=�$��10q����� *Strings.GoldCoin+$ OneWay=�#	 [  �� $��2��$��0��$��Right=�$��Center=�$��10q��� $�1 �� �� ��� *Strings.Mine+$ OneWay=�#	 [   �� $�=��  $��2��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *
RMB_Mine+$ TwoWay=��� $��2��$��2��$
��Left=�$��Center=�$��10q����� *Strings.RMB+$ OneWay=�#	 [  �� $��3��$��0��$��Right=�$��Center=�$��10q��� $�1 �� �� ��� *Strings.Miner+$ OneWay=�#	 [   �� $�=��  $��3��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *GoldCoin_Miner+$ TwoWay=��� $��3��$��2��$
��Left=�$��Center=�$��10q����� *Strings.GoldCoin+$ OneWay=�#	 [  �� $��4��$��0��$��Right=�$��Center=�$��10q��� $�1 �� �� ��� *Strings.RMB+$ OneWay=�#	 [   �� $�=��  $��4��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *
Stones_RMB+$ TwoWay=��� $��4��$��2��$
��Left=�$��Center=�$��10q����� *Strings.Stone+$ OneWay=�#	 [  �� $��5��$��0��$��Right=�$��Center=�$��10q��� $�1 �� �� ��� *Strings.RMB+$ OneWay=�#	 [   �� $�=��  $��5��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *Diamonds_RMB+$ TwoWay=��� $��5��$��2��$
��Left=�$��Center=�$��10q����� *Strings.Diamond+$ OneWay=�#	 [  �� $��6��$��矿山储量��$��3��.���  �� $	��200 ��� $	��200 ��� $	��100 � �� �� �� �� .+��� -   $��chkMineReserversRandom��$��0��$!��开采矿山为随机储量��$��10q�$��Right=� d� 	IsChecked �� *MineReservesIsRandom+�� $��1��$��0��$��矿山固定储量为：��$��Right=�$��Center=�$��10q�  $��1��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *StonesReservesPerMines+$ TwoWay=��� $��1��$��2��$
��Left=�$��Center=�$��10q����� *Strings.Stone+$ OneWay=�#	 [  �� $��2��$��0��$$��矿山最小随机储量为：��$��Right=�$��Center=�$��10q�  $��2��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *MinStonesReservesPerMine+$ TwoWay=��� $��2��$��2��$
��Left=�$��Center=�$��10q����� *Strings.Stone+$ OneWay=�#	 [  �� $��3��$��0��$$��矿山最大随机储量为：��$��Right=�$��Center=�$��10q�  $��3��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 1000000Q�$
 2�� �� *MaxStonesReservesPerMine+$ TwoWay=��� $��3��$��2��$
��Left=�$��Center=�$��10q����� *Strings.Stone+$ OneWay=�#	 [  �� $��7��$��0��$$��每个矿工每小时生产：��$��Right=�$��Center=�$��10q�  $��7��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 3�� �� *OutputStonesPerHour+$ TwoWay=��� $��7��$��2��$
��Left=�$��Center=�$��10q����� *Strings.Stone+$ OneWay=�#	 [  �� $��8��$��0��$!��购买矿石奖励倍数：��$��Right=�$��Center=�$��10q�  $��8��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 10000Q�$
 2�� �� *!StoneBuyerAwardGoldCoinMultiple+$ TwoWay=��� $��8��$��2��$��	倍金币��$
��Left=�$��Center=�$��10q��� -   $ ��lblExchangeExpensePercent1��$��9��$��0��$'��!出售矿石手续费百分比：��$��Right=�$��Center=�$��10q�  -     Name$ txtExchangeExpensePercent��$��9��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 2�� �� *ExchangeExpensePercent+$ TwoWay=��� -   $ ��lblExchangeExpensePercent2��$��9��$��2��$��%��$
��Left=�$��Center=�$��10q��� $��10��$��0��$��最低手续费：��$��Right=�$��Center=�$��10q�  $��10��$��1��$	��100��$
��Left=�$��Center=�$��10q�$
 5�� �� *ExchangeExpenseMinNumber+$ TwoWay=��� $��10��$��2��$
��Left=�$��Center=�$��10q����� *Strings.RMB+$ OneWay=�#	 [  �� $��11��$$��玩家最多可拥有矿工数��$��Right=�$��Center=�$��10q�  $��11��$��1��$	��100��$
��Left=�$��Center=�$��10q�$ 10000Q� �� *UserMaxHaveMinersCount+$ TwoWay=��� $��12��$��买家锁定订单时间��$��Right=�$��Center=�$��10q�  $��12��$��1��$	��100��$
��Left=�$��Center=�$��10q� �� *BuyOrderLockTimeMinutes+$ TwoWay=��� $��12��$��2��$��分钟��$
��Left=�$��Center=�$��10q��� $��13��$$��矿石生产有效记录时间��$��Right=�$��Center=�$��10q�  $��13��$��1��$
 1000Q�$	��100��$
��Left=�$��Center=�$��10q� �� *TempStoneOutputValidHour+$ TwoWay=��� $��13��$��2��$��小时��$
��Left=�$��Center=�$��10q��� $��14��$��可以提现的贡献值��$��Right=�$��Center=�$��10q�  $��14��$��1��$
 1000Q�$	��100��$
��Left=�$��Center=�$��10q� �� *CanExchangeMinExp+$ TwoWay=��� $��15��$��可以打折的贡献值��$��Right=�$��Center=�$��10q�  $��15��$��1��$
 1000Q�$	��100��$
��Left=�$��Center=�$��10q� �� *CanDiscountMinExp+$ TwoWay=��� $��16��$��折扣��$��Right=�$��Center=�$��10q�  $��16��$��1��$ 1Q�   Minimum$ 0Q�$
 3��$	��100��$
��Left=�$��Center=�$��10q� �� *
Discount+$ TwoWay=��� $��16��$��2��$��[0 - 1]��$
��Left=�$��Center=�$��10q��� -   $��btnSaveConfig��$��1��$��保  存��$��Right=�$��20,10q�$
��30,5q��� -   $��tabItemNotice��$��公  告��.���  �� $
��174* ��� $	��24* �.+�&   System.Windows.Controls.DataGrid  -     Name$ datagridNotices��   
IsReadOnly .    AutoGenerateColumns .     CanUserReorderColumns .     HeadersVisibility$ Column=�   SelectionMode$ Single=�   CanUserResizeRows .  $   HorizontalScrollBarVisibility$ Disabled=�   EnableRowVirtualization .     AlternatingRowBackground �	#FFE4E2E2$��14(�   RowDetailsVisibilityMode$ 	Collapsed=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    ClipboardContentBinding Z�    CellTemplate ��   $ *     Header$  标题�� Z�  �� .R��� $��Center=�$��Center=����� *Title+  $ * $  时间�� Z�  �� .R��� $��Center=�$��Center=����� *Time+� $��1�� �� �� .+��� $ 
Horizontal=�.+��� -   $��btnSelectAllNotices��$��5q�$��全选��$��Center=�$	��100���� -   $��btnclearAllNotices��$��5q�$��	全取消��$��Center=�$	��100���� $��1��$ 
Horizontal=�$��RightToLeft=�.+��� -   $��btnDeleteNotices��$��5q�$��删除通知��$��Center=�$	��100���� -   $��btnUpdateNotices��$��5q�$��修改通知��$��Center=�$	��100���� -   $��btnCreateNotices��$��5q�$��新建通知��$��Center=�$	��100���� -   $��tabItemPlayerManager��$��系统管理员��.���  �� $
��174* ��� $	��24* �.+�  -   $ datagridAdmin�� .  .   .  !   CanUserResizeColumns! .  "   CanUserSortColumns" .  $ Column=�$ Single=� .  $ Disabled=� .   �	#FFE4E2E2$��14(�$ 	Collapsed=�   $	 100  Z�  �� .R��� $��Center=�$��Center=� �� *	IsChecked+$ TwoWay=�  $ * $  	用户名�� Z�  �� .R��� $��Center=�$��Center=����� *
UserName+  $ * $	  Mac�� Z�  �� .R��� $��Center=�$��Center=����� *Mac+� $��1��.+��� $��1��$ 
Horizontal=�$��RightToLeft=�.+��� -    $��btnDeleteAdmin��$��5q�$��删除该管理员��$��Center=�$	��100���� -!   $��btnEditdmin��$��5q�$��修改管理员��$��Center=�$	��100���� -"   $��btnAddAdmin��$��5q�$��添加管理员��$��Center=�$	��100���� -#   $��tabItemClientVersion��$��客户端版本��.��� $��Center=�$��Center=� �� ��  �� �� .+��� $��最新客户端版本��$��10q��� -$   $��txtClientVersion��$��1��$��10q�$	��200���� -%   $��btnSaveClientVersion��$��1��$��1��$��保存��$	��100��$��10q��� $��修复功能��.���  �� $
��Auto ��� .+��� $�� ��$
��Left=�.���  �� �� .+��  �� �� .+��� -&   $��btnDeletePlayers��$!��按规则删除无效玩家��$	��200��$��10q��� -'   $��chkPerDayAutoDelete��$��1��$��每日12时自动删除��$��Center=�$��10q�� $��1��.+��� -(   $��btnDeleteAllSellStones��$y�	Collapsed=�$��清理矿石订单��$��10q�$	��150���� -)   $!��btnRejectAllSellStoneOrders��$$��驳回所有矿石出售订单��$y�	Collapsed=�$��10q�$��10q�$��Center=�$��Center=��� $��股市管理��.��� .+�� $	��Top=�$��Center=�$��10q� �� $��* ��� $��* ��� $��* �.+��� .+��� -*   $��btnCheckStackMarketState��$��检查股票状态��$	��100��$��10q��� -+   $��txtStackMarketState��$��1��$��10q��� $��2��.+��� -,   $��btnOpenMarket��$��开市��$	��100��$��10q��� --   $��btnSuspendMarket��$��间休��$	��100��$��10q��� -.   $��btnResumeMarket��$��继续��$	��100��$��10q��� -/   $��btnCloseMarket��$��闭市��$	��100��$��10q�