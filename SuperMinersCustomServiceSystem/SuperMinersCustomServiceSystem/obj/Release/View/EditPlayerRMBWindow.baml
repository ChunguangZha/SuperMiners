   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  =    7SuperMinersCustomServiceSystem.View.EditPlayerRMBWindow   4	MyControl&clr-namespace:SuperMinersWPF.MyControl  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  修改玩家灵币��$	��300��$	��300�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � ColumnDefinitions �� $	��100 ���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+��� $��	玩家：��$��5q�$��Right=��� -   $��txtUserName��$��1�� �� 
IsReadOnly . $��5q��� $��1��$��0��$��现有灵币：��$��5q�$��Right=��� -   $��txtCurrentRMB��$��1��$��1�� . $��5q��� -   $��chkChangeType��$��2��$��5q�$��	充值：�� d� 	IsChecked$
 TrueY�$��Right=�$��Center=��� -   $��panelInCharge��$��2��$��1�� �� Orientation$ 
Horizontal=�.+�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numYuanValue��-   'SuperMinersWPF.MyControl.NumericTextBox	   ValueChanged$!	 NumericTextBox_ValueChanged��$��5q�$	��100��
   Minimum$
 0Q�   Maximum$ 10000Q� � ToolTip$B <充值时输入充入的人民币值，自动换算成灵币���� $��	人民币��$��5q��� $��3��$��修改后灵币：��$��5q�$��Right=�  -   $ txtRMBChanged��$��3��$��1��$
 0Q�$ 200000Q� . $��5q�   DecimalPlaces$ 2���� -   $��btnOK��$��4��$��2��$��确定��$	��100��$��Right=�$��Bottom=�$��	5,5,120,5q��� -   $��	btnCancel��$��4��$��2��$��取消��$	��100��$��Right=�$��Bottom=�$��5q�