   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  `    ZSuperMinersCustomServiceSystem.View.Controls.GameFunny.RouletteCurrentAwardItemListControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <	MyControl&clr-namespace:SuperMinersWPF.MyControl     @d2http://schemas.microsoft.com/expression/blend/2008     -   .��� $��5q�  � RowDefinitions  �� $
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
��Auto ��� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $	��100 ��� $	��300 ��� $	��100 ��� .+��� $��4��$>��8幸运大转盘当前展出的奖项集合（共12项）��$��18(�$
��Bold"�$��10q��� -   $��
btnRefresh��$��1��$��4��$��刷新��$	��100��$��10q�$
��Left=��� $��2��$��顺序��$��Center=�$��Center=�$��10q�$��16(�$
��Bold"��� $��2��$��1��$��奖项��$��Center=�$��Center=�$��10q�$��16(�$
��Bold"��� $��2��$��2��$��中奖概率��$��Center=�$��Center=�$��10q�$��16(�$
��Bold"��� $��3��$��第1项��$��Center=�$��Center=�$��5q��� -   $
��cmb1��$��3��$��1�� �� DisplayMemberPath$ 	AwardName�� �� SelectedValuePath$ ID��$��5q�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numWinProbability1��$��3��$��2��   Maximum$ 100000Q�$
��10,5q�$��Center=��� $��4��$��第2项��$��Center=�$��Center=�$��5q��� -   $
��cmb2��$��4��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability2��$��4��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��5��$��第3项��$��Center=�$��Center=�$��5q��� -   $
��cmb3��$��5��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability3��$��5��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��6��$��第4项��$��Center=�$��Center=�$��5q��� -	   $
��cmb4��$��6��$��1��$ 	AwardName��$ ID��$��5q�  -
   $ numWinProbability4��$��6��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��7��$��第5项��$��Center=�$��Center=�$��5q��� -   $
��cmb5��$��7��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability5��$��7��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��8��$��第6项��$��Center=�$��Center=�$��5q��� -   $
��cmb6��$��8��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability6��$��8��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��9��$��第7项��$��Center=�$��Center=�$��5q��� -   $
��cmb7��$��9��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability7��$��9��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��10��$��第8项��$��Center=�$��Center=�$��5q��� -   $
��cmb8��$��10��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability8��$��10��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��11��$��第9项��$��Center=�$��Center=�$��5q��� -   $
��cmb9��$��11��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability9��$��11��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��12��$��第10项��$��Center=�$��Center=�$��5q��� -   $��cmb10��$��12��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability10��$��12��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��13��$��第11项��$��Center=�$��Center=�$��5q��� -   $��cmb11��$��13��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability11��$��13��$��2��$ 100000Q�$
��10,5q�$��Center=��� $��14��$��第12项��$��Center=�$��Center=�$��5q��� -   $��cmb12��$��14��$��1��$ 	AwardName��$ ID��$��5q�  -   $ numWinProbability12��$��14��$��2��$ 100000Q�$
��10,5q�$��Center=��� -   $��btnSaveAllAwardItems��$��16��$��3��$��保存��$	��100��$��130,10,10,10q�$
��Left=�