   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      c6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  5      ]    WSuperMinersCustomServiceSystem.View.Controls.TradeSystem.DiamondShoppingItemListControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5      Fmyc6clr-namespace:SuperMinersCustomServiceSystem.MyControl     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��钻石商品项目列表��6   $��18(�6$   $
��Bold"�62   $��10q�6D   � 5   
   $��1��6     5      �� 5      �� 5      �� 5      .+�5      �� $��2��6    �� Orientation$ 
Horizontal=�6&   .+�5      �� $��商品类型：��6   $��10q�6)   �� 5      -   5        $��cmbItemType��5       �� DisplayMemberPath$ Value��6.    �� SelectedValuePath$	 Key��6H   $��10q�6`   $	��250��6l   �� 5      -   5        $��	btnSearch��5      $��查询��6*   $	��100��67   $��10q�6C   $��Center=�6O   6j   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dgRecords��5       $��2��6   $��5q�6d      AutoGenerateColumns .  $��Center=�6�   $
��Left=�6�      FrozenColumnCount$ 4��6�   ��61   �� *ListStoneDelegateBuyOrders+   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ 编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 61   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6<   �� *ID+ �� Mode$ OneWay=�6`     5"      $ 名称��6%   $	 100 61   	 . 
 6=   �� *Name+$ OneWay=�6c     5#      $ 类别��6%   $	 100 61   	 . 
 6=   �� *ItemTypeText+$ OneWay=�6k   4  .System.Windows.Controls.DataGridTemplateColumn  5$      $ 图标��6)   $	 100 65   	 .    CellTemplate 5%      �� 5&      .R�5'      �� $��64��6<   $��64��6G   $��5q�6S   ��6$   �� *Icon+6S   5(      5*        5+      $ 价格(人民币)��6%   $	 100 66   	 . 
 6B   �� *ValueRMBYuan+$ OneWay=�6p     5,      $ 价格(钻石)��6%   $ 70 65   	 . 
 6@   �� *ValueDiamonds+$ OneWay=�6o     5-      $ 库存��6%   $ 70 61   	 . 
 6<   �� *StocksCount+$ OneWay=�6i     5.      $ 说明��6%   $ * 61   	 . 
 6;   �� *Remark+$ OneWay=�6c     5/      $ 状态��6%   $ 70 61   	 . 
 6<   �� *SellStateText+$ OneWay=�6k   51      �� 52   
   $��3��6   $ 
Horizontal=�6"   $
��20,5q�6;   .+�53      �� -   5        $��
btnAddItem��53      $��添加新商品��6'   $	��150��67   $��10q�6C   6O   �� 54      -   5        $��btnUpdateItem��54      $��修改商品��6*   $	��150��69   $��10q�6E   6Q   55      56      57      