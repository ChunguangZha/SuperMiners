   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  O    ISuperMinersCustomServiceSystem.View.Windows.EditVirtualShoppingItemWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     <	MyControl&clr-namespace:SuperMinersWPF.MyControl       � Title$  积分商品��$	��500��$	��500�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��3* ��� $
��Auto ��� $
��Auto ��� �� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��	编号：��$��5q�$��Right=��� -   $��txtID��$��1�� �� 
IsReadOnly . $��5q��� $��1��$��	名称：��$��5q�$��Right=��� -   $��txtName��$��1��$��1��$��5q� �� 	MaxLength$	 100���� $��2��$��	说明：��$��5q�$��Right=��� -   $��	txtRemark��$��2��$��1��$��3��$��5q�$
 1000�� �� AcceptsReturn . $
��Auto=�$��Disabled=��� $��5��$��	图标：��$��5q�$��Right=�� $��5��$��1��$��3�� �� $
��Auto ��� .+��� -    ��Name$ imgIcon��$��64��$��64��$��10q�$
��Left=�$��Stretch=��� -   $��btnUploadIcon��$��1��$��上传图标��$	��100��$��10q�$
��Left=�$��Center=��� $��8��$��商品状态：��$��5q�$��Right=��� -   $��cmbItemState��$��8��$��1��$��5q�	 �� SelectedIndex$	 0��.@��� $��上架���� $��下架���� $��9��$��商品类型：��$��5q�$��Right=��� -   $��cmbItemType��$��9��$��1��
 �� DisplayMemberPath$
 Value�� �� SelectedValuePath$	 Key��$��5q��� $��10��$��限购量：��$��5q�$��Right=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ txtPlayerMaxBuyCount��$��10��$��1��$��5q�   Minimum$ -1Q��� $��11��$��1��$+��%限购量小于等于0表示不限购��$��5q�
���  ���� $��0��$��3��$��价格(积分)：��
���  ��$��5q�$��Right=�  -	   $ txtPriceRMB��$��0��$��4��
���  ��$��5q�   DecimalPlaces$ 2��   Maximum$ 100000Q��� $��1��$��3��$��含贡献值：��$��5q�$��Right=�  -
   $ 
txtGainExp��$��1��$��4��$��5q�$ 2���� $��2��$��3��$��含灵币：��$��5q�$��Right=�  -   $ 
txtGainRMB��$��2��$��4��$��5q�$ 2���� $��3��$��3��$��含金币：��$��5q�$��Right=�  -   $ txtGainGoldCoin��$��3��$��4��$��5q�$ 2���� $��4��$��3��$��含矿石储量：��$��5q�$��Right=�  -   $ txtGainMine_StoneReserves��$��4��$��4��$��5q�$ 2���� $��5��$��3��$��含矿工：��$��5q�$��Right=�  -   $ txtGainMiner��$��5��$��4��$��5q�$ 2���� $��6��$��3��$��含矿石：��$��5q�$��Right=�  -   $ txtGainStone��$��6��$��4��$��5q�$ 2���� $��7��$��3��$��含钻石：��$��5q�$��Right=�  -   $ txtGainDiamond��$��7��$��4��$��5q�$ 2���� $��8��$��3��$��含积分：��$��5q�$��Right=�  -   $ txtGainShoppingCredits��$��8��$��4��$��5q�$ 2���� $��9��$��3��$��含碎片：��$��5q�$��Right=�  -   $ txtGainGravel��$��9��$��4��$��5q�$ 2���� -   $��btnOK��$��15��$��6��$��保存��$	��100��$��10,10,130,10q�$��Right=��� -   $��	btnCancel��$��15��$��6��$��取消��$	��100��$��10q�$��Right=�