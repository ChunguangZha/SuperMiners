   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  M    GSuperMinersCustomServiceSystem.View.Windows.EditRouletteAwardItemWindow   4	MyControl&clr-namespace:SuperMinersWPF.MyControl  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$!  添加幸运大转盘奖项��$	��400��$	��300�� � WindowStyle$ 
ToolWindow=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��181* �.+��� $��ID��$
��10,5q�$��Right=�$��Center=��� -   $��txtRecordID��$��1�� �� 
IsReadOnly . $
��10,5q�$��Center=��� $��1��$��奖项信息��$
��10,5q�$��Right=�$��Center=��� -   $��txtAwardName��$��1��$��1��$
��10,5q�$��Center=��� $��2��$��奖项数值��$
��10,5q�$��Right=�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numAwardNumber��$��2��$��1��   Maximum$ 100000Q�$
��10,5q�$��Center=��� $��3��$��奖项类型��$
��10,5q�$��Right=�$��Center=��� -   $��cmbAwardType��$��3��$��1��$
��10,5q�$��Center=�	 �� SelectedIndex$	 0��.@��� $��	没摇中���� $��矿石���� $��金币���� $��	贡献值���� $��矿石储量���� $��实物���� $��4��$��价值人民币��$
��10,5q�$��Right=�$��Center=�  -   $ numValueMoneyYuan��$��4��$��1��$ 100000Q�$
��10,5q�$��Center=�
   DecimalPlaces$
 2���� -   $��chkIsLargeAward��$��5��$��1��$��是否为大奖��$
��10,5q�$��Center=��� $��6��$��中奖概率��$
��10,5q�$��Right=�$��Center=�  -   $ numWinProbability��$��6��$��1��$ 100000Q�$
��10,5q�$��Center=��� $��7��$��图标��$
��10,5q�$��Right=�$��Center=��� -	    ��Name$ imgIcon��$��7��$��1��$��64��$��64��$��10q�$
��Left=�$	��Top=��� -
   $��btnUpdateIcon��$��7��$��1��$��上传图片��$��70��$��Right=�$��10q�$��Center=��� -   $��	btnCancel��$��9��$��2��$��取消��$	��100��$��10q�$��Right=��� -   $��btnOK��$��9��$��2��$��确认��$	��100��$��10,10,120,10q�$��Right=�