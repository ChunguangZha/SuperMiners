   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  J    DSuperMinersCustomServiceSystem.View.Windows.EditPlayerGoldCoinWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :	MyControl&clr-namespace:SuperMinersWPF.MyControl    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  修改玩家金币��$	��300��$	��300�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $
��Auto ���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+��� $��	玩家：��$��5q�$��Right=��� -   $��txtUserName��$��1�� �� 
IsReadOnly . $��5q��� $��1��$��0��$��现有金币：��$��5q�$��Right=��� -   $��txtCurrentGoldCoin��$��1��$��1�� . $��5q��� -   $��cmbOper��$��2��$
��10,5q�$��Center=� �� SelectedIndex$ 0��$��Center=�.@��� $��增加���� $��减少���� -   $��panelInCharge��$��2��$��1�� �� Orientation$ 
Horizontal=�.+�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numGoldCoinValue��-   'SuperMinersWPF.MyControl.NumericTextBox	   ValueChanged$#	 numGoldCoinValue_ValueChanged��$��5q�$	��100��
   Minimum$
 0Q�   Maximum$ 	100000000Q��� $��金币��$��5q��� $��3��$��修改后金币：��$��5q�$��Right=�  -   $ numGoldCoinChanged��$��3��$��1��$
 0Q�$ 
1000000000Q� . $��5q��� -   $��btnOK��$��4��$��2��$��确定��$	��100��$��Right=�$��Bottom=�$��	5,5,120,5q��� -   $��	btnCancel��$��4��$��2��$��取消��$	��100��$��Right=�$��Bottom=�$��5q�