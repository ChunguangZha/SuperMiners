   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  ;    5SuperMinersWPF.Views.Windows.DelegateSellStoneWindows   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5my&clr-namespace:SuperMinersWPF.MyControl     -     � Title$!  委托服务器出售矿石��$	��330��$	��330�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�   PlayerInfoBackground#���   
������� � WindowStyle$ 
ToolWindow=�.��� $��5q� � ColumnDefinitions �� $
��Auto ��� $	��97* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��	总库存��$��Right=�$��5q��� -   $��txtAllStones��$��1�� �� 
IsReadOnly . $��5q��� $��2��$��5q����� *Strings.Stone+ �� Mode$ OneWay=�  Language �� Source# [ �� $��1��$��	已冻结��$��Right=�$��5q��� -   $��txtForzenStones��$��1��$��1�� . $��5q��� $��1��$��2��$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��2��$��	可出售��$��Right=�$��5q��� -   $��txtSellableStones��$��2��$��1�� . $��5q��� $��2��$��2��$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��3��$��本次出售��$��Right=�$��5q�-   'SuperMinersWPF.MyControl.NumericTextBox  -   	  Name$	 numSellStoneHandsCount��-   'SuperMinersWPF.MyControl.NumericTextBox
   ValueChanged$ 
 numSellStones_ValueChanged��$��3��$��1��   Minimum$ 0Q�   Maximum$ 100000Q�   Value$ 1Q�$��5q��� $��3��$��2��$��	手矿石��$��5q��� $��4��$��	挂单价��$��Right=�$��5q��� -    ��Name$ numPrice��$��4��$��1�� . 
��� � �$��5q��� -   $��sliderPrice��$��5��$��1�� � Value$	 100Q� � Minimum$	 100Q� � Maximum$	 110Q� � SmallChange$
 0.01Q�$��5q� � LargeChange$
 0.01Q��� $��4��$��2��$��灵币/手矿石��$��5q��� $��6��$��	手续费��$��Right=�$��5q��� -   $��
txtExpense��$��6��$��1�� . 
��� � �$��5q��� $��6��$��2��$��矿石��$��5q��� $��7��$��0��$��提示��$��Right=�$	��Top=�$��5q��� $��7��$��1��$��5q�.+��� $-��'取消订单时，手续费不予退回��$
��Wrap=�
���  ���� -	   $��	btnCancel��$��8��$��3��$��取消��$��75��$��Right=�$��5q��� -
   $��btnSell��$��8��$��3��$��挂单出售��$��75��$��Right=�$��	5,5,110,5q�