   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  ]    WSuperMinersCustomServiceSystem.View.Controls.TradeSystem.DiamondShoppingItemListControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     <	MyControl&clr-namespace:SuperMinersWPF.MyControl     Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��钻石商品项目列表��$��18(�$
��Bold"�$��10q�� $��1��  �� �� �� .+��� $��2�� �� Orientation$ 
Horizontal=�.+��� $��商品类型：��$��10q��� -   $��cmbItemType�� �� DisplayMemberPath$ Value�� �� SelectedValuePath$	 Key��$��10q�$	��250���� -   $��	btnSearch��$��查询��$	��100��$��10q�$��Center=�&   System.Windows.Controls.DataGrid  -   $��	dgRecords��$��2��$��5q�   AutoGenerateColumns .  $��Center=�$
��Left=�   FrozenColumnCount$ 4������ *ListStoneDelegateBuyOrders+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 编号��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *ID+ �� Mode$ OneWay=�  $ 名称��$	 100 	 . 
 �� *Name+$ OneWay=�  $ 类别��$	 100 	 . 
 �� *ItemTypeText+$ OneWay=�4  .System.Windows.Controls.DataGridTemplateColumn  $ 图标��$	 100 	 .    CellTemplate �� .R��� $��64��$��64��$��5q����� *Icon+  $ 价格(人民币)��$	 100 	 . 
 �� *ValueRMBYuan+$ OneWay=�  $ 价格(钻石)��$ 70 	 . 
 �� *ValueDiamonds+$ OneWay=�  $ 库存��$ 70 	 . 
 �� *StocksCount+$ OneWay=�  $ 说明��$ * 	 . 
 �� *Remark+$ OneWay=�  $ 状态��$ 70 	 . 
 �� *SellStateText+$ OneWay=��� $��3��$ 
Horizontal=�$
��20,5q�.+��� -   $��
btnAddItem��$��添加新商品��$	��150��$��10q��� -   $��btnUpdateItem��$��修改商品��$	��150��$��10q�