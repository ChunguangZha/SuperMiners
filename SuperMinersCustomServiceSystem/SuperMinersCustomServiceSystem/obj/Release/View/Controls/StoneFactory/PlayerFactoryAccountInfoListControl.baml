   M S B A M L   `   `   `  ���� "  SuperMinersCustomServiceSystemc    ]SuperMinersCustomServiceSystem.View.Controls.StoneFactory.PlayerFactoryAccountInfoListControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��矿石工厂账户管理��$��18(�$
��Bold"�$��10q�� $��1��  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�.+��� $��用户名：��$��Right=�$��Center=�$��5q��� -   $��txtUserName��$��5q�$	��100��$��Center=��� $��工厂状态��$��5q�$��Center=��� -   $��cmbUserGroup�� �� SelectedIndex$ 0��$��5q�$	��100��.@��� $��全部���� $��	开启中���� $��	已关闭���� -   $��	btnSearch��$��当前页面中查找��$	��100��$��70,5,5,5q�$
��Left=��� $��1��$ 
Horizontal=�.+��� -   $��
btnRefresh��$��重新加载列表��$	��150��$��5q�$
��Left=��� -   $��panelPlayersManager��$��2��$��5q�.��&   System.Windows.Controls.DataGrid  -   $��datagridPlayerInfos��   SelectionMode$ Single=�   AutoGenerateColumns .     FrozenColumnCount$ 2��   
IsReadOnly . ���� *ListFiltedPlayerFactoryInfos+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 用户ID��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *UserID+  $ 	用户名��$	 * 
 �� *
UserName+   ClipboardContentBinding Z�   $ 工厂状态��$	 * 
 �� *FactoryIsOpening+  $ 工厂寿命天数��$	 * 
 �� *FactoryIsOpening+  $ 运输中苦力��$	 * 
 �� *FreezingSlavesCount+  $ 劳动中苦力��$	 * 
 �� *EnableSlavesCount+  $ 食物存量��
 �� *Food+  $ 昨日凝练完成矿石��
 �� *LastDayValidStoneCount+  $ 凝练中矿石��$	 * 
 �� *TotalStoneCount+  $ 运输中矿石��$	 * 
 �� *FreezingStoneCount+  $ 可取回矿石��$	 * 
 �� *WithdrawableStoneCount+  $ 累计总收益��$	 * 
 �� *TotalProfitRMB+  $ 可提取的收益��$	 * 
 �� *WithdrawableProfitRMB+  $ 昨日总收益��$	 * 
 �� *YesterdayTotalProfitRMB+� $��3�� � ColumnDefinitions �� $	��100 ��� $��* ��� $	��130 ��� $��* ��� $	��100 ��� $��* �  �� �� �� �� .+��� $��	总计：��$
��10,5q��� $��1��$��运输中苦力：��$
��10,5q�$��Right=��� $��1��$��1�� �� 
IsReadOnly . $
��10,5q����� *#!SumAllFactory_FreezingMinersCount+ �� Mode$ OneWay=��� $��1��$��2��$��劳动中苦力：��$
��10,5q�$��Right=��� $��1��$��3�� . $
��10,5q����� *" SumAllFactory_WorkingMinersCount+$ OneWay=��� $��1��$��4��$��	食物：��$
��10,5q�$��Right=��� $��1��$��5�� . $
��10,5q����� *SumAllFactory_Foods+$ OneWay=��� $��2��$��运输中矿石：��$
��10,5q�$��Right=��� $��2��$��1�� . $
��10,5q����� *" SumAllFactory_FreezingStoneCount+$ OneWay=��� $��2��$��2��$��凝练中矿石：��$
��10,5q�$��Right=��� $��2��$��3�� . $
��10,5q����� *" SumAllFactory_WorkableStoneCount+$ OneWay=��� $��2��$��4��$��可取回矿石：��$
��10,5q�$��Right=��� $��2��$��5�� . $
��10,5q����� *&$SumAllFactory_WithdrawableStoneCount+$ OneWay=��� $��3��$��累计总收益：��$
��10,5q�$��Right=��� $��3��$��1�� . $
��10,5q����� *SumAllFactory_TotalProfit+$ OneWay=��� $��3��$��2��$��昨日凝练矿石：��$
��10,5q�$��Right=��� $��3��$��3�� . $
��10,5q����� *(&SumAllFactory_YesterdayValidStoneCount+$ OneWay=��� $��3��$��4��$��昨日总收益：��$
��10,5q�$��Right=��� $��3��$��5�� . $
��10,5q����� *SumAllFactory_YesterdayProfit+$ OneWay=�