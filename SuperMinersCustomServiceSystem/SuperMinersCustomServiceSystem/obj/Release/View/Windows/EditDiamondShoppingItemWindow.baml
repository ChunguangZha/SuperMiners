   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  O    ISuperMinersCustomServiceSystem.View.Windows.EditDiamondShoppingItemWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6myc&clr-namespace:SuperMinersWPF.MyControl     -     � Title$  修改钻石商品��$	��700��$	��800��$	��800��$	��700�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $	��100 ��� $
��Auto ���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��首图��$��Right=�$	��Top=�$��10q��� -   $��btnUploadFirstImage��$��上传首图��$��10,50,10,10q�$	��Top=��� -   $��imgIcon��$��1��$	��200��$	��200��$��5q�� $��2�� �� $	��110 ��� $
��140* ��� $	��91* ��� $
��121* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��编号��$��Right=�$��Center=�$
��10,5q��� -   $��txtID��$��0��$��1�� �� 
IsReadOnly . $��	10,5,10,0q� �� 	MaxLength$ 20��$��Center=�$��3���� $��1��$��0��$��标题��$��Right=�$	��Top=�$
��10,5q��� -   $��txtTitle��$��1��$��1��$��	10,5,10,0q�$ 20��$	��Top=�$��3���� $��2��$��0��$��产品描述��$
��10,5q�$��Right=�$	��Top=��� -   $��	txtRemark��$��2��$��1��$
��10,5q�$��70��$	 200�� �� AcceptsReturn .  �� 
AcceptsTab .  �� TextWrapping$
 Wrap=�$��Stretch=�"	 �� VerticalScrollBarVisibility$
	 Auto=�$��3���� $��3��$��0��$��价格（人民币）��$
��10,5q�$��Right=�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -   
  Name$
 numPriceYuan��-   'SuperMinersWPF.MyControl.NumericTextBox   ValueChanged$ numPriceYuan_ValueChanged��$��3��$��1��   DecimalPlaces$ 2��$	��100��   Maximum$ 100000Q�   Minimum$ 0Q�$��10,5,0,0q�$	��Top=�$
��Left=��� $��3��$��1��$	��元��$��	120,5,5,5q�$	��Top=�$
��Left=��� $��4��$��0��$��价格（钻石）��$
��10,5q�$��Right=�$��Center=�  -   $
 numPrice��$ numPrice_ValueChanged��$��4��$��1��$ 2��$	��100��$ 100000Q�$ 0Q�$��10,5,0,0q�$	��Top=�$
��Left=��� $��4��$��1��$	��块��$��	120,5,5,5q�$	��Top=�$
��Left=��� $��5��$��0��$��类别��$
��10,5q�$��Right=�$��Center=��� -	   $��cmbItemType��$��5��$��1�� �� DisplayMemberPath$ Value�� �� SelectedValuePath$	 Key��$	��100��$
��10,5q�$	��Top=�$
��Left=��� $��5��$��2��$��出售状态��$
��10,5q�$��Right=�$��Center=��� -
   $��cmbSellState��$��5��$��3��$
��10,5q�$��Center=� �� SelectedIndex$ 0��$	��100��$
��Left=�.@��� $��上架���� $��下架���� $��6��$��0��$��库存��$��5q�$��Center=�$��Right=�  -   $
 numStockCount��$��6��$��1��$	��100��$ 100000Q�$ 0Q�$
��10,5q�$��Center=�$
��Left=��� $��1��$��产品详情文本��$��10q�$��Right=�$	��Top=��� -   $��txtDetailText��$��1��$��1��$��3��$��50��$��10q�$	 200�� .  . $
 Wrap=�$
	 Auto=��� $��2��$��产品详情图片��$��10q�$��Right=�$	��Top=�� $��2��$��1��$��3�� �� �� $
��Auto �.+��� -   $��lvDetailImages��$��Stretch=�$��Disabled=� �� SelectionMode$ Single=�$��10q����� .R��� $��5q����� �� -   $��btnAddDetailImage��$��1��$��添加详情图片��$	��120��$��10q�$	��Top=��� -   $��btnMoveUpDetailImage��$��1��$��上移选中图片��$	��120��$��10,50,10,10q�$	��Top=��� -   $��btnMoveDownDetailImage��$��1��$��下移选中图片��$	��120��$��10,90,10,10q�$	��Top=��� -   $��btnDeleteDetailImage��$��1��$��删除选中图片��$	��120��$��10,130,10,10q�$	��Top=��� -   $��	btnCancel��$��3��$��4��$��取消��$	��100��$��Center=�$��10q�$��Right=��� -   $��btnSave��$��3��$��4��$��保存��$	��100��$��Center=�$��10,10,140,10q�$��Right=�