   M S B A M L   `   `   `  ����   SuperMinersWPF5    /SuperMinersWPF.Views.OrderRecordListSellControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -   .���   � RowDefinitions  �� $
��Auto ��� .+���  �� Orientation$ 
Horizontal=�.+��� $��订单状态��$��5q�$��Center=��� -   $��cmbOrderState��$	��100�� �� SelectedIndex$ 0��$��Center=�$��Center=�.@��� $��全部���� $��等待接单���� $��已被锁定���� $��交易成功���� $��交易异常���� $	��共��$��Center=�$��20,0,0,0q��� -   $��txtItemsCount��$��0��$��Center=�$
��10,0q��� $��	条记录��$��Center=��� -   $��listboxSellOrder��$��1��$��Stretch=�$��5q����� .R�� $��5q�  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $ 
Horizontal=�.+��� $��订单号：��$��14(�$��5q��� $��14(�$
��Bold"�$��5q����� *OrderNumber+�� $��挂单时间：��$��30,5,5,5q��� 
���  ��$��Italic$�$��5q����� *
SellTime+�� $��1��$ 
Horizontal=�.+��� $��卖出矿石：��$��5q��� 
���  ��$��Italic$�$��5q����� *SellStonesCount+�� $��价值灵币：��$��30,5,5,5q��� 
���  ��$��Italic$�$��5q����� *
ValueRMB+�� $��手续费：��$��30,5,5,5q��� 
���  ��$��Italic$�$��5q����� *	Expense+�� $��	状态：��$��30,5,5,5q��� 
���  ��$��Italic$�$��5q����� *OrderStateString+�� $��2�� �� StrokeDashArray$	 4,2R����	#FF8D8D8D	 �� X2$	 300��